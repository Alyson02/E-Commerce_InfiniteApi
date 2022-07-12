using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinite.Core.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindAsync(ISpecification<T> spec, CancellationToken cancellationToken = default, bool asNoTracking = true);
        Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default, bool asNoTracking = true);
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default, bool asNoTracking = true);
        Task InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);

    }
}
