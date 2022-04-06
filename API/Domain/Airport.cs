using Domain.Interfaces;

namespace Domain
{
    public class Airport : IAirport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public override string ToString()
        {
            return $"Id: {Id};\ncountry: {Country};\ncity: {City};\n" +
                $"IATA: {Iata};\nICAO: {Icao};\nlat.: {Latitude};\nlong.: {Longitude}.";
        }
    }
}
