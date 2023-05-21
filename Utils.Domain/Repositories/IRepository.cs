using Utils.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Utils.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> : IConcurrencyHandler<TEntity>
        where TEntity : BaseEntity<TKey>
    {
        IUnitOfWork UnitOfWork { get; }

        #region Non Asynchronous Function
        IQueryable<TEntity> GetAll(bool isUntrackEntities = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFn = null);

        TEntity FindById(TKey id);

        void Delete(TEntity entity);

        void DeleteRange(IList<TEntity> entities);

        IQueryable<TEntity> ExecuteSqlRaw(string query);

        void BulkInsert(IEnumerable<TEntity> entities);

        void BulkInsert(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> columnNamesSelector);

        void BulkUpdate(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> columnNamesSelector);

        void BulkMerge(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> idSelector, Expression<Func<TEntity, object>> updateColumnNamesSelector, Expression<Func<TEntity, object>> insertColumnNamesSelector);

        void BulkDelete(IEnumerable<TEntity> entities);

        #endregion

        #region Asynchronous Function
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> FirstOrDefaultAsync<T>(Expression<Func<TEntity, bool>> predicate, bool isUntrackEntities = false);

        Task<TEntity> SingleOrDefaultAsync<T>(Expression<Func<TEntity, bool>> predicate, bool isUntrackEntities = false);

        Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query);
        #endregion
    }
}
