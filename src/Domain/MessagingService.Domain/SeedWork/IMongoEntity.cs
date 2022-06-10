using System;

namespace MessagingService.Domain.SeedWork
{
    public interface IMongoEntity
    {
    }

    public interface IMongoEntity<out TKey> : IMongoEntity where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}


