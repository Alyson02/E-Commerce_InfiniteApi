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
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.Repository<T>();
        }

        public Specification<T> CreateSpec(Expression<Func<T, bool>> criteria)
        {
            return new Specification<T>(criteria);
        }

        public Specification<T> CreateSpec()
        {
            return new Specification<T>();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public virtual async Task<T> FindAsync(ISpecification<T> spec, CancellationToken cancellationToken = default, bool asNoTracking = true)
        {
            return await _repository.FindAsync(spec, cancellationToken, asNoTracking);
        }

        public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _repository.InsertAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default, bool asNoTracking = true)
        {
            return await _repository.ListAsync(cancellationToken, asNoTracking);
        }

        public virtual async Task<IEnumerable<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default, bool asNoTracking = true)
        {
            return await _repository.ListAsync(spec, cancellationToken, asNoTracking);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
