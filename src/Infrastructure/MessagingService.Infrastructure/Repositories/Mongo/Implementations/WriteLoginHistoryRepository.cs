using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace MessagingService.Infrastructure.Repositories.Mongo.Implementations
{
    public class WriteLoginHistoryRepository:WriteMongoRepository<LoginHistory>,IWriteLoginHistoryRepository
    {
        public WriteLoginHistoryRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}