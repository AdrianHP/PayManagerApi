using PayManager.Business.Domain;
using PayManager.DataAccess.Contracts;
using PayManager.DataAccess.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PayManager.DataAccess.Implementation.Repository
{
    public class BaseRepository<TEntity, TKey>(IObjectContext context) : IRepository<TEntity> where TEntity : Entity<TKey>
    {
        public IObjectContext Context { get; private set; } = context;

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Query<TEntity>();
        }

        public IQueryable<TEntity> GetAll(params string[] properties)
        {
            var baseQuery = Context.Query<TEntity>();
            foreach (var property in properties)
                baseQuery = baseQuery.Include(property);
            return baseQuery;
        }

        public virtual TEntity Find(object id)
        {
            return Context.Find<TEntity>(id);
        }


        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Query<TEntity>().FirstOrDefaultAsync(predicate);
        }
        public async Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Query<TEntity>().LastOrDefaultAsync(predicate);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Context.Query<TEntity>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Update(TEntity entity)
        {
            Context.Update<TEntity, TKey>(entity);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.AddRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            Context.Delete<TEntity, TKey>(entity);
        }

        public async Task CommitAsync()
        {
            await Context.CommitAsync();
        }

        public void Commit()
        {
            Context.Commit();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Query<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(object id)
        {
            return await Context.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> FindAsync(object id, params Expression<Func<TEntity, object>>[] properties)
        {
            if (id == null)
                return null;
            var baseQuery = Context.Query<TEntity>();
            baseQuery = properties.Aggregate(baseQuery, (current, property) => current.Include(property));
            return await baseQuery.FirstOrDefaultAsync(a => a.Id.ToString() == id.ToString());
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Update<TEntity, TKey>(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            Context.Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            Context.AddRange(entities);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Context.Delete<TEntity, TKey>(entity);
            await Context.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
        }

        protected IDbConnection OpenConnection()
        {
            var connection = GetConnection();
            connection.Open();
            return connection;
        }

        protected IDbConnection GetConnection()
        {
            var connection = new NpgsqlConnection(Context.ConnectionString);
            return connection;
        }

        public Expression Expression { get; } = context.Query<TEntity>().Expression;
        public Type ElementType => typeof(TEntity);
        public IQueryProvider Provider { get; } = context.Query<TEntity>().Provider;
    }
}