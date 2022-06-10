using System;
using System.Linq;
using MessagingService.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessagingService.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> GetAllSoftDeleted()
        {
            return DbSet.IgnoreQueryFilters()
                .Where(e => EF.Property<bool>(e, "IsDeleted") == true);
        }
        
        public virtual IQueryable<TEntity> GetAllNotSoftDeleted()
        {
            return DbSet.IgnoreQueryFilters()
                .Where(e => EF.Property<bool>(e, "IsDeleted") == false);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void UpdateRange(TEntity[] obj)
        {
            DbSet.UpdateRange(obj);
        }

        public virtual void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}