using AutoMapper;
using Core.Entites;
using Core.Repositories;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Project.DTOs;
using Talabat_Project.Errors;

namespace Talabat_Project.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IGenaricRepository<Product> genaricRepository;
        private readonly IMapper mapper;

        public ProductController(IGenaricRepository<Product>GenaricRepository,IMapper _Mapper) 
        {
            genaricRepository = GenaricRepository;
            mapper = _Mapper;
        }
        // Get All Products
        [HttpGet]
       public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecification();
            var Products = await genaricRepository.GetAllWithSpecificationAsync(Spec);
            var MappedProduct=mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(Products) ;
            return Ok(MappedProduct);
        }
        // Get Product By Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>>GetProductById(int Id)
        {
            var Spec=new ProductWithBrandAndTypeSpecification(Id);
            var Product = await genaricRepository.GetByIdWithSpecificationAsync(Spec);
            if (Product is null) return NotFound(new ApiResponse(404));
            var MappedProduct=mapper.Map<Product,ProductToReturnDto>(Product) ;
            return Ok(MappedProduct);
        }
    }
}
