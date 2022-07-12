using Infinite.Core.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinite.Core.Business.Services.Base
{
    public interface IServiceBase<T> where T : class
    {
        Task<T> FindAsync(ISpecification<T> spec, CancellationToken cancellationToken = default, bool asNoTracking = true);
        Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default, bool asNoTracking = true);
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default, bool asNoTracking = true);
        Task InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Specification<T> CreateSpec(Expression<Func<T, bool>> criteria);
        Specification<T> CreateSpec();
    }
}
