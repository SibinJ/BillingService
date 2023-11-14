using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using BillService.Data;

namespace BillService.SyncDataServices.Grpc
{
    public class GrpcBillService : GrpcBill.GrpcBillBase
    {
        private readonly IBillRepo _repository;
        private readonly IMapper _mapper;

        public GrpcBillService(IBillRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<BillResponse> GetAllBills(GetAllRequest request, ServerCallContext context)
        {
            var response = new BillResponse();
            var bills = _repository.GetAllBills();

            foreach(var plat in bills)
            {
                response.Bill.Add(_mapper.Map<GrpcBillModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}