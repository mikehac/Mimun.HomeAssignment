using AutoMapper;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Models;

namespace Mimun.HomeAssignment.Mapping
{
    public class EntityiesProfile : Profile
    {
        public EntityiesProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Contract, ContractDto>()
                .ForMember(m => m.TypeName, o => o.MapFrom(x => x.Type.Type))
                .ReverseMap();
        }
    }
}
