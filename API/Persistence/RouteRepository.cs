using Domain.Interfaces;
using Persistence.Interfaces;

namespace Persistence
{
    public class RouteRepository : IRouteRepository
    {
        private readonly IQueryable<IRoute> _entities;

        public RouteRepository(IDataProvider provider)
        {
            _entities = provider.GetRoutes();
        }

        public IQueryable<IRoute> GetAll()
        {
            return _entities;
        }

        public IQueryable<IRoute> GetByAirport(IAirport airport)
        {
            return _entities.Where(r => ((r.SourceId.HasValue && r.SourceId == airport.Id) ||
                                        (!r.SourceId.HasValue && r.SourceIata == airport.Iata)) &&
                                        r.StopCount == 0);
        }
    }
}
