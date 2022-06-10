using System.Linq;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllSoftDeleted();
        IQueryable<TEntity> GetAllNotSoftDeleted();
        void Update(TEntity obj);
        void UpdateRange(TEntity[] obj);
        void Remove(int id);
        int SaveChanges();
    }
}
