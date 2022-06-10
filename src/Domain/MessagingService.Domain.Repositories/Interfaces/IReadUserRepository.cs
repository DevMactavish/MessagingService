using MessagingService.Domain.Aggregates;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IReadUserRepository : IReadMongoRepository<User>
    {
    
    }
}

