using System.ComponentModel;

namespace API
{
    [TypeConverter(typeof(AirportCodesConverter))]
    public class AirportCodes
    {
        public string Iata { get; set; }
        public string Icao { get; set; }

        public static bool TryParse(string s, out AirportCodes result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }

            result = new AirportCodes() { Iata = parts[0], Icao = parts[1] };
            
            return true;
        }
    }

}
