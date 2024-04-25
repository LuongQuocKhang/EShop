using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using EShop.Catalog.API.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

Assembly assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

string connectionString = builder.Configuration.GetConnectionString("Catalog_Database")!;

bool useDocker = builder.Configuration.GetValue<bool>("UseDocker");

if (useDocker)
{
    connectionString = builder.Configuration.GetConnectionString("Catalog_Database_Docker")!;
}

builder.Services.AddMarten(config =>
{
    config.Connection(connectionString);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => {});

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
