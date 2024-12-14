
using EdirneTravel.Core.Helpers;
using EdirneTravel.Data;
using EdirneTravel.Models.Entities.Base;
using EdirneTravel.Models.Enums;
using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace EdirneTravel.Models.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly DbSet<TEntity> _dbSet;
        protected readonly AppDbContext _dbContext;
        protected readonly Expression<Func<TEntity, bool>> _defaultFilter;

        public Repository(AppDbContext dbContext, Expression<Func<TEntity, bool>> defaultFilter = null)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _defaultFilter = defaultFilter;
        }

        [Obsolete("This type is obsolete and will be removed in a future version.", false)]
        public IQueryable<TEntity> Table => _dbSet.AsQueryable();

        public virtual TEntity GetById(int id, bool asTracking = false)
        {
            return GetDbSetForSelect(asTracking).FirstOrDefault(t => t.Id == id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, bool asTracking = false,
            CancellationToken cancellationToken = default)
        {
            return await GetDbSetForSelect(asTracking).FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public virtual IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> filterExpression = null,
            bool asTracking = false)
        {
            return filterExpression != null ? GetDbSetForSelect(asTracking).Where(filterExpression) : GetDbSetForSelect(asTracking);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRangeAsync(entities);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            TEntity originalEntity = GetById(entity.Id);

            Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AttachRange(entities);

            List<int> ids = entities.Select(x => x.Id).ToList();
            List<TEntity> originalEntities = GetDbSetForSelect().Where(x => ids.Any(y => y == x.Id)).ToList();

            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity entity = GetById(id);

            Delete(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
            }

            _dbSet.RemoveRange(entities);
        }

        public PagedList<TEntity> GetList(PaginationParameters paginationParameters, FilterParameters filterParameters)
        {
            IQueryable<TEntity> filteredEntites = Select();

            var filters = filterParameters.Filters;

            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    Expression<Func<TEntity, bool>> filterExpression = FilterHelper<TEntity>.BuildFilterExpression<TEntity>(filter);
                    filteredEntites = filteredEntites.Where(filterExpression);
                }
            }

            if (filterParameters.OrderType != OrderTypeEnum.None)
                filteredEntites = FilterHelper<TEntity>.ApplyOrderBy(filteredEntites, filterParameters.OrderProp, filterParameters.OrderType);

            return PagedList<TEntity>.ToPagedList(filteredEntites.ToList(), paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null) Console.WriteLine(ex.InnerException.Message);

#if DEBUG
                throw;
#else
                // throw new Exception("DB_RELATED_SERVER_ERROR");
                // Temporary Detailed Error
                throw new Exception("DB_RELATED_SERVER_ERROR" + " | " + ex.Message + (ex.InnerException != null ? " | " + ex.InnerException.Message : ""));
#endif
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        protected virtual DbSet<TEntity> GetDbSet(bool asTracking = false)
        {
            return _dbSet;
        }

        protected virtual IQueryable<TEntity> GetDbSetForSelect(bool asTracking = false)
        {
            IQueryable<TEntity> dbSetResult = asTracking ? _dbSet.AsQueryable() : _dbSet.AsNoTracking();

            if (_defaultFilter != null)
            {
                dbSetResult = dbSetResult.Where(_defaultFilter);
            }

            return dbSetResult;
        }

        public void Attach(TEntity entity)
        {
            var existingEntity = _dbContext.ChangeTracker.Entries<TEntity>()
                .FirstOrDefault(e => e.Entity.Id == entity.Id);

            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity.Entity).State = EntityState.Detached;
            }

            _dbSet.Attach(entity);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
    }
}
