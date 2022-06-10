using MessagingService.Domain.Aggregates;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IReadMessageRepository : IReadMongoRepository<Message>
    {
    
    }   
}