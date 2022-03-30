using Domain;
using Domain.Interfaces;
using Persistence.Interfaces;
using System.Text.RegularExpressions;
using static Persistence.Constants;

namespace Persistence
{
    public class CsvParser : IDataProvider
    {
        private readonly IFileReader _fileReader;
        
        public CsvParser(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public IQueryable<IAirport> GetAirports()
        {
            var lines = _fileReader.Read(AIRPORT_PATH);

            return lines
                .Skip(1)
                .Select(line => ParseAirport(line))
                .DistinctBy(a => new { a.Iata, a.Icao, a.Latitude, a.Longitude })
                .AsQueryable<IAirport>();
        }

        public IQueryable<IRoute> GetRoutes()
        {
            var lines = _fileReader.Read(ROUTE_PATH);

            return lines
                .Skip(1)
                .Select(line => ParseRoute(line))
                .DistinctBy(r =>
                new { r.SourceId, r.SourceIata, r.DestinationId, r.DestinationIata, r.StopCount })
                .AsQueryable();
        }

        private static IAirport ParseAirport(string csvLine)
        {
            var values = Regex.Split(csvLine, DELIMETER);

            var builder = AirportBuilder.Init();
            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i].Trim();
                if (i == (int) AirportProps.Id)
                {
                    builder.SetId(value);
                }
                if (i == (int)AirportProps.Name)
                {
                    builder.SetName(value);
                }
                if (i == (int)AirportProps.City)
                {
                    builder.SetCity(value);
                }
                if (i == (int)AirportProps.Country)
                {
                    builder.SetCountry(value);
                }
                if (i == (int)AirportProps.Iata)
                {
                    builder.SetIata(value);
                }
                if (i == (int)AirportProps.Icao)
                {
                    builder.SetIcao(value);
                }
                if (i == (int)AirportProps.Latitude)
                {
                    builder.SetLatitude(value);
                }
                if (i == (int)AirportProps.Longitude)
                {
                    builder.SetLongitude(value);
                }
            }

            return builder.Build();
        }

        private static IRoute ParseRoute(string csvLine)
        {
            var values = Regex.Split(csvLine, DELIMETER);

            var builder = RouteBuilder.Init();
            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i].Trim();
                if (i == (int)RouteProps.SourceIata)
                {
                    builder.SetSourceIata(value);
                }
                if (i == (int)RouteProps.SourceId)
                {
                    builder.SetSourceId(value);
                }
                if (i == (int)RouteProps.DestiationIata)
                {
                    builder.SetDestinationIata(value);
                }
                if (i == (int)RouteProps.DestinationId)
                {
                    builder.SetDestinationId(value);
                }
                if (i == (int)RouteProps.StopCount)
                {
                    builder.SetStopCount(value);
                }
            }

            return builder.Build();
        }
    }
}
