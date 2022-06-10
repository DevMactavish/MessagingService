using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingService.Domain.SeedWork
{
    public abstract class MongoDbEntity : IMongoEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement(Order = 101)]
        public DateTime CreatedAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement(Order = 101)]
        public DateTime UpdatedAt { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        [BsonElement(Order = 109)]
        public bool IsDeleted { get; set; }
    }
}