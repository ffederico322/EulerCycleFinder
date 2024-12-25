using EulerCycleFinder.Models;
using EulerCycleFinder.Interfaces;
using EulerCycleFinder.Services;
using System.IO;

namespace EulerCycleFinder.Services;

public class GraphService : IGraphService
{
    private readonly FleuryAlgorithm _fleuryAlgorithm;

    public GraphService()
    {
        _fleuryAlgorithm = new FleuryAlgorithm();
    }

    public List<int> FindEulerianCycle(Graph graph)
    {
        if (!HasEulerianCycle(graph))
        {
            throw new InvalidOperationException("Graph does not have an Eulerian cycle");
        }

        return _fleuryAlgorithm.FindEulerianCycle(graph);
    }

    public bool HasEulerianCycle(Graph graph)
    {
        if (graph.IsEmpty())
            return false;

        var adjacencyList = graph.GetAdjacencyList();
            
        // Проверяем, что все вершины имеют чётную степень
        foreach (var vertex in adjacencyList)
        {
            if (vertex.Value.Count % 2 != 0)
                return false;
        }

        return true;
    }
}