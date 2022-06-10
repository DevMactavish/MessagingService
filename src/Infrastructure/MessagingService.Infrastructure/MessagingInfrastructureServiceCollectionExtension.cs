using MessagingService.Domain.Repositories.Interfaces;
using MessagingService.Infrastructure.Repositories.Mongo.Implementations;
using MessagingService.Infrastructure.Token.Implementations;
using MessagingService.Infrastructure.Token.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingService.Infrastructure
{
    public static class MessagingInfrastructureServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
            {
                services.AddScoped<IWriteUserRepository, WriteUserRepository>();
                services.AddScoped<IWriteLoginHistoryRepository, WriteLoginHistoryRepository>();
                services.AddScoped<IWriteMessageRepository, WriteMessageRepository>();
                services.AddScoped<IReadUserRepository, ReadUserRepository>();
                services.AddScoped<IReadMessageRepository, ReadMessageRepository>();
                services.AddScoped<ITokenFactory, TokenFactory>();
               
                return services;
            }
        
    }
}