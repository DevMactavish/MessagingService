using MessagingService.Domain.Aggregates;
using MessagingService.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace MessagingService.Infrastructure.Repositories.Mongo.Implementations
{
    public class ReadMessageRepository : ReadMongoRepository<Message>, IReadMessageRepository
    {
        public ReadMessageRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}