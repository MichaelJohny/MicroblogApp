using Api.Common;
using Application.Common.Interfaces;
using MicrblogApp.Infrastructure.Services;
using StackExchange.Redis;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddRedis(this IServiceCollection services , IConfiguration configuration)
    {
        var redisConfig = ConfigurationOptions.Parse(configuration.GetRequiredConnectionString("redis"), true);
        var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConfig);
        services.AddSingleton(sp => { return connectionMultiplexer; });
    
        services.AddSingleton<ICacheService, CacheService>();
        return services;
    }
}