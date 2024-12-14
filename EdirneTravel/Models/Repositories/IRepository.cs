using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace EdirneTravel.Models.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Table { get; }
        TEntity GetById(int id, bool asTracking = false);
        Task<TEntity> GetByIdAsync(int id, bool asTracking = false, CancellationToken cancellationToken = default);
        IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> filterExpression = null, bool asTracking = false);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(int id);
        void DeleteRange(IEnumerable<TEntity> entities);
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        PagedList<TEntity> GetList(PaginationParameters paginationParameters, FilterParameters filterParameters);
        Task<IDbContextTransaction> BeginTransactionAsync();
        bool Exists(Expression<Func<TEntity, bool>> predicate);
    }
}
