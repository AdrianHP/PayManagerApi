using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Contracts.ApplicationService
{
    public interface IApplicationService<TEntity> : IQueryable<TEntity>, IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(params string[] properties);
        TEntity Find(object id);
        IQueryable<TEntity> FromSql(string sqlQuery);
        Task<TEntity> FindAsync(object id, params Expression<Func<TEntity, object>>[] properties);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(Expression<Func<TEntity, bool>> predicate, params string[] properties);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindAsync(object id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
