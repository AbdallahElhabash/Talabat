using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>where T : BaseEntity
    {
        // Sign for property for where Condition
        Expression<Func<T, bool>> Criteria { get; set; }
        //Sign for property for Includes
        List<Expression<Func<T, object>>> Includes { get; set; }
        //Sign for Order By
        Expression<Func<T,object>> OrderBy { get; set; }   
        //Sign for OrderByDesc
        Expression<Func<T,object>> OrderByDesc { get; set; }
        // Sign for Skip
        public int Skip { get; set; }
        //Sign for Take
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
