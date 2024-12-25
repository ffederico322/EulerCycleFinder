namespace EulerCycleFinder.Models;

public class Graph
    {
        private readonly Dictionary<int, HashSet<int>> _adjacencyList;
        public int GraphId { get; }
        
        public Graph(int graphId)
        {
            GraphId = graphId;
            _adjacencyList = new Dictionary<int, HashSet<int>>();
        }

        public void AddEdge(Edge edge)
        {
            if (!_adjacencyList.ContainsKey(edge.Source))
                _adjacencyList[edge.Source] = new HashSet<int>();
            
            if (!_adjacencyList.ContainsKey(edge.Destination))
                _adjacencyList[edge.Destination] = new HashSet<int>();

            _adjacencyList[edge.Source].Add(edge.Destination);
            _adjacencyList[edge.Destination].Add(edge.Source);
        }

        public void RemoveEdge(Edge edge)
        {
            if (_adjacencyList.ContainsKey(edge.Source) && _adjacencyList.ContainsKey(edge.Destination))
            {
                _adjacencyList[edge.Source].Remove(edge.Destination);
                _adjacencyList[edge.Destination].Remove(edge.Source);
            }
        }

        public HashSet<int> GetNeighbors(int vertex)
        {
            return _adjacencyList.ContainsKey(vertex) 
                ? new HashSet<int>(_adjacencyList[vertex]) 
                : new HashSet<int>();
        }

        public IReadOnlyDictionary<int, HashSet<int>> GetAdjacencyList()
        {
            return _adjacencyList;
        }

        public bool ContainsVertex(int vertex)
        {
            return _adjacencyList.ContainsKey(vertex);
        }

        public int VertexCount => _adjacencyList.Count;

        public int EdgeCount
        {
            get
            {
                int count = 0;
                foreach (var vertices in _adjacencyList.Values)
                {
                    count += vertices.Count;
                }
                return count / 2; // Делим на 2, так как каждое ребро учтено дважды
            }
        }

        public bool IsEmpty()
        {
            return _adjacencyList.Count == 0;
        }

        public override string ToString()
        {
            var edges = new List<string>();
            var processedVertices = new HashSet<int>();

            foreach (var vertex in _adjacencyList.Keys)
            {
                foreach (var neighbor in _adjacencyList[vertex])
                {
                    if (!processedVertices.Contains(neighbor))
                    {
                        edges.Add($"({vertex}, {neighbor})");
                    }
                }
                processedVertices.Add(vertex);
            }

            return $"Graph {GraphId}: {string.Join(", ", edges)}";
        }
    }