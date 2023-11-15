using AutoMapper;
using BillService.Dtos;
using BillService.Models;

namespace BillService.Profiles
{
    public class BillsProfile : Profile
    {
        public BillsProfile()
        {
            // Source -> Target
            CreateMap<Bill, BillReadDto>();
            CreateMap<BillCreateDto, Bill>();
            CreateMap<BillReadDto, BillPublishedDto>();
        }
    }
}