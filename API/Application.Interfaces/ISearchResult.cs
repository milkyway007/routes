namespace Application.Interfaces
{
    public interface ISearchResult
    {
        int LegCount { get; set; }
        IEnumerable<int> Path { get; set; }
        decimal DistanceInKm { get; set; }
        int GroundChangeSourceId { get; set; }
    }
}
