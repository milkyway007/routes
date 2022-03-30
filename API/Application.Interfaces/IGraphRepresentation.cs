namespace Application.Interfaces
{
    public interface IGraphRepresentation
    {
        int NodeCount { get; }
        void AddEdge(int sourceId, int destinationId, decimal distance);
        LinkedList<ValueTuple<int, decimal>> GetNodeEdges(int sourceId);
        IEnumerable<int> GetNodeIds();
    }
}
