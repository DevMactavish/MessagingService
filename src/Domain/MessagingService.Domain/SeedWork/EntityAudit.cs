using System;
using MessagingService.Domain.SeedWork;

namespace MessagingService.Domain.SeedWork
{
    public abstract class EntityAudit : Entity
    {
        public DateTime Created { get; set; }
    }
}
