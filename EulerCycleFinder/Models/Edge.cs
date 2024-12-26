namespace EulerCycleFinder.Models;

public class Edge
{
    // Свойства для хранения вершин, соединенных ребром
    public int Source { get; }  // Исходная вершина
    public int Destination { get; }  // Конечная вершина
        
    // Конструктор, инициализирующий ребро с заданными вершинами
    public Edge(int source, int destination)
    {
        Source = source;  // Инициализация исходной вершины
        Destination = destination;  // Инициализация конечной вершины
    }

    // Переопределение метода Equals для сравнения двух рёбер
    public override bool Equals(object obj)
    {
        // Проверяем, является ли объект другим ребром
        if (obj is not Edge other)
            return false;

        // Для неориентированного графа (1,2) и (2,1) считаются одинаковыми
        return (Source == other.Source && Destination == other.Destination) ||
               (Source == other.Destination && Destination == other.Source);
    }

    // Переопределение метода GetHashCode для корректной работы коллекций, например, HashSet
    public override int GetHashCode()
    {
        // Для неориентированного графа хеш должен быть одинаковым для (1,2) и (2,1)
        return Source.GetHashCode() ^ Destination.GetHashCode();
    }

    // Переопределение метода ToString для строкового представления ребра
    public override string ToString()
    {
        // Возвращает строку в виде (Source, Destination)
        return $"({Source}, {Destination})";
    }
}
