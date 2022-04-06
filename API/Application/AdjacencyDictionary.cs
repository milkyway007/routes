using Application.Interfaces;

namespace Application
{
    public class AdjacencyDictionary : IGraphRepresentation
    {
        private readonly IDictionary<int, LinkedList<ValueTuple<int, decimal>>> _container;

        public AdjacencyDictionary(IEnumerable<int> airportIds)
        {
            if (airportIds == null)
            {
                throw new ArgumentNullException(nameof(airportIds));
            }

            NodeCount = airportIds.Count();
            _container = new Dictionary<int, LinkedList<ValueTuple<int, decimal>>>();

            foreach (var id in airportIds)
            {
                _container[id] = new LinkedList<ValueTuple<int, decimal>>();
            }
        }

        public int NodeCount { get; }

        public void AddEdge(int sourceId, int destinationId, decimal distance)
        {
            if (sourceId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sourceId));
            }
            if (destinationId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(destinationId));
            }
            if (distance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(distance));
            }

            _container[sourceId].AddLast((destinationId, distance));
        }

        public LinkedList<ValueTuple<int, decimal>> GetNodeEdges(int sourceId)
        {
            if (sourceId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sourceId));
            }

            return _container[sourceId];
        }

        public IEnumerable<int> GetNodeIds()
        {
            return _container.Keys;
        }
    }
}
