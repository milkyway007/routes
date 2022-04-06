// See https://aka.ms/new-console-template for more information
using Application;
using Persistence;

var fileReader = new FileReader();
var dataProvider = new CsvParser(fileReader);

var distanceCalculator = new LatitudeLongitudeDistanceCalculator();
var airportRepository = new AirportRepository(dataProvider);
var routeRepository = new RouteRepository(dataProvider);

var graphRepresentationCreator = new AdjacencyDictionaryCreator(
    distanceCalculator, airportRepository, routeRepository);
var search = new DijkstraSearch();

var routeFinder = new RouteFinder(
    airportRepository, graphRepresentationCreator, search, distanceCalculator);

var result = routeFinder.Find(("AER", "URSS"), ("MRV", "URMM"));

Console.WriteLine(result.DistanceInKm);
Console.WriteLine(result.Path.Count());
foreach (var id in result.Path)
{
    Console.WriteLine(id);
}

Console.ReadKey();

