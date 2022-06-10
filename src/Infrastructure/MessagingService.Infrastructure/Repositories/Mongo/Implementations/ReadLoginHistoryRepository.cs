using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace MessagingService.Infrastructure.Repositories.Mongo.Implementations
{
    public class ReadLoginHistoryRepository : ReadMongoRepository<LoginHistory>, IReadLoginHistoryRepository
    {
        public ReadLoginHistoryRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}