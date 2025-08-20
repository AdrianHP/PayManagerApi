using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PayManager.DataAccess.Contracts.Repository
{
    public partial interface IRepository<TEntity> : IQueryable<TEntity>, IDisposable where TEntity : class
    {
        IObjectContext Context { get; }
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(params string[] properties);
        TEntity Find(object id);
        void Update(TEntity entity);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Commit();

        Task CommitAsync();

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindAsync(object id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(object id, params Expression<Func<TEntity, object>>[] properties);
        Task UpdateAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
    }
}