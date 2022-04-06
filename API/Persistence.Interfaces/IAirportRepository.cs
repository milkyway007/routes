using Domain.Interfaces;

namespace Persistence.Interfaces
{
    public interface IAirportRepository
    {
        IQueryable<IAirport> GetAll();
        IAirport GetByCodes(string iata, string icao);
        IAirport GetById(int id);
        IAirport GetRouteDestination(IRoute route);
    }
}
