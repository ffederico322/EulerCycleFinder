using EulerCycleFinder.Models;
using System.IO;

namespace EulerCycleFinder.Interfaces;

public interface IFileService
{
    List<Graph> ReadGraphsFromFile(string filePath);
    Graph ReadGraphById(string filePath, int graphId);
}