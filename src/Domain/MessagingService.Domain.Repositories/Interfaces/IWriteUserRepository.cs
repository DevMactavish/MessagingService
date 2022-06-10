using MessagingService.Domain.Aggregates;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IWriteUserRepository:IWriteMongoRepository<User>
    {
        
    }
}