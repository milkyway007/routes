namespace Persistence
{
    public enum AirportProps
    {
        Id = 0,
        Name = 1,
        City = 2,
        Country = 3,
        Iata = 4,
        Icao = 5,
        Latitude = 6,
        Longitude = 7,
    }

    public enum RouteProps
    {
        SourceIata = 2,
        SourceId = 3,
        DestiationIata = 4,
        DestinationId = 5,
        StopCount = 7,
    }
}
