using MessagingService.Domain.SeedWork;

namespace MessagingService.Domain.SeedWork
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }

    }
}
