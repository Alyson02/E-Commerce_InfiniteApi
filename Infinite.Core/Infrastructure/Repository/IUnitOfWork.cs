using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infinite.Core.Infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task SaveChangesAsync();
        void SaveChanges();
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
        Task<int> ExecuteSQLAsync(string sql, params object[] parameteres);
        Task<IEnumerable<T>> QuerySQLAsync<T>(string sql, CancellationToken cancellation = default);
        Task<IEnumerable<T>> QuerySQLAsync<TFirst, TSecond, T>(string sql, Func<TFirst, TSecond, T> map, string splitOn = "Id", CancellationToken cancellation = default);
        Task<IEnumerable<T>> QuerySQLAsync<TFirst, TSecond, TThird, T>(string sql, Func<TFirst, TSecond, TThird, T> map, string splitOn = "Id", CancellationToken cancellation = default);
        Task<IEnumerable<T>> QuerySQLAsync<TFirst, TSecond, TThird, TFourth, T>(string sql, Func<TFirst, TSecond, TThird, TFourth, T> map, string splitOn = "Id", CancellationToken cancellation = default);
        Task<IEnumerable<T>> QuerySQLAsync<TFirst, TSecond, TThird, TFourth, TFifth, T>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, T> map, string splitOn = "Id", CancellationToken cancellation = default);
        Task<IEnumerable<T>> QuerySQLAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, T>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, T> map, string splitOn = "Id", CancellationToken cancellation = default);
        Task<IEnumerable<T>> QuerySQLAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T> map, string splitOn = "Id", CancellationToken cancellation = default);
        IRepository<T> Repository<T>() where T : class;
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, CancellationToken cancellation = default);
    }
}
