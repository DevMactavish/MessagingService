using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MessagingService.Domain.Repositories.Interfaces;
using MessagingService.Domain.SeedWork;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MessagingService.Infrastructure.Repositories.Mongo.Implementations
{
    public class WriteMongoRepository<T> : IWriteMongoRepository<T> where T : MongoDbEntity
    {
        protected readonly IMongoCollection<T> Collection;

        public WriteMongoRepository(IOptions<MongoDbSettings> options)
        {
            var mongoDbSettings = options.Value;
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var db = client.GetDatabase(mongoDbSettings.Database);
            var collectionSettings = new MongoCollectionSettings
            {
                GuidRepresentation = GuidRepresentation.Standard
            };
            Collection = db.GetCollection<T>(typeof(T).Name, collectionSettings);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            var options = new InsertOneOptions {BypassDocumentValidation = false};

            await Collection.InsertOneAsync(entity, options, cancellationToken);
            return entity;
        }

        public async Task<bool> AddRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            var options = new BulkWriteOptions {IsOrdered = false, BypassDocumentValidation = false};
            var listWrites = entities.Select(t => new InsertOneModel<T>(t)).Cast<WriteModel<T>>().ToList();

            return (await Collection.BulkWriteAsync(listWrites, options,cancellationToken)).IsAcknowledged;
        }

        public async Task<T> UpdateAsync(string id, T entity, CancellationToken cancellationToken)
        {
            return await Collection.FindOneAndReplaceAsync(x => x.Id == id, entity,cancellationToken:cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate,CancellationToken cancellationToken)
        {
            return await Collection.FindOneAndReplaceAsync(predicate, entity,cancellationToken:cancellationToken);
        }

        public async Task<bool> UpdateAsync(Expression<Func<T, bool>> predicate, UpdateDefinition<T> updateDefinition,CancellationToken cancellationToken)
        {
            var result = await Collection.UpdateOneAsync(predicate, updateDefinition,cancellationToken:cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount == 1;
        }
    }
}