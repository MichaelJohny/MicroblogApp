using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicrblogApp.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlogAppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("BlogAppConnection"), ops =>
                {
                    ops.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                    ops.EnableRetryOnFailure(maxRetryCount: 6, maxRetryDelay: TimeSpan.FromSeconds(3), null);
                })
                .EnableSensitiveDataLogging());

        services.AddScoped<IBlogAppDbContext>(provider => provider.GetService<BlogAppDbContext>());
        
        return services;
    }
}