using CinemaService.Infrastructure.Data;
using CinemaService.Infrastructure.Data.Interceptors;
using Microsoft.Extensions.Configuration;

namespace CinemaService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connection);
        });

        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}