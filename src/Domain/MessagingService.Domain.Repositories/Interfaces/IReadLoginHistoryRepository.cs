using MessagingService.Domain.Aggregates;

namespace MessagingService.Domain.Repositories.Interfaces
{
    public interface IReadLoginHistoryRepository : IReadMongoRepository<LoginHistory>
    {
    }
}
