using EulerCycleFinder.Models;
using EulerCycleFinder.Interfaces;
using System.IO;

namespace EulerCycleFinder.Services;

public class FileService : IFileService
{
    public List<Graph> ReadGraphsFromFile(string filePath)
    {
        var graphs = new List<Graph>();
        
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            int currentLine = 0;

            while (currentLine < lines.Length)
            {
                if (string.IsNullOrWhiteSpace(lines[currentLine]) || lines[currentLine].StartsWith("#"))
                {
                    currentLine++;
                    continue;
                }

                if (int.TryParse(lines[currentLine], out int graphId))
                {
                    var graph = new Graph(graphId);
                    currentLine++;

                    if (currentLine < lines.Length && int.TryParse(lines[currentLine], out int edgeCount))
                    {
                        currentLine++;
                        
                        for (int i = 0; i < edgeCount && currentLine < lines.Length; i++)
                        {
                            string[] vertices = lines[currentLine].Split(' ');
                            if (vertices.Length == 2 && 
                                int.TryParse(vertices[0], out int v1) && 
                                int.TryParse(vertices[1], out int v2))
                            {
                                graph.AddEdge(new Edge(v1, v2));
                            }
                            currentLine++;
                        }
                        graphs.Add(graph);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading graphs from file: {ex.Message}");
        }

        return graphs;
    }

    public Graph ReadGraphById(string filePath, int targetGraphId)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            int currentLine = 0;

            while (currentLine < lines.Length)
            {
                if (string.IsNullOrWhiteSpace(lines[currentLine]) || lines[currentLine].StartsWith("#"))
                {
                    currentLine++;
                    continue;
                }

                if (int.TryParse(lines[currentLine], out int graphId) && graphId == targetGraphId)
                {
                    var graph = new Graph(graphId);
                    currentLine++;

                    if (currentLine < lines.Length && int.TryParse(lines[currentLine], out int edgeCount))
                    {
                        currentLine++;
                        
                        for (int i = 0; i < edgeCount && currentLine < lines.Length; i++)
                        {
                            string[] vertices = lines[currentLine].Split(' ');
                            if (vertices.Length == 2 && 
                                int.TryParse(vertices[0], out int v1) && 
                                int.TryParse(vertices[1], out int v2))
                            {
                                graph.AddEdge(new Edge(v1, v2));
                            }
                            currentLine++;
                        }
                        return graph;
                    }
                }
                currentLine++;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading graph {targetGraphId}: {ex.Message}");
        }

        throw new Exception($"Graph with ID {targetGraphId} not found");
    }
}
    
