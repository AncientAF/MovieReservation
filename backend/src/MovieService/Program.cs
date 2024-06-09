using Serilog;
using Shared.Behaviors;
using Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

var assembly = typeof(Program).Assembly;
var dbOptions = builder.Configuration.GetSection(nameof(MongoDbSettings));

builder.Services.AddSerilog();

builder.Services.Configure<MongoDbSettings>(dbOptions);
builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
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