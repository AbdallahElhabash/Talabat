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
        public ProductWithBrandAndTypeSpecification(ProductSpecParams Params):
            base(p => 
                 (!Params.BrandId.HasValue||p.ProductBrandId==Params.BrandId)
                 &&
                 (!Params.TypeId.HasValue||p.ProductTypeId==Params.TypeId)
                )
        { 
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "Price":
                        AddOrderBy(p => p.price);
                        break;

                    case "PriceDesc":
                        AddOrderByDesc(p => p.price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);
        }
       
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
