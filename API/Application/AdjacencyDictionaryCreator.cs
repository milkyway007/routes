using Application.Interfaces;
using Domain.Interfaces;
using Persistence.Interfaces;

namespace Application
{
    public class AdjacencyDictionaryCreator : IGraphRepresentationCreator
    {
        private readonly object _lock = new object();
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly IList<IAirport> _airports;
        private readonly IList<IRoute> _routes;

        public AdjacencyDictionaryCreator(
            IDistanceCalculator distanceCalculator,
            IAirportRepository airportRepository,
            IRouteRepository reportRepository)
        {
            _distanceCalculator = distanceCalculator;
            _airports = airportRepository.GetAll().ToList();
            _routes = reportRepository.GetAll().ToList();
        }

        public IGraphRepresentation GraphRepresentation { get; private set; }

        public void Create()
        {
            var adjacencyList = new AdjacencyDictionary(_airports.Select(ap => ap.Id));
            _ = Parallel.ForEach(_airports, airport =>
                     {
                         var airportRoutes = _routes.Where(r =>
                         ((r.SourceId.HasValue && r.SourceId == airport.Id) ||
                         (!r.SourceId.HasValue && r.SourceIata == airport.Iata)) &&
                         r.StopCount == 0);

                         _ = Parallel.ForEach(airportRoutes, route =>
                         {
                             var destination = _airports.FirstOrDefault(a =>
                             (route.SourceId.HasValue && a.Id == route.DestinationId) ||
                             (!route.SourceId.HasValue && a.Iata == route.DestinationIata));
                             if (destination != null)
                             {
                                 var distance = _distanceCalculator.Calculate(airport, destination);
                                 lock (_lock)
                                 {
                                     adjacencyList.AddEdge(airport.Id, destination.Id, distance);
                                 }
                             }
                         });
                     });

            GraphRepresentation = adjacencyList;
        }
    }
}
