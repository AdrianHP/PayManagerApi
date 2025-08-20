using Insurella.Business.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayManager.Business.Domain;


namespace PayManager.DataAccess.Contracts
{
    public interface IObjectContext : IDisposable
    {
        string ConnectionString { get; }
        TEntity Find<TEntity>(object id) where TEntity : class;
        Task<TEntity> FindAsync<TEntity>(object id) where TEntity : class;
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		void Add<TEntity>(TEntity entity) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity, TKey>(TEntity entity) where TEntity : Entity<TKey>;
        void Update<TEntity, TKey>(TEntity entity) where TEntity : Entity<TKey>;
        void Commit();
        Task CommitAsync();
        Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        EntityEntry Entry(object entity);
    }
}
