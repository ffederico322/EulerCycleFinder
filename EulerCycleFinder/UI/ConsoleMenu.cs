using EulerCycleFinder.Models;
using EulerCycleFinder.Interfaces;
using EulerCycleFinder.Services;
using System.IO;

namespace EulerCycleFinder.UI;

public class ConsoleMenu
{
    private readonly IGraphService _graphService;
    private readonly IFileService _fileService;
    private readonly ConsoleHelper _consoleHelper;
    private readonly string _graphFilePath;
    private readonly string _outputPath;

    public ConsoleMenu(IGraphService graphService, IFileService fileService, string graphFilePath)
    {
        _graphService = graphService;
        _fileService = fileService;
        _consoleHelper = new ConsoleHelper();
        _graphFilePath = graphFilePath;
        // Создаем директорию для вывода в том же месте, где находится файл с графами
        _outputPath = Path.Combine(Path.GetDirectoryName(_graphFilePath), "Results");
    }

    // Главный метод, запускающий консольное меню
    public void Run()
    {
        bool running = true;
        while (running)
        {
            _consoleHelper.DisplayHeader("Поиск Эйлерова цикла");
            DisplayMainMenu();

            var choice = _consoleHelper.GetUserInput("Введите ваш выбор (1-4): ");
            Console.Clear();

            try
            {
                switch (choice)
                {
                    case "1":
                        ProcessGraphSelection();
                        break;
                    case "2":
                        ProcessGraphWithFileOutput();
                        break;
                    case "3":
                        DisplayHelp();
                        break;
                    case "4":
                        running = false;
                        _consoleHelper.DisplayMessage("Спасибо за использование программы для поиска Эйлерова цикла!");
                        break;
                    default:
                        _consoleHelper.DisplayError("Некорректный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
            catch (Exception ex)
            {
                _consoleHelper.DisplayError($"Произошла ошибка: {ex.Message}");
            }

            if (running)
            {
                _consoleHelper.DisplayMessage("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    // Метод для отображения главного меню
    private void DisplayMainMenu()
    {
        _consoleHelper.DisplayMenuOption("1", "Найти Эйлеров цикл (Только отображение)");
        _consoleHelper.DisplayMenuOption("2", "Найти Эйлеров цикл (Сохранить в файл)");
        _consoleHelper.DisplayMenuOption("3", "Помощь");
        _consoleHelper.DisplayMenuOption("4", "Выход");
        Console.WriteLine();
    }
    
    // Метод для обработки поиска графа и сохранения результатов в файл
    private void ProcessGraphWithFileOutput()
    {
        _consoleHelper.DisplayHeader("Выбор графа (Сохранение в файл)");
        var graphId = _consoleHelper.GetIntegerInput("Введите номер графа (1-5): ", 1, 5);

        try
        {
            var inputData = new InputData(graphId, _graphFilePath);
            var outputData = _graphService.ProcessGraph(inputData);
            
            // Сохраняем результаты в файл
            outputData.SaveToFile(_outputPath);

            // Также отображаем результаты в консоли
            if (outputData.HasEulerianCycle)
            {
                DisplayEulerCycle(outputData.EulerianCycle);
                _consoleHelper.DisplayMessage($"\nРезультаты сохранены в: {_outputPath}");
                _consoleHelper.DisplayMessage($"Время выполнения: {outputData.ExecutionTime.TotalMilliseconds:F2} мс");
            }
            else
            {
                _consoleHelper.DisplayMessage("Этот граф не имеет Эйлерова цикла.");
                _consoleHelper.DisplayMessage($"\nРезультаты сохранены в: {_outputPath}");
            }
        }
        catch (Exception ex)
        {
            _consoleHelper.DisplayError($"Ошибка при обработке графа: {ex.Message}");
        }
    }

    // Метод для обработки выбора графа и отображения результата в консоли
    private void ProcessGraphSelection()
    {
        _consoleHelper.DisplayHeader("Выбор графа");
        var graphId = _consoleHelper.GetIntegerInput("Введите номер графа (1-5): ", 1, 5);

        try
        {
            var graph = _fileService.ReadGraphById(_graphFilePath, graphId);
            _consoleHelper.DisplayMessage($"\nОбработка графа {graphId}...\n");

            if (_graphService.HasEulerianCycle(graph))
            {
                var cycle = _graphService.FindEulerianCycle(graph);
                DisplayEulerCycle(cycle);
            }
            else
            {
                _consoleHelper.DisplayMessage("Этот граф не имеет Эйлерова цикла.");
            }
        }
        catch (Exception ex)
        {
            _consoleHelper.DisplayError($"Ошибка при обработке графа: {ex.Message}");
        }
    }

    // Метод для отображения найденного Эйлерова цикла
    private void DisplayEulerCycle(List<int> cycle)
    {
        _consoleHelper.DisplayHeader("Эйлеров цикл найден");
        _consoleHelper.DisplayMessage("Путь: " + string.Join(" -> ", cycle));
    }

    // Метод для отображения помощи
    private void DisplayHelp()
    {
        _consoleHelper.DisplayHeader("Помощь");
        _consoleHelper.DisplayMessage(@"
Программа для поиска Эйлерова цикла в графах.

Использование:
1. Выберите 'Найти Эйлеров цикл' в главном меню
2. Введите номер графа (1-5)
3. Программа проанализирует граф и:
- Отобразит Эйлеров цикл, если он существует
- Сообщит, если Эйлеров цикла нет

Примечание: Граф имеет Эйлеров цикл, если и только если:
- Все вершины имеют чётную степень
- Граф связан");
    }
}
