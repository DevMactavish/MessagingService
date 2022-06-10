using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MessagingService.Domain.SeedWork;
using MongoDB.Driver.Linq;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IReadMongoRepository<T> where T : class, IMongoEntity
    {
        IMongoQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}