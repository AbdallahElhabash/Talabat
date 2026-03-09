using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecificaions<Product>
    {
        public ProductWithBrandAndTypeSpecification(string? Sort):base()
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(Sort))
            {
                switch(Sort)
                {
                    case "Price":
                        AddOrderBy(p => p.price);
                        break;

                    case "PriceDesc":
                        AddOrderBy(p => p.price);
                        break;

                    default:
                        AddOrderBy(p=>p.Name); 
                        break;         
                }
            }
        }
       
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
