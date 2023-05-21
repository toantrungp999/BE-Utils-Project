using Utils.Domain.Entities;
using Utils.Domain.Repositories;
using Utils.Persistence.Contexts;
using EntityFrameworkCore.SqlServer.SimpleBulks.BulkDelete;
using EntityFrameworkCore.SqlServer.SimpleBulks.BulkInsert;
using EntityFrameworkCore.SqlServer.SimpleBulks.BulkMerge;
using EntityFrameworkCore.SqlServer.SimpleBulks.BulkUpdate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Utils.Persistence.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;

        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _dbContext;
            }
        }

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Non Asynchronous Function

        public IQueryable<TEntity> GetAll(bool isUntrackEntities = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFn = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (isUntrackEntities)
            {
                query = query.AsNoTracking();
            }

            if (includeFn is not null)
            {
                query = includeFn(query);
            }

            return query;
        }

        public TEntity FindById(TKey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public IQueryable<TEntity> ExecuteSqlRaw(string query)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(query);
        }

        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            _dbContext.BulkInsert(entities);
        }

        public void BulkInsert(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> columnNamesSelector)
        {
            _dbContext.BulkInsert(entities, columnNamesSelector);
        }

        public void BulkUpdate(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> columnNamesSelector)
        {
            _dbContext.BulkUpdate(entities, columnNamesSelector);
        }

        public void BulkMerge(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> idSelector, Expression<Func<TEntity, object>> updateColumnNamesSelector, Expression<Func<TEntity, object>> insertColumnNamesSelector)
        {
            _dbContext.BulkMerge(entities, idSelector, updateColumnNamesSelector, insertColumnNamesSelector);
        }

        public void BulkDelete(IEnumerable<TEntity> entities)
        {
            _dbContext.BulkDelete(entities);
        }

        public void SetRowVersion(TEntity entity, byte[] version)
        {
            _dbContext.Entry(entity).OriginalValues[nameof(entity.RowVersion)] = version;
        }

        public bool IsDbUpdateConcurrencyException(Exception e)
        {
            return e is DbUpdateConcurrencyException;
        }

        #endregion

        #region Asynchronous Function

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync<T>(Expression<Func<TEntity, bool>> predicate, bool isUntrackEntities = false)
        {
            IQueryable<TEntity> query = GetAll(isUntrackEntities);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync<T>(Expression<Func<TEntity, bool>> predicate, bool isUntrackEntities = false)
        {
            IQueryable<TEntity> query = GetAll(isUntrackEntities);
            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query)
        {
            return await query.ToListAsync();
        }

        #endregion
    }
}
