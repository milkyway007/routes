using Application.Interfaces;
using Domain.Interfaces;

namespace Application
{
    public class RouteResultBuilder
    {
        private readonly RouteResult _result = new RouteResult();

        private RouteResultBuilder()
        {
        }

        public static RouteResultBuilder Init()
        {
            return new RouteResultBuilder();
        }

        public RouteResult Build() => _result;

        public RouteResultBuilder SetSorce(IAirport value)
        {
            _result.Source = value;

            return this;
        }

        public RouteResultBuilder SetDestination(IAirport value)
        {
            _result.Destination = value;

            return this;
        }

        public RouteResultBuilder SetLegCount(int value)
        {
            _result.LegCount = value;

            return this;
        }

        public RouteResultBuilder SetPath(IEnumerable<int> value)
        {
            _result.Path = value;

            return this;
        }
        public RouteResultBuilder SetDistanceInKm(decimal value)
        {
            _result.DistanceInKm = value;

            return this;
        }

        public RouteResultBuilder SetGroundChange(IGroundChange value)
        {
            _result.GroundChange = value;

            return this;
        }
    }
}
