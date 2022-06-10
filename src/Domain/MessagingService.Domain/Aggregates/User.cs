using System;
using MessagingService.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace MessagingService.Domain.Aggregates
{
    [BsonIgnoreExtraElements]
    public class User : MongoDbEntity, IMongoEntity
    {
        private User(string firstName, string lastName, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = userName;
            Password = password;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static User Create(string firstName,string lastName,string username, string password)
        {
            //Buraya Guard yazılabilir. Extra bir business olmadığından dolayı gerek yok.
            return new User(firstName, lastName, username, password);
        }
        
        public User ChangeUpdateTime(DateTime date)
        {
            UpdatedAt = date;
            return this;
        } 
        public User ChanceCreateTime(DateTime date)
        {
            CreatedAt = date;
            return this;
        } 
    }   
}