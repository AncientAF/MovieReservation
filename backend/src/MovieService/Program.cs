using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Serilog;
using Shared.Behaviors;
using Shared.Caching;
using Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Elasticsearch(new [] { new Uri("http://localhost:9200" )}, opts =>
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

var assembly = typeof(Program).Assembly;
var dbOptions = builder.Configuration.GetSection(nameof(MongoDbSettings));
var redisString = builder.Configuration.GetConnectionString("Cache");

builder.Services.AddSerilog();
builder.Host.UseSerilog();

builder.Services.Configure<MongoDbSettings>(dbOptions);
builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddStackExchangeRedisCache(options => options.Configuration = redisString);
builder.Services.AddScoped<ICacheService, DefaultCacheService>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
    config.AddOpenBehavior(typeof(InvalidateCachePipelineBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    var context = scope.ServiceProvider.GetRequiredService<MongoDbService>();

    await context.PopulateIfEmpty();
}

app.MapCarter();

app.UseExceptionHandler();

app.Run();