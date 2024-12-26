using EulerCycleFinder.Models;
using System.IO;

namespace EulerCycleFinder.Interfaces;

public interface IGraphService
{
    List<int> FindEulerianCycle(Graph graph);
    bool HasEulerianCycle(Graph graph);
    OutputData ProcessGraph(InputData input);
}