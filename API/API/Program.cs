using Application;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence;
using Persistence.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices(services =>
{
    services.TryAddSingleton<IDistanceCalculator, LatitudeLongitudeDistanceCalculator>();
    services.TryAddSingleton<IGraphRepresentationCreator, AdjacencyDictionaryCreator>();
    services.TryAddSingleton<ISearch, DijkstraSearch>();
    services.TryAddSingleton<IRouteFinder, RouteFinder>();
    services.TryAddSingleton<IFileReader, FileReader>();
    services.TryAddSingleton<IDataProvider, CsvParser>();
    services.TryAddSingleton<IAirportRepository, AirportRepository>();
    services.TryAddSingleton<IRouteRepository, RouteRepository>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var graphRepresentationCreator = app.Services.GetRequiredService<IGraphRepresentationCreator>();
graphRepresentationCreator.Create();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
