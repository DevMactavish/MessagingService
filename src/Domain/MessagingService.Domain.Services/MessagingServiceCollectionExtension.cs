using MediatR;
using MessagingService.Domain.Services.Implementations;
using MessagingService.Domain.Services.Interfaces;
using MessagingService.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingService.Domain.Services
{
    public static class MessagingServiceCollectionExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddInfrastructureServices();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            return services;
        }
    }
}