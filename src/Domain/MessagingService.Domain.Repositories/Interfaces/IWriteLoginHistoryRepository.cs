using MessagingService.Domain.Aggregates;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IWriteLoginHistoryRepository:IWriteMongoRepository<LoginHistory>
    {
        
    }
}