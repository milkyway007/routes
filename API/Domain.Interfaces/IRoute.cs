namespace Domain.Interfaces
{
    public interface IRoute
    {
        int? SourceId { get; }

        string SourceIata { get; }

        int? DestinationId { get; }

        string DestinationIata { get; }

        int StopCount { get; }
    }
}
