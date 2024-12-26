using EulerCycleFinder.Models;
using EulerCycleFinder.Interfaces;
using EulerCycleFinder.Services;
using System.IO;

public class FleuryAlgorithm
{
    // Метод для поиска Эйлерова цикла в графе
    public List<int> FindEulerianCycle(Graph graph)
    {
        var cycle = new List<int>(); // Список для хранения найденного Эйлерова цикла
        var workingGraph = CloneGraph(graph); // Создаем копию графа для работы
        var currentVertex = workingGraph.GetAdjacencyList().Keys.First(); // Начинаем с первой вершины

        // Пока есть соседи у текущей вершины, продолжаем искать цикл
        while (true)
        {
            var neighbors = workingGraph.GetNeighbors(currentVertex);
            if (!neighbors.Any()) // Если у вершины нет соседей, завершаем цикл
                break;

            int nextVertex = -1;
            // Ищем соседей, которые не являются мостами (их удаление не разделит граф)
            foreach (var neighbor in neighbors)
            {
                if (!IsBridge(workingGraph, currentVertex, neighbor))
                {
                    nextVertex = neighbor;
                    break;
                }
            }

            // Если все соседи являются мостами, просто выбираем первого соседа
            if (nextVertex == -1)
                nextVertex = neighbors.First();

            // Добавляем текущую вершину в цикл и удаляем ребро между текущей и следующей вершинами
            cycle.Add(currentVertex);
            workingGraph.RemoveEdge(new Edge(currentVertex, nextVertex));
            currentVertex = nextVertex; // Переходим к следующей вершине
        }

        // Добавляем последнюю вершину в цикл
        cycle.Add(currentVertex);
        return cycle;
    }

    // Метод для проверки, является ли ребро мостом
    private bool IsBridge(Graph graph, int source, int destination)
    {
        // Если у вершины только одно соседство, то это не мост
        if (graph.GetNeighbors(source).Count() == 1)
            return false;

        // Подсчитываем количество рёбер, достижимых из исходной вершины
        var edgeCount = CountReachableEdges(graph, source);
        // Удаляем ребро и проверяем, уменьшилось ли количество достижимых рёбер
        graph.RemoveEdge(new Edge(source, destination));
        var newEdgeCount = CountReachableEdges(graph, source);
        // Восстанавливаем ребро
        graph.AddEdge(new Edge(source, destination));

        // Если количество рёбер уменьшилось, значит это мост
        return newEdgeCount < edgeCount;
    }

    // Метод для подсчета количества достижимых рёбер из вершины
    private int CountReachableEdges(Graph graph, int startVertex)
    {
        var visited = new HashSet<int>(); // Множество посещённых вершин
        var stack = new Stack<int>(); // Стек для обхода графа в глубину
        stack.Push(startVertex);
        visited.Add(startVertex);
        int edges = 0;

        // Обход графа с помощью стека
        while (stack.Count > 0)
        {
            var vertex = stack.Pop();
            // Обходим всех соседей текущей вершины
            foreach (var neighbor in graph.GetNeighbors(vertex))
            {
                edges++; // Увеличиваем счётчик рёбер
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    stack.Push(neighbor); // Добавляем соседа в стек
                }
            }
        }

        // Возвращаем количество рёбер, делённое на 2 (так как граф неориентированный и рёбра считаются дважды)
        return edges / 2;
    }

    // Метод для клонирования графа
    private Graph CloneGraph(Graph original)
    {
        var clone = new Graph(original.GraphId); // Создаём новый граф с тем же идентификатором
        foreach (var kvp in original.GetAdjacencyList())
        {
            // Добавляем все рёбра, избегая дублирования (чтобы добавить ребро только один раз)
            foreach (var neighbor in kvp.Value)
            {
                if (kvp.Key < neighbor) // Условие избегания дублирования рёбер
                {
                    clone.AddEdge(new Edge(kvp.Key, neighbor));
                }
            }
        }
        return clone; // Возвращаем клонированный граф
    }
}
