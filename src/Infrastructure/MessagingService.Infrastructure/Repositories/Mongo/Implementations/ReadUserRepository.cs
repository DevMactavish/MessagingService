using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace MessagingService.Infrastructure.Repositories.Mongo.Implementations
{
    public class ReadUserRepository:ReadMongoRepository<User>,IReadUserRepository
    {
        public ReadUserRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}