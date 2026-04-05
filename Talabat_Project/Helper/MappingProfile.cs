using AutoMapper;
using Core.Entites;
using Core.Entites.Identity;
using Talabat_Project.DTOs;

namespace Talabat_Project.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(d=>d.ProductType,O=>O.MapFrom(S=>S.ProductType.Name))
                .ForMember(d=>d.ProductBrand,O=>O.MapFrom(S=>S.ProductBrand.Name))
                .ForMember(d=>d.PictureUrl,O=>O.MapFrom<ProductPictureUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();
        }
    }
}
