using Core.Entites;
using Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase Database;
        public BasketRepository(IConnectionMultiplexer Redis)
        {
            Database=Redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await Database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket=await Database.StringGetAsync(basketId);
            if(Basket.IsNull) return null;
            else
              return  JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
          //Convert from CustomerBasket to RedisValue(Json)
          var JsonBasket=JsonSerializer.Serialize(Basket);
           var CreatedOrUpdated= await Database.StringSetAsync(Basket.Id, JsonBasket, TimeSpan.FromDays(1));
            if (!CreatedOrUpdated) return null;
           else
                return await GetBasketAsync(Basket.Id);
        }
    }
}
