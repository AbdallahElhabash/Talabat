using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IBasketRepository
    {
        // Get Basket
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        // Update Or Create Basket 
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);
        // Delete Basket
        Task<bool> DeleteBasketAsync(string basketId);

    }
}
