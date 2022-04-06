using Domain.Interfaces;

namespace Domain
{
    public class Route : IRoute
    {
        public int? SourceId { get; set; }

        public string SourceIata { get; set; }

        public int? DestinationId { get; set; }

        public string DestinationIata { get; set; }

        public int StopCount { get; set; }
    }
}
