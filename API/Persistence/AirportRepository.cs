using Domain.Interfaces;
using Persistence.Interfaces;

namespace Persistence
{
    public class AirportRepository : IAirportRepository
    {
        private readonly IQueryable<IAirport> _entities;

        public AirportRepository(IDataProvider provider)
        {
            _entities = provider.GetAirports();
        }

        public IQueryable<IAirport> GetAll()
        {
            return _entities;
        }

        public IAirport GetByCodes(string iata, string icao)
        {
            return _entities.FirstOrDefault(a => (string.IsNullOrWhiteSpace(a.Iata) || a.Iata == iata) &&
               (string.IsNullOrWhiteSpace(a.Icao) || a.Icao == icao));
        }

        public IAirport GetById(int id)
        {
            return _entities.FirstOrDefault(a => a.Id == id);
        }

        public IAirport GetRouteDestination(IRoute route)
        {
            return _entities.FirstOrDefault(a => (route.SourceId.HasValue && a.Id == route.DestinationId) ||
                             (!route.SourceId.HasValue && a.Iata == route.DestinationIata));
        }
    }
}
