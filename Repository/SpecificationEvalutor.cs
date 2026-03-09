using Core.Entites;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.Dtata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> CreateQuery(IQueryable<T> Query, ISpecification<T> spec) 
        {
            var query = Query;
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy is not null)
            {
                query= query.OrderBy(spec.OrderBy);
            }
             if(spec.OrderByDesc is not null)
            {
                query=query.OrderByDescending(spec.OrderByDesc);
            }
            query =spec.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return query;
        }

    }
}
