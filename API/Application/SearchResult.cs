using Application.Interfaces;

namespace Application
{
    public class SearchResult : ISearchResult
    {
        public SearchResult(
            int legCount,
            IEnumerable<int> path,
            decimal distanceInKm,
            int groundChangeSourceId)
        {
            LegCount = legCount;
            Path = path;
            DistanceInKm = distanceInKm;
            GroundChangeSourceId = groundChangeSourceId;
        }

        public int LegCount { get; set; } = -1;
        public IEnumerable<int> Path { get; set; }
        public decimal DistanceInKm { get; set; }
        public int GroundChangeSourceId { get; set; } = -1;
    }
}
