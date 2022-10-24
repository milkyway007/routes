using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IRouteResult
    {
        IAirport Source { get; }
        IAirport Destination { get; }
        int LegCount { get; }
        IEnumerable<int> Path { get; }
        decimal DistanceInKm { get; }
        IGroundChange GroundChange { get; }
    }
}
