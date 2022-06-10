using System;

namespace MessagingService.Infrastructure.Token.Interfaces
{
    public interface ITokenFactory
    {
        (string, DateTime) GenerateToken(string id, string userName);
    }
}