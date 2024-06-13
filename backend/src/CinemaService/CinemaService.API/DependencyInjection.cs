using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Serilog;
using Shared.Exceptions.Handler;

namespace CinemaService.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var elasticString = configuration.GetConnectionString("ElasticSearch");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Elasticsearch(new[] { new Uri(elasticString!) }, opts =>
            {
                opts.DataStream = new DataStreamName("logs", "cinema-service", "development");
                opts.BootstrapMethod = BootstrapMethod.Failure;
                opts.ConfigureChannel = channelOpts =>
                {
                    channelOpts.BufferOptions = new BufferOptions
                    {
                        ExportMaxConcurrency = 10
                    };
                };
            })
            .ReadFrom.Configuration(configuration).CreateLogger();

        services.AddSerilog();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCarter();

        services.AddProblemDetails();
        services.AddExceptionHandler<ExceptionHandler>();

        //services.AddHealthChecks()
        //.AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapCarter();

        app.UseExceptionHandler();

        /*app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });*/

        return app;
    }
}