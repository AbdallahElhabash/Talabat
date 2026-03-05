using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecificaions<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get;set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>() { };
        public BaseSpecificaions() 
        {
          //Includes = new List<Expression<Func<T, object>>>();
        }
        public BaseSpecificaions(Expression<Func<T, bool>>CriteriaExp)
        {
            Criteria = CriteriaExp;
         // Includes = new List<Expression<Func<T, object>>>();
        }
    }
}
