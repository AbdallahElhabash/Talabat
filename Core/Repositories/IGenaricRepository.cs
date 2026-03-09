using Core.Entites;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        #region Without Specification
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        #endregion

        #region With Specification
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> Spec);
        Task<T>GetByIdWithSpecificationAsync(ISpecification<T> Spec);
        #endregion
    }
}
