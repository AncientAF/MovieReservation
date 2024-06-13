using CinemaService.API;
using CinemaService.Application;
using CinemaService.Infrastructure;
using CinemaService.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddApplicationServices(configuration)
    .AddInfrastructureServices(configuration)
    .AddApiServices(configuration);

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
    await app.InitialiseDatabaseAsync();

app.Run();