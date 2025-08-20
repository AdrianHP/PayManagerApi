using PayManager.Business.Contracts.ApplicationService;
using PayManager.DataAccess.Contracts.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.ApplicationService
{
    public abstract partial class BaseApplicationService<TEntity> : IApplicationService<TEntity> where TEntity : class
    {
        protected IRepository<TEntity> BaseRepository { get; }


        protected string TableName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="tableName">Name of Table associated in DB</param>
        /// <param name="eventPublisher"></param>
        protected BaseApplicationService(IRepository<TEntity> repository, string tableName)
        {
            BaseRepository = repository;
            TableName = tableName;
            Provider = BaseRepository.Provider;
            Expression = BaseRepository.Expression;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return BaseRepository.GetAll();
        }

        public IQueryable<TEntity> GetAll(params string[] properties)
        {
            return BaseRepository.GetAll(properties);
        }

        public virtual TEntity Find(object id)
        {
            return BaseRepository.Find(id);
        }
          public TEntity Find(Expression<Func<TEntity, bool>> predicate, params string[] properties)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FindAsync(object id, params Expression<Func<TEntity, object>>[] properties)
        {
            return await BaseRepository.FindAsync(id, properties);
        }

        public virtual void Add(TEntity entity)
        {
            BaseRepository.Add(entity);
            BaseRepository.Commit();
        }

        public virtual void Update(TEntity entity)
        {
            BaseRepository.Update(entity);
            BaseRepository.Commit();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            // update all entities
            foreach (var entity in entities)
            {
                BaseRepository.Update(entity);
            }

            // commit the changes
            BaseRepository.Commit();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            // update all entities
            foreach (var entity in entities)
            {
                await BaseRepository.UpdateAsync(entity);
            }

            await BaseRepository.CommitAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            BaseRepository.Delete(entity);
            BaseRepository.Commit();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await BaseRepository.GetAllAsync();
        }

        public virtual async Task<TEntity> FindAsync(object id)
        {
            return await BaseRepository.FindAsync(id);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await BaseRepository.UpdateAsync(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await BaseRepository.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await BaseRepository.DeleteAsync(entity);
        }

        public void Dispose()
        {
        }



        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await BaseRepository.AddRangeAsync(entities);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await BaseRepository.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await BaseRepository.LastOrDefaultAsync(predicate);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return BaseRepository.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

      

        public Expression Expression { get; }
        public Type ElementType => typeof(TEntity);
        public IQueryProvider Provider { get; }
    }
}