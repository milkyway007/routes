using Application.Interfaces;
using Domain.Interfaces;

namespace Application
{
    public class GroundChange : IGroundChange
    {
        public GroundChange(IAirport source, IAirport destination, decimal distanceInKm)
        {
            Source = source;
            Destination = destination;
            DistanceInKm = distanceInKm;
        }

        public IAirport Source { get; }
        public IAirport Destination { get; }
        public decimal DistanceInKm { get; }
    }
}
