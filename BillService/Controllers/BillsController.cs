using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BillService.AsyncDataServices;
using BillService.Data;
using BillService.Dtos;
using BillService.Models;
using BillService.SyncDataServices.Http;

namespace BillService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public BillsController(
            IBillRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BillReadDto>> GetBills()
        {
            Console.WriteLine("--> Getting Bills....");

            var billItem = _repository.GetAllBills();

            return Ok(_mapper.Map<IEnumerable<BillReadDto>>(billItem));
        }

        [HttpGet("{id}", Name = "GetBillById")]
        public ActionResult<BillReadDto> GetBillById(int id)
        {
            var billItem = _repository.GetBillById(id);
            if (billItem != null)
            {
                return Ok(_mapper.Map<BillReadDto>(billItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<BillReadDto>> CreateBill(BillCreateDto billCreateDto)
        {
            var billModel = _mapper.Map<Bill>(billCreateDto);
            _repository.CreateBill(billModel);
            _repository.SaveChanges();

            var billReadDto = _mapper.Map<BillReadDto>(billModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendBillToCommand(billReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            //Send Async Message
            try
            {
                var billPublishedDto = _mapper.Map<BillPublishedDto>(billReadDto);
                billPublishedDto.Event = "Bill_Published";
                _messageBusClient.PublishNewBill(billPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetBillById), new { Id = billReadDto.Id}, billReadDto);
        }
    }
}