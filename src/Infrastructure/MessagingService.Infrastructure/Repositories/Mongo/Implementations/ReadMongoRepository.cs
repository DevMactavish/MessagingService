using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MessagingService.Domain.Repositories.Interfaces;
using MessagingService.Domain.SeedWork;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MessagingService.Infrastructure.Repositories.Mongo.Implementations
{
    public class ReadMongoRepository<T> : IReadMongoRepository<T> where T : MongoDbEntity
    {
        protected readonly IMongoCollection<T> Collection;
        private readonly MongoDbSettings _mongoDbSettings;

        public ReadMongoRepository(IOptions<MongoDbSettings> options)
        {
            _mongoDbSettings = options.Value;
            var client = new MongoClient(_mongoDbSettings.ConnectionString);
            var db = client.GetDatabase(_mongoDbSettings.Database);
            var collectionSettings = new MongoCollectionSettings
            {
                GuidRepresentation = GuidRepresentation.Standard
            };
            Collection = db.GetCollection<T>(typeof(T).Name, collectionSettings);
        }

        public IMongoQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? Collection.AsQueryable()
                : Collection.AsQueryable().Where(predicate);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return Collection.AsQueryable().Where(x => !x.IsDeleted).FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return predicate == null
                ? Collection.AsQueryable().Where(x => !x.IsDeleted).ToListAsync(cancellationToken)
                : Collection.AsQueryable().Where(x => !x.IsDeleted).Where(predicate).ToListAsync(cancellationToken);
        }

        public Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return Collection.AsQueryable().Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return Collection.AsQueryable().Where(x => !x.IsDeleted).AnyAsync(predicate, cancellationToken);
        }
    }
}