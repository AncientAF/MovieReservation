using CinemaService.Core.Repositories;
using CinemaService.Infrastructure.Data;
using CinemaService.Infrastructure.Data.Interceptors;
using CinemaService.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Shared.Caching;

namespace CinemaService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database");
        var redisString = configuration.GetConnectionString("Cache");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connection);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<IHallRepository, HallRepository>();
        services.AddScoped<ICinemaRepository, CinemaRepository>();

        services.AddStackExchangeRedisCache(options => options.Configuration = redisString);
        services.AddScoped<ICacheService, DefaultCacheService>();

        return services;
    }
}