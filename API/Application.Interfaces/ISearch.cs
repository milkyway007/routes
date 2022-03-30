namespace Application.Interfaces
{
    public interface ISearch
    {
        ISearchResult Search(int sourceId, int destinationId, IGraphRepresentation graph);
    }
}
