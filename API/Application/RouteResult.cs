using Application.Interfaces;
using Domain.Interfaces;

namespace Application
{
    public class RouteResult : IRouteResult
    {
        public IAirport Source { get; set; }
        public IAirport Destination { get; set; }
        public int LegCount { get; set; } = -1;
        public IEnumerable<int> Path { get; set; }
        public decimal DistanceInKm { get; set; }
        public IGroundChange GroundChange { get; set; }
    }
}
