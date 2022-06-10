using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MessagingService.Domain.SeedWork;
using MongoDB.Driver;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IWriteMongoRepository<T> where T : class, IMongoEntity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<bool> AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
        Task<T> UpdateAsync(string id, T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(Expression<Func<T, bool>> predicate, UpdateDefinition<T> updateDefinition,
            CancellationToken cancellationToken);
    }
}