using static DecimalMath.DecimalEx;
using static System.Math;
using static Application.Constants;
using Domain.Interfaces;
using Application.Interfaces;

namespace Application
{
    public class LatitudeLongitudeDistanceCalculator : IDistanceCalculator
    {
        public decimal Calculate(IAirport source, IAirport destiation)
        {
            if (destiation == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destiation == null)
            {
                throw new ArgumentNullException(nameof(destiation));
            }

            if (source.Latitude == destiation.Latitude &&
                source.Longitude == destiation.Longitude)
            {
                return 0;
            }

            (decimal latitude, decimal longitude) coordinates1 =
                new(ConvertToRadians(source.Latitude), ConvertToRadians(source.Longitude));
            (decimal latitude, decimal longitude) coordinates2 =
                new(ConvertToRadians(destiation.Latitude), ConvertToRadians(destiation.Longitude));

            var longitudeDiff = coordinates2.longitude - coordinates1.longitude;
            var latitudeDiff = coordinates2.latitude - coordinates1.latitude;
            var a = Pow(Sin(latitudeDiff / 2), 2) +
                              Cos(coordinates1.latitude) *
                              Cos(coordinates2.latitude) *
                              Pow(Sin(longitudeDiff / 2), 2);
            var c = 2 * ATan2(Sqrt(a), Sqrt(1 - a));

            return EARTH_RADIUS_KM * c;
        }

        private static decimal ConvertToRadians(decimal decimalDegees)
        {
            return decimal.Divide(decimal.Multiply(decimalDegees, (decimal)PI), DEGREES_IN_PI_RAD);
        }
    }
}
