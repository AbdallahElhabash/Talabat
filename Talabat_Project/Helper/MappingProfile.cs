using AutoMapper;
using Core.Entites;
using Talabat_Project.DTOs;

namespace Talabat_Project.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(d=>d.ProductType,O=>O.MapFrom(S=>S.ProductType.Name))
                .ForMember(d=>d.ProductBrand,O=>O.MapFrom(S=>S.ProductBrand.Name));
        }
    }
}
