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
        public ProductWithBrandAndTypeSpecification():base()
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
        public ProductWithBrandAndTypeSpecification(Expression<Func<Product,bool>>CriteriaExp)
        {
            Criteria= CriteriaExp;
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
