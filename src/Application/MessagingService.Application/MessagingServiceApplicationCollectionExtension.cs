using System.Collections.Generic;
using MessagingService.Application.UseCases.CreateUser;
using MessagingService.Application.UseCases.CreateUser.Dtos;
using MessagingService.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingService.Application
{
    public static class MessagingServiceApplicationCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainServices(configuration);
            
            return services;
        }
    }
}