using Core.Entites;
using Core.Repositories;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Repository.Dtata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbContext;

        public GenaricRepository(StoreContext DbContext)
        {
            dbContext = DbContext;
        }
        #region GetAll and GetById With Specification
        public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecificationAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }
        #endregion

        #region GetAll and GetById Without Specification
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
         
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }
        #endregion

        private IQueryable<T>ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvalutor<T>.CreateQuery(dbContext.Set<T>(), Spec);
        }

        public async Task<int> GetCountProductsWithSpecificationAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).CountAsync();
        }
    }
}
