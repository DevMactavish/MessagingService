using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace MessagingService.Infrastructure.Repositories.Mongo.Implementations
{
    public class WriteMessageRepository:WriteMongoRepository<Message>,IWriteMessageRepository
    {
        public WriteMessageRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}