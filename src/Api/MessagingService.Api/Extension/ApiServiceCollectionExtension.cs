using System;
using MessagingService.Domain.Repositories.Interfaces;
using MessagingService.Infrastructure.Repositories;
using MessagingService.Infrastructure.Repositories.Mongo.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace MessagingService.Api.Extension
{
    public static class ApiServiceCollectionExtension
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IWriteMongoRepository<>), typeof(WriteMongoRepository<>));
            services.AddScoped(typeof(IReadMongoRepository<>), typeof(ReadMongoRepository<>));
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
                options.Database = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
            });
            return services;
        }

        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            var key = System.Text.Encoding.ASCII.GetBytes("MySecretKeyforapp12");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => { 
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }

        public static WebApplicationBuilder UseElasticWithSeriLog(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            string logConfig = configuration.GetSection("ElasticConfiguration").Value;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(logConfig))

                {
                    MinimumLogEventLevel = LogEventLevel.Information,
                    AutoRegisterTemplate = true,
                    IndexFormat = configuration.GetSection("ElasticSearchIndexName").Value + "-{0:yyyy.MM}"

                }).Destructure.AsScalar<byte[]>().CreateLogger();

            builder.Host.UseSerilog(Log.Logger);
            return builder;
        }
    }
}