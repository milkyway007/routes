using Domain.Interfaces;

namespace Persistence.Interfaces
{
    public interface IDataProvider
    {
        IQueryable<IAirport> GetAirports();
        IQueryable<IRoute> GetRoutes();
    }
}
