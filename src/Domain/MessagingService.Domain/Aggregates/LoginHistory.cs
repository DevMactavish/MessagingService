using System;
using MessagingService.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingService.Domain.Aggregates
{
    [BsonIgnoreExtraElements]
    public class LoginHistory : MongoDbEntity, IMongoEntity
    {
        private LoginHistory(string activity, string activityDescription, string username)
        {
            Activity = activity;
            ActivityDescription = activityDescription;
            UserName = username;
        }

        public static LoginHistory Create(string activity, string activityDescription, string userName)
        {
            return new LoginHistory(activity, activityDescription, userName);
        }
        public string Activity { get; set; }
        public string ActivityDescription { get; set; }
        public string UserName { get; set; }

        public LoginHistory ChangeUpdateTime(DateTime date)
        {
            UpdatedAt = date;
            return this;
        } 
        public LoginHistory ChanceCreateTime(DateTime date)
        {
            CreatedAt = date;
            return this;
        } 
    }
}
