using EulerCycleFinder.Models;
using EulerCycleFinder.Interfaces;
using EulerCycleFinder.Services;
using System.IO;

public class FleuryAlgorithm
{
    public List<int> FindEulerianCycle(Graph graph)
    {
        var cycle = new List<int>();
        var workingGraph = CloneGraph(graph);
        var currentVertex = workingGraph.GetAdjacencyList().Keys.First();

        while (true)
        {
            var neighbors = workingGraph.GetNeighbors(currentVertex);
            if (!neighbors.Any())
                break;

            int nextVertex = -1;
            foreach (var neighbor in neighbors)
            {
                if (!IsBridge(workingGraph, currentVertex, neighbor))
                {
                    nextVertex = neighbor;
                    break;
                }
            }

            if (nextVertex == -1)
                nextVertex = neighbors.First();

            cycle.Add(currentVertex);
            workingGraph.RemoveEdge(new Edge(currentVertex, nextVertex));
            currentVertex = nextVertex;
        }

        cycle.Add(currentVertex);
        return cycle;
    }

    private bool IsBridge(Graph graph, int source, int destination)
    {
        if (graph.GetNeighbors(source).Count() == 1)
            return false;

        var edgeCount = CountReachableEdges(graph, source);
        graph.RemoveEdge(new Edge(source, destination));
        var newEdgeCount = CountReachableEdges(graph, source);
        graph.AddEdge(new Edge(source, destination));

        return newEdgeCount < edgeCount;
    }

    private int CountReachableEdges(Graph graph, int startVertex)
    {
        var visited = new HashSet<int>();
        var stack = new Stack<int>();
        stack.Push(startVertex);
        visited.Add(startVertex);
        int edges = 0;

        while (stack.Count > 0)
        {
            var vertex = stack.Pop();
            foreach (var neighbor in graph.GetNeighbors(vertex))
            {
                edges++;
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    stack.Push(neighbor);
                }
            }
        }

        return edges / 2;
    }

    private Graph CloneGraph(Graph original)
    {
        var clone = new Graph(original.GraphId);
        foreach (var kvp in original.GetAdjacencyList())
        {
            foreach (var neighbor in kvp.Value)
            {
                if (kvp.Key < neighbor) // Избегаем дублирования рёбер
                {
                    clone.AddEdge(new Edge(kvp.Key, neighbor));
                }
            }
        }
        return clone;
    }
}