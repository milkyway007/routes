using System.Globalization;

namespace Domain
{
    public class AirportBuilder
    {
        private readonly Airport _airport = new Airport();

        private AirportBuilder()
        {
        }

        public static AirportBuilder Init()
        {
            return new AirportBuilder();
        }

        public Airport Build() => _airport;

        public AirportBuilder SetId(string value)
        {
            if (!int.TryParse(
                value,
                out int parsed))
            {
                throw new InvalidOperationException();
            }

            if (parsed < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _airport.Id = parsed;

            return this;
        }

        public AirportBuilder SetCountry(string value)
        {
            _airport.Country = value;

            return this;
        }

        public AirportBuilder SetCity(string value)
        {
            _airport.City = value;

            return this;
        }

        public AirportBuilder SetIata(string value)
        {
            _airport.Iata = value;

            return this;
        }
        public AirportBuilder SetIcao(string value)
        {
            _airport.Icao = value;

            return this;
        }

        public AirportBuilder SetName(string value)
        {
            _airport.Name = value;

            return this;
        }

        public AirportBuilder SetLatitude(string value)
        {
            if (!decimal.TryParse(
                value,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal parsed))
            {
                throw new InvalidOperationException();
            }

            _airport.Latitude = parsed;

            return this;
        }

        public AirportBuilder SetLongitude(string value)
        {
            if (!decimal.TryParse(
                value,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal parsed))
            {
                throw new InvalidOperationException();
            }

            _airport.Longitude = parsed;

            return this;
        }
    }
}
