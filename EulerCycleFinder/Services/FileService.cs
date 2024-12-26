using EulerCycleFinder.Models;
using EulerCycleFinder.Interfaces;
using System.IO;
using System.Text;

namespace EulerCycleFinder.Services
{
    // Класс, отвечающий за чтение графов из файла и сохранение результатов
    public class FileService : IFileService
    {
        // Метод для чтения всех графов из файла
        public List<Graph> ReadGraphsFromFile(string filePath)
        {
            var graphs = new List<Graph>();

            try
            {
                // Чтение всех строк из файла
                string[] lines = File.ReadAllLines(filePath);
                int currentLine = 0;

                // Проход по строкам файла
                while (currentLine < lines.Length)
                {
                    // Пропускаем пустые строки и строки, начинающиеся с комментариев
                    if (string.IsNullOrWhiteSpace(lines[currentLine]) || lines[currentLine].StartsWith("#"))
                    {
                        currentLine++;
                        continue;
                    }

                    // Если строка содержит идентификатор графа, начинаем читать новый граф
                    if (int.TryParse(lines[currentLine], out int graphId))
                    {
                        var graph = new Graph(graphId);
                        currentLine++;

                        // Если следующая строка содержит количество рёбер
                        if (currentLine < lines.Length && int.TryParse(lines[currentLine], out int edgeCount))
                        {
                            currentLine++;

                            // Чтение рёбер
                            for (int i = 0; i < edgeCount && currentLine < lines.Length; i++)
                            {
                                string[] vertices = lines[currentLine].Split(' ');
                                if (vertices.Length == 2 && 
                                    int.TryParse(vertices[0], out int v1) && 
                                    int.TryParse(vertices[1], out int v2))
                                {
                                    // Добавление рёбер в граф
                                    graph.AddEdge(new Edge(v1, v2));
                                }
                                currentLine++;
                            }
                            // Добавляем граф в список
                            graphs.Add(graph);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений при чтении файла
                throw new Exception($"Ошибка при чтении графа из файла: {ex.Message}");
            }

            return graphs;
        }

        // Метод для чтения графа по его идентификатору из файла
        public Graph ReadGraphById(string filePath, int targetGraphId)
        {
            try
            {
                // Чтение всех строк из файла
                string[] lines = File.ReadAllLines(filePath);
                int currentLine = 0;

                // Проход по строкам файла
                while (currentLine < lines.Length)
                {
                    // Пропускаем пустые строки и строки с комментариями
                    if (string.IsNullOrWhiteSpace(lines[currentLine]) || lines[currentLine].StartsWith("#"))
                    {
                        currentLine++;
                        continue;
                    }

                    // Если находим граф с нужным идентификатором
                    if (int.TryParse(lines[currentLine], out int graphId) && graphId == targetGraphId)
                    {
                        var graph = new Graph(graphId);
                        currentLine++;

                        // Если следующая строка содержит количество рёбер
                        if (currentLine < lines.Length && int.TryParse(lines[currentLine], out int edgeCount))
                        {
                            currentLine++;

                            // Чтение рёбер
                            for (int i = 0; i < edgeCount && currentLine < lines.Length; i++)
                            {
                                string[] vertices = lines[currentLine].Split(' ');
                                if (vertices.Length == 2 && 
                                    int.TryParse(vertices[0], out int v1) && 
                                    int.TryParse(vertices[1], out int v2))
                                {
                                    // Добавление рёбер в граф
                                    graph.AddEdge(new Edge(v1, v2));
                                }
                                currentLine++;
                            }
                            // Возвращаем найденный граф
                            return graph;
                        }
                    }
                    currentLine++;
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений при чтении конкретного графа
                throw new Exception($"Ошибка при чтении графа {targetGraphId}: {ex.Message}");
            }

            // Если граф не найден, выбрасываем исключение
            throw new Exception($"Граф с ID {targetGraphId} не найден");
        }
        
        // Метод для сохранения результатов анализа в файл
        public void SaveResults(string filePath, OutputData outputData)
        {
            // Создаем строку с результатами для записи в файл
            var resultBuilder = new StringBuilder();
            resultBuilder.AppendLine("=== Результат работы ===");
            resultBuilder.AppendLine($"Graph ID: {outputData.GraphId}");
            resultBuilder.AppendLine($"Analysis Date: {outputData.ProcessingTime:yyyy-MM-dd HH:mm:ss}");
            resultBuilder.AppendLine($"Execution Time: {outputData.ExecutionTime.TotalMilliseconds:F2} ms");
            resultBuilder.AppendLine();

            // Если Эйлеров цикл найден, выводим его, иначе сообщаем, что его нет
            if (outputData.HasEulerianCycle)
            {
                resultBuilder.AppendLine("Результат: Эйлеров цикл найден");
                resultBuilder.AppendLine("Цикл: " + string.Join(" -> ", outputData.EulerianCycle));
                resultBuilder.AppendLine($"Длина пути: {outputData.EulerianCycle.Count} вершины");
            }
            else
            {
                resultBuilder.AppendLine("Результат: Эйлеров цикл не существует в этом графе");
                resultBuilder.AppendLine("Причина: Граф не удовлетворяет требованиям Эйлерова цикла");
            }

            resultBuilder.AppendLine();
            resultBuilder.AppendLine("=== Требования для анализа ===");
            resultBuilder.AppendLine("Чтобы граф имел Эйлеров цикл:");
            resultBuilder.AppendLine("1. Все вершины должны иметь чётную степень");
            resultBuilder.AppendLine("2. Граф должен быть связным");

            // Создаем директорию для результатов, если она не существует
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Генерируем имя файла с временной меткой для результатов
            var fileName = $"graph_{outputData.GraphId}_result_{outputData.ProcessingTime:yyyyMMdd_HHmmss}.txt";
            var fullPath = Path.Combine(filePath, fileName);

            // Пытаемся сохранить результаты в файл
            try
            {
                File.WriteAllText(fullPath, resultBuilder.ToString());
            }
            catch (Exception ex)
            {
                // Обработка исключений при сохранении результатов в файл
                throw new IOException($"Ошибка при сохранении результатов в файл: {ex.Message}", ex);
            }
        }
    }
}

