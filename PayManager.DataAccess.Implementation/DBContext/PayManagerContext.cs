using PayManager.Business.Contracts.ApplicationService;
using PayManager.Business.Domain;
using PayManager.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayManager.DataAccess.Implementation.DBContext
{
    public class PayManagerContext (
        DbContextOptions<PayManagerContext> options,
        IConfiguration configuration)
        : DbContext(options), IObjectContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<PaymentOrder> PaymentOrders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public string ConnectionString { get; private set; } = configuration.GetConnectionString("DefaultConnection");

        #region Generic methods

        public TEntity Find<TEntity>(object id) where TEntity : class
        {
            if (id == null)
                return null;
            if (id is object[] ids)
                return Set<TEntity>().Find(ids);
            return Set<TEntity>().Find(id);
        }

        public async Task<TEntity> FindAsync<TEntity>(object id) where TEntity : class
        {
            if (id == null)
                return null;
            if (id is object[] ids)
                return await Set<TEntity>().FindAsync(ids);
            return await Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public void Update<TEntity, TKey>(TEntity entity) where TEntity : Entity<TKey>
        {
            var old = Find<TEntity>(entity.Id);
            var oldEntity = Entry(old);
            oldEntity.CurrentValues.SetValues(entity);
            oldEntity.State = EntityState.Modified;
        }

        public new void Add<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public void Delete<TEntity, TKey>(TEntity entity) where TEntity : Entity<TKey>
        {
            TEntity old;
            if ((old = Find<TEntity>(entity.Id)) != null)
            {
                Set<TEntity>().Remove(old);
            }
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (Set<TEntity>().ToList().Any(x => x.Equals(entity)))
            {
                var old = Set<TEntity>().ToList().FirstOrDefault(x => x.Equals(entity));
                Set<TEntity>().Remove(old ??
                    throw new InvalidOperationException("Old Entity is null"));
            }
            else
            {
                var entityObj = Entry(entity);
                if (entityObj.State == EntityState.Detached)
                    Set<TEntity>().Attach(entityObj.Entity);
                Set<TEntity>().Remove(entity);
            }
        }

        public void Commit()
        {
            SaveChanges();
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaveChanges()
        {
        }

        #endregion
    }
}
