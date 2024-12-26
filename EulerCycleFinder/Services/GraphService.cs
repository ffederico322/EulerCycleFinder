using EulerCycleFinder.Models;
using EulerCycleFinder.Interfaces;
using EulerCycleFinder.Services;
using System.IO;

namespace EulerCycleFinder.Services;

public class GraphService : IGraphService
{
    // Алгоритм Флёри для поиска Эйлерова цикла
    private readonly FleuryAlgorithm _fleuryAlgorithm;
    
    // Сервис для работы с файлами
    private readonly IFileService _fileService;

    // Конструктор, который инициализирует необходимые сервисы
    public GraphService(IFileService fileService) // Добавляем fileService в конструктор
    {
        _fleuryAlgorithm = new FleuryAlgorithm(); // Инициализация алгоритма Флёри
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService)); // Инициализация fileService
    }

    // Метод для нахождения Эйлерова цикла в графе
    public List<int> FindEulerianCycle(Graph graph)
    {
        // Проверка наличия Эйлерова цикла в графе
        if (!HasEulerianCycle(graph))
        {
            // Если Эйлеров цикл не существует, выбрасываем исключение
            throw new InvalidOperationException("Граф не содержит Эйлеров цикл");
        }

        // Если цикл существует, находим его с помощью алгоритма Флёри
        return _fleuryAlgorithm.FindEulerianCycle(graph);
    }

    // Метод для проверки наличия Эйлерова цикла в графе
    public bool HasEulerianCycle(Graph graph)
    {
        // Если граф пуст, то Эйлеров цикл невозможен
        if (graph.IsEmpty())
            return false;

        // Получаем список смежности графа
        var adjacencyList = graph.GetAdjacencyList();
            
        // Проверяем, что все вершины имеют чётную степень
        foreach (var vertex in adjacencyList)
        {
            // Если у вершины нечётная степень, то в графе нет Эйлерова цикла
            if (vertex.Value.Count % 2 != 0)
                return false;
        }

        // Если все вершины имеют чётную степень, возвращаем true
        return true;
    }
    
    // Метод для обработки графа, нахождения цикла и возвращения результатов
    public OutputData ProcessGraph(InputData input)
    {
        // Записываем время начала обработки
        var startTime = DateTime.Now;

        // Считываем граф из файла с помощью FileService
        var graph = _fileService.ReadGraphById(input.FilePath, input.GraphId);
    
        // Проверяем, имеет ли граф Эйлеров цикл
        var hasEulerianCycle = HasEulerianCycle(graph);
        
        // Если цикл есть, находим его, иначе возвращаем пустой список
        var cycle = hasEulerianCycle ? FindEulerianCycle(graph) : new List<int>();
    
        // Записываем время завершения обработки
        var executionTime = DateTime.Now - startTime;
    
        // Создаём и возвращаем объект OutputData с результатами
        return new OutputData(
            input.GraphId, // Идентификатор графа
            cycle, // Список вершин Эйлерова цикла
            hasEulerianCycle, // Есть ли Эйлеров цикл
            input.ProcessingTime, // Время обработки на входе
            executionTime // Время выполнения алгоритма
        );
    }
}
