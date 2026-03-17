using AutoMapper;
using Core.Entites;
using Core.Repositories;
using Core.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat_Project.DTOs;
using Talabat_Project.Errors;
using Talabat_Project.Helper;

namespace Talabat_Project.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IGenaricRepository<Product> ProductRepo;
        private readonly IGenaricRepository<ProductBrand> BrandRepo;
        private readonly IGenaricRepository<ProductType> TypeRepo;
        private readonly IMapper mapper;

        public ProductController(IGenaricRepository<Product> productRepo, IGenaricRepository<ProductBrand> brandRepo, IGenaricRepository<ProductType> typeRepo, IMapper _Mapper)
        {
            ProductRepo = productRepo;
            BrandRepo = brandRepo;
            TypeRepo = typeRepo;
            mapper = _Mapper;
        }
        // Get All Products
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts([FromQuery]ProductSpecParams Params)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(Params);
            var Products = await ProductRepo.GetAllWithSpecificationAsync(Spec);
            var MappedProduct = mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(Products);
            var CountSpec = new ProductWithFilterationForCountAsync(Params);
            var ReturnedObject = new Pagination<ProductToReturnDto>()
            {
                PageSize = Params.PageSize,
                PageIndex = Params.PageIndex,
                Count =await ProductRepo.GetCountProductsWithSpecificationAsync(CountSpec),
            Data = MappedProduct
            };
            return Ok(ReturnedObject);
        }
        // Get Product By Id
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int Id)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(Id);
            var Product = await ProductRepo.GetByIdWithSpecificationAsync(Spec);
            if (Product is null) return NotFound(new ApiResponse(404));
            var MappedProduct = mapper.Map<Product, ProductToReturnDto>(Product);
            return Ok(MappedProduct);
        }

        [HttpGet("Brands")]
       public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Barnds = await BrandRepo.GetAllAsync();
            return Ok(Barnds);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await TypeRepo.GetAllAsync();
            return Ok(Types);
        }
}
}
