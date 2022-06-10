using System;
using MessagingService.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingService.Domain.Aggregates
{
    [BsonIgnoreExtraElements]
    public class Message:MongoDbEntity, IMongoEntity
    {
        private Message(string messageDetail, string userFrom, string userTo)
        {
            MessageDetail = messageDetail;
            UserFrom = userFrom;
            UserTo = userTo;
            SendedAt = DateTime.UtcNow;
        }
        public string MessageDetail { get; set; }
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
        public DateTime SendedAt { get; set; }

        public static Message Create(string messageDetail, string userFrom, string userTo)
        {
            return new Message(messageDetail, userFrom, userTo);
        }
        
        public Message ChangeUpdateTime(DateTime date)
        {
            UpdatedAt = date;
            return this;
        } 
        public Message ChanceCreateTime(DateTime date)
        {
            CreatedAt = date;
            return this;
        } 
    }   
}