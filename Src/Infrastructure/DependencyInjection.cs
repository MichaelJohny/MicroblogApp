using Application.Common.Interfaces;
using Common;
using MicrblogApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicrblogApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
    
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        
        return services;
    }
}