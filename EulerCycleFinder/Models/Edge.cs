namespace EulerCycleFinder.Models;

public class Edge
{
    public int Source { get; }
    public int Destination { get; }
        
    public Edge(int source, int destination)
    {
        Source = source;
        Destination = destination;
    }

    public override bool Equals(object obj)
    {
        if (obj is not Edge other)
            return false;

        // Неориентированный граф: (1,2) = (2,1)
        return (Source == other.Source && Destination == other.Destination) ||
               (Source == other.Destination && Destination == other.Source);
    }

    public override int GetHashCode()
    {
        // Для неориентированного графа хеш должен быть одинаковым для (1,2) и (2,1)
        return Source.GetHashCode() ^ Destination.GetHashCode();
    }

    public override string ToString()
    {
        return $"({Source}, {Destination})";
    }
}