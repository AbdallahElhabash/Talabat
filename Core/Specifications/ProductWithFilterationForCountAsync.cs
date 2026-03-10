using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithFilterationForCountAsync:BaseSpecificaions<Product>
    {
        public ProductWithFilterationForCountAsync(ProductSpecParams Params):
            base(P=>
            (!Params.BrandId.HasValue||P.ProductBrandId==Params.BrandId)
            &&
            (!Params.TypeId.HasValue||P.ProductTypeId==Params.TypeId)
            ) { }
    }
}
