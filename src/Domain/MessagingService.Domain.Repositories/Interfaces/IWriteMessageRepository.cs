using MessagingService.Domain.Aggregates;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IWriteMessageRepository:IWriteMongoRepository<Message>
    {
        
    }
}