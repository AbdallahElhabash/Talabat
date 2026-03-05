using AutoMapper;
using AutoMapper.Execution;
using Core.Entites;
using Talabat_Project.DTOs;

namespace Talabat_Project.Helper
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;
        public ProductPictureUrlResolver(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
           if(!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["BaseUrl"]}{source.PictureUrl}";

           else return string.Empty ;
            
        }
    }
}
