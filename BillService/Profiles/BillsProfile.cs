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
            CreateMap<Bill, GrpcBillModel>()
                .ForMember(dest => dest.BillId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src =>src.Publisher));
        }
    }
}