using AutoMapper;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Models;

namespace Mimun.HomeAssignment.Mapping
{
    public class EntityiesProfile : Profile
    {
        public EntityiesProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForPath(m => m.Address.CustomerId, o => o.MapFrom(x => x.Id))
                .ForPath(m => m.Address.City, o => o.MapFrom(x => x.City))
                .ForPath(m => m.Address.Street, o => o.MapFrom(x => x.Street))
                .ForPath(m => m.Address.HouseNumber, o => o.MapFrom(x => x.HouseNumber))
                .ForPath(m => m.Address.PostalCode, o => o.MapFrom(x => x.PostalCode))
                .ReverseMap();
            CreateMap<Contract, ContractDto>()
                .ForMember(m => m.TypeName, o => o.MapFrom(x => x.Type.Type))
                .ReverseMap();
            CreateMap<Package, PackageDto>()
                .ForMember(m => m.PackageTypeName, o => o.MapFrom(x => x.PackageType.TypeName))
                .ReverseMap();
        }
    }
}
