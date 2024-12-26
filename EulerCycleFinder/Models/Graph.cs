namespace EulerCycleFinder.Models
{
    // Класс, представляющий граф с использованием списка смежности
    public class Graph
    {
        // Словарь для хранения списка смежности, где ключ - вершина, а значение - набор соседей
        private readonly Dictionary<int, HashSet<int>> _adjacencyList;

        // Идентификатор графа
        public int GraphId { get; }

        // Конструктор, который инициализирует граф с заданным идентификатором
        public Graph(int graphId)
        {
            GraphId = graphId; // Устанавливаем идентификатор графа
            _adjacencyList = new Dictionary<int, HashSet<int>>(); // Инициализация пустого списка смежности
        }

        // Метод для добавления ребра в граф
        public void AddEdge(Edge edge)
        {
            // Если вершина источника не существует в списке смежности, создаем для нее новый набор соседей
            if (!_adjacencyList.ContainsKey(edge.Source))
                _adjacencyList[edge.Source] = new HashSet<int>();

            // Если вершина назначения не существует в списке смежности, создаем для нее новый набор соседей
            if (!_adjacencyList.ContainsKey(edge.Destination))
                _adjacencyList[edge.Destination] = new HashSet<int>();

            // Добавляем вершину назначения в список соседей для вершины источника
            _adjacencyList[edge.Source].Add(edge.Destination);
            // Добавляем вершину источника в список соседей для вершины назначения (граф неориентированный)
            _adjacencyList[edge.Destination].Add(edge.Source);
        }

        // Метод для удаления ребра из графа
        public void RemoveEdge(Edge edge)
        {
            // Проверяем, существуют ли вершины источника и назначения в списке смежности
            if (_adjacencyList.ContainsKey(edge.Source) && _adjacencyList.ContainsKey(edge.Destination))
            {
                // Удаляем вершину назначения из списка соседей для вершины источника
                _adjacencyList[edge.Source].Remove(edge.Destination);
                // Удаляем вершину источника из списка соседей для вершины назначения
                _adjacencyList[edge.Destination].Remove(edge.Source);
            }
        }

        // Метод для получения соседей заданной вершины
        public HashSet<int> GetNeighbors(int vertex)
        {
            // Возвращаем набор соседей для вершины, если она существует, иначе возвращаем пустой набор
            return _adjacencyList.ContainsKey(vertex) 
                ? new HashSet<int>(_adjacencyList[vertex]) 
                : new HashSet<int>();
        }

        // Метод для получения неизменяемого представления списка смежности
        public IReadOnlyDictionary<int, HashSet<int>> GetAdjacencyList()
        {
            return _adjacencyList; // Возвращаем неизменяемый список смежности
        }

        // Метод для проверки наличия вершины в графе
        public bool ContainsVertex(int vertex)
        {
            return _adjacencyList.ContainsKey(vertex); // Проверяем, содержится ли вершина в графе
        }

        // Свойство для получения количества вершин в графе
        public int VertexCount => _adjacencyList.Count;

        // Свойство для получения количества рёбер в графе
        public int EdgeCount
        {
            get
            {
                int count = 0;
                // Проходим по всем вершинам и считаем количество рёбер
                foreach (var vertices in _adjacencyList.Values)
                {
                    count += vertices.Count; // Для каждой вершины добавляем количество соседей
                }
                return count / 2; // Каждое ребро учтено дважды, так как граф неориентированный
            }
        }

        // Метод для проверки, является ли граф пустым
        public bool IsEmpty()
        {
            return _adjacencyList.Count == 0; // Проверяем, есть ли хотя бы одна вершина в графе
        }

        // Переопределение метода ToString для вывода графа в строковом формате
        public override string ToString()
        {
            var edges = new List<string>(); // Список для хранения строкового представления рёбер
            var processedVertices = new HashSet<int>(); // Множество для отслеживания обработанных рёбер

            // Проходим по всем вершинам графа
            foreach (var vertex in _adjacencyList.Keys)
            {
                // Проходим по всем соседям текущей вершины
                foreach (var neighbor in _adjacencyList[vertex])
                {
                    // Добавляем ребро в список, если оно еще не было обработано
                    if (!processedVertices.Contains(neighbor))
                    {
                        edges.Add($"({vertex}, {neighbor})");
                    }
                }
                // Добавляем текущую вершину в множество обработанных вершин
                processedVertices.Add(vertex);
            }

            // Возвращаем строковое представление графа с его рёбрами
            return $"Graph {GraphId}: {string.Join(", ", edges)}";
        }
    }
}
