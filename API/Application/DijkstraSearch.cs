using Application.Interfaces;
using static Application.Constants;

namespace Application
{
    public class DijkstraSearch : ISearch
    {
        public ISearchResult Search(int sourceId, int destinationId, IGraphRepresentation graph)
        {
            if (sourceId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sourceId));
            }

            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            var nodeCount = graph.NodeCount;
            var distance = new Dictionary<int, decimal>();
            var pathLength = new Dictionary<int, int>();
            var parent = new Dictionary<int, int>();
            var groundChange = new Dictionary<int, int>();

            var heap = FibHeap.CreateHip();
            var nodes = new FibHeapNode[nodeCount];
            var i = 0;
            foreach (var id in graph.GetNodeIds())
            {
                distance[id] = decimal.MaxValue;
                pathLength[id] = 0;
                parent[id] = -1;
                groundChange[id] = -1;
                var node = new FibHeapNode
                {
                    AirportId = id,
                    Distance = distance[id],
                };
                nodes[i++] = node;
                heap.Insert(node);
            }

            distance[sourceId] = 0;
            heap.DecreaseKey(nodes.First(n => n.AirportId == sourceId), distance[sourceId]);

            while (!heap.IsEmpty())
            {
                var u = heap.ExtractMinimum();
                if (u.AirportId == int.MaxValue || u.AirportId == destinationId)
                {
                    break;
                }

                var edges = graph.GetNodeEdges(u.AirportId);
                var v = edges.First;
                while (v != null)
                {
                    int id = v.Value.Item1;
                    groundChange[id] = groundChange[u.AirportId];
                    var maxPathLength = groundChange[id] >= 0
                        ? MAX_PATH_LENGTH_WITH_GROUND_CHANGE
                        : MAX_PATH_LENGTH;
                    var hasAtMostFourLegs = pathLength[u.AirportId] + 1 <= maxPathLength;
                    if (!hasAtMostFourLegs)
                    {
                        v = v.Next;
                        continue;
                    }

                    decimal edgeDistace = v.Value.Item2;
                    var isNewPathShorter = pathLength[u.AirportId] + 1 < pathLength[id];

                    if (Convert.ToBoolean(edgeDistace) &&
                        distance[u.AirportId] != decimal.MaxValue &&
                        edgeDistace != decimal.MaxValue &&
                        (distance[u.AirportId] + edgeDistace < distance[id] ||
                        (distance[u.AirportId] + edgeDistace == distance[id] &&
                        isNewPathShorter)))
                    {
                        distance[id] = decimal.Add(distance[u.AirportId], edgeDistace);
                        parent[id] = u.AirportId;
                        pathLength[id] = pathLength[u.AirportId] + 1;
                        
                        if (groundChange[id] < 0 && edgeDistace <= 100)
                        {
                            groundChange[id] = u.AirportId;
                        }
                        
                        heap.DecreaseKey(nodes.First(n => n.AirportId == id),
                            distance[id]);
                    }

                    v = v.Next;
                }
            }

            return new SearchResult(
                pathLength[destinationId],
                GetPath(destinationId, parent),
                distance[destinationId],
                groundChange[destinationId]);
        }

        private IEnumerable<int> GetPath(
            int destinatioId,
            IDictionary<int,int> parent)
        {
            var path = new List<int>();
            path.Add(destinatioId);
            var u = parent[destinatioId];
            
            while (u >= 0)
            {
                path.Add(u);
                u = parent[u];
            }

            path.Reverse();

            return path;
        }
    }
}
