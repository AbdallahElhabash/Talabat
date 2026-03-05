using Core.Entites;
using Core.Repositories;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talabat_Project.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IGenaricRepository<Product> genaricRepository;

        public ProductController(IGenaricRepository<Product>GenaricRepository) 
        {
            genaricRepository = GenaricRepository;
        }
        // Get All Products
        [HttpGet]
       public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var Spec = new ProductWithBrandAndTypeSpecification();
            var Products = await genaricRepository.GetAllWithSpecificationAsync(Spec);
            return Ok(Products);
        }
        // Get Product By Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>>GetProductById(int Id)
        {
            var Spec=new ProductWithBrandAndTypeSpecification(Id);
            var Product = await genaricRepository.GetByIdWithSpecificationAsync(Spec);
            return Ok(Product);
        }
    }
}
