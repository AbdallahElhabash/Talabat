using AutoMapper;
using Core.Entites;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Project.DTOs;
using Talabat_Project.Errors;

namespace Talabat_Project.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository BasketRepo;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepo,IMapper _mapper)
        {
            BasketRepo = basketRepo;
            mapper = _mapper;
        }
    
        // Get Or ReCreate Basket
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>GetBasket(string BasketId)
        {
            var Basket=await BasketRepo.GetBasketAsync(BasketId);
            return Basket is null ? new CustomerBasket(BasketId) : Basket;
        }

        //Update Or Create Basket
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto Basket)
        {
            var MappedBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(Basket);
            var basket=await BasketRepo.UpdateBasketAsync(MappedBasket);
            return basket is null ? BadRequest(new ApiResponse(400)) : basket;
        }

        // Delete Basket
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
        {
            return await BasketRepo.DeleteBasketAsync(BasketId);
        }

    }
}
