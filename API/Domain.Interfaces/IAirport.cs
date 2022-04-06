namespace Domain.Interfaces
{
    public interface IAirport
    {
        int Id { get; }
        string Name { get; }
        string Country { get; }
        string City { get; }
        string Iata { get; }
        string Icao { get; }
        decimal Latitude { get; }
        decimal Longitude { get; }
        string ToString();
    }
}
