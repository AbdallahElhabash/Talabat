using Core.Entites;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Project.Errors;

namespace Talabat_Project.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository BasketRepo;

        public BasketController(IBasketRepository basketRepo)
        {
            BasketRepo = basketRepo;
        }
    
        // Get Or ReCreate Basket
        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerBasket>>GetBasket(string BasketId)
        {
            var Basket=await BasketRepo.GetBasketAsync(BasketId);
            return Basket is null ? new CustomerBasket(BasketId) : Basket;
        }

        //Update Or Create Basket
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
        {
            var basket=await BasketRepo.UpdateBasketAsync(Basket);
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
