using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IGroundChange
    {
        IAirport Source { get; }
        IAirport Destination { get; }
        decimal DistanceInKm { get; }
    }
}
