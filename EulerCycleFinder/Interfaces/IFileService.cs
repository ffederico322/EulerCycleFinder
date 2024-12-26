using EulerCycleFinder.Models;
using System.IO;

namespace EulerCycleFinder.Interfaces;

public interface IFileService
{
    Graph ReadGraphById(string filePath, int graphId);
    void SaveResults(string filePath, OutputData outputData);
}