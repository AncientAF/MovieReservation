using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using FluentValidation;
using Serilog;
using Shared.Behaviors;
using Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database") ??
                       throw new ApplicationException("connection string can't be empty");
var elasticString = builder.Configuration.GetConnectionString("ElasticSearch");
var assembly = typeof(Program).Assembly;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Elasticsearch(new[] { new Uri(elasticString!) }, opts =>
    {
        opts.DataStream = new DataStreamName("logs", "movie-service", "development");
        opts.BootstrapMethod = BootstrapMethod.Failure;
        opts.ConfigureChannel = channelOpts =>
        {
            channelOpts.BufferOptions = new BufferOptions
            {
                ExportMaxConcurrency = 10
            };
        };
    })
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddSerilog();
builder.Host.UseSerilog();

//builder.Services.AddStackExchangeRedisCache(options => options.Configuration = redisString);
//builder.Services.AddScoped<ICacheService, DefaultCacheService>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    //config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
    //config.AddOpenBehavior(typeof(InvalidateCachePipelineBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

builder.Services.AddSingleton(new NpgsqlConnectionFactory(connectionString));

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var connectionFactory = scope.ServiceProvider.GetRequiredService<NpgsqlConnectionFactory>();

    await DbInitializer.InitializeDb(connectionFactory);
}

app.MapCarter();

app.UseExceptionHandler();

app.Run();