using Domain.Interfaces;

namespace Persistence.Interfaces
{
    public interface IRouteRepository
    {
        IQueryable<IRoute> GetAll();
        IQueryable<IRoute> GetByAirport(IAirport airport);
    }
}
