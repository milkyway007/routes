using Application.Interfaces;
using Persistence.Interfaces;

namespace Application
{
    public class RouteFinder : IRouteFinder
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IGraphRepresentationCreator _graphRepresentationCreator;
        private readonly ISearch _search;
        private readonly IDistanceCalculator _distanceCalculator;

        public RouteFinder(
            IAirportRepository airportRepository,
            IGraphRepresentationCreator graphRepresentationCreator,
            ISearch search,
            IDistanceCalculator distanceCalculator)
        {
            _airportRepository = airportRepository;
            _graphRepresentationCreator = graphRepresentationCreator;
            _search = search;
            _distanceCalculator = distanceCalculator;
        }

        public IRouteResult Find(
            (string IATA, string ICAO) sourceInput,
            (string IATA, string ICAO) destinationInput)
        {
            var source = _airportRepository.GetByCodes(sourceInput.IATA, sourceInput.ICAO);
            var destination = _airportRepository.GetByCodes(destinationInput.IATA, destinationInput.ICAO);
            if (source == null || destination == null)
            {
                return RouteResultBuilder.Init()
                        .SetSorce(source)
                        .SetDestination(destination)
                        .Build();
            }

            var graph = _graphRepresentationCreator.GraphRepresentation;

            var searchResult = _search.Search(source.Id, destination.Id, graph);
            
            var builder = RouteResultBuilder.Init()
                .SetSorce(source)
                .SetDestination(destination)
                .SetLegCount(searchResult.LegCount)
                .SetPath(searchResult.Path)
                .SetDistanceInKm(searchResult.DistanceInKm);

            if (searchResult.GroundChangeSourceId >= 0)
            {
                var pathAsList = searchResult.Path.ToList();
                var groundChangeSource = _airportRepository.GetById(searchResult.GroundChangeSourceId);
                var groundChangeDestinationId =
                    pathAsList[pathAsList.IndexOf(searchResult.GroundChangeSourceId) + 1];
                var groundChangeDestination = _airportRepository.GetById(groundChangeDestinationId);
                var groundChangeDistance = _distanceCalculator
                    .Calculate(groundChangeSource, groundChangeDestination);

                builder.SetGroundChange(
                new GroundChange(groundChangeSource, groundChangeDestination, groundChangeDistance));
            }
                      
            return builder.Build();
        }
    }
}
