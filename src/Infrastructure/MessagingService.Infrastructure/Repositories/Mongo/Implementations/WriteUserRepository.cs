using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace MessagingService.Infrastructure.Repositories.Mongo.Implementations
{
    public class WriteUserRepository : WriteMongoRepository<User>, IWriteUserRepository
    {
        public WriteUserRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}