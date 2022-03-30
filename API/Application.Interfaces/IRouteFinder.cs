namespace Application.Interfaces
{
    public interface IRouteFinder
    {
        IRouteResult Find(
            (string IATA, string ICAO) sourceInput,
            (string IATA, string ICAO) destinationInput);
    }
}
