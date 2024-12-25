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

    public ConsoleMenu(IGraphService graphService, IFileService fileService, string graphFilePath)
    {
        _graphService = graphService;
        _fileService = fileService;
        _consoleHelper = new ConsoleHelper();
        _graphFilePath = graphFilePath;
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            _consoleHelper.DisplayHeader("Euler Cycle Finder");
            DisplayMainMenu();

            var choice = _consoleHelper.GetUserInput("Enter your choice (1-3): ");
            Console.Clear();

            try
            {
                switch (choice)
                {
                    case "1":
                        ProcessGraphSelection();
                        break;
                    case "2":
                        DisplayHelp();
                        break;
                    case "3":
                        running = false;
                        _consoleHelper.DisplayMessage("Thank you for using Euler Cycle Finder!");
                        break;
                    default:
                        _consoleHelper.DisplayError("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                _consoleHelper.DisplayError($"An error occurred: {ex.Message}");
            }

            if (running)
            {
                _consoleHelper.DisplayMessage("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    private void DisplayMainMenu()
    {
        _consoleHelper.DisplayMenuOption("1", "Find Euler Cycle");
        _consoleHelper.DisplayMenuOption("2", "Help");
        _consoleHelper.DisplayMenuOption("3", "Exit");
        Console.WriteLine();
    }

    private void ProcessGraphSelection()
    {
        _consoleHelper.DisplayHeader("Graph Selection");
        var graphId = _consoleHelper.GetIntegerInput("Enter graph number (1-5): ", 1, 5);

        try
        {
            var graph = _fileService.ReadGraphById(_graphFilePath, graphId);
            _consoleHelper.DisplayMessage($"\nProcessing graph {graphId}...\n");

            if (_graphService.HasEulerianCycle(graph))
            {
                var cycle = _graphService.FindEulerianCycle(graph);
                DisplayEulerCycle(cycle);
            }
            else
            {
                _consoleHelper.DisplayMessage("This graph does not have an Euler cycle.");
            }
        }
        catch (Exception ex)
        {
            _consoleHelper.DisplayError($"Error processing graph: {ex.Message}");
        }
    }

    private void DisplayEulerCycle(List<int> cycle)
    {
        _consoleHelper.DisplayHeader("Euler Cycle Found");
        _consoleHelper.DisplayMessage("Path: " + string.Join(" -> ", cycle));
    }

    private void DisplayHelp()
    {
        _consoleHelper.DisplayHeader("Help");
        _consoleHelper.DisplayMessage(@"
Euler Cycle Finder helps you find Euler cycles in graphs.

Usage:
1. Select 'Find Euler Cycle' from the main menu
2. Enter a graph number (1-5)
3. The program will analyze the graph and:
- Display the Euler cycle if one exists
- Inform you if no Euler cycle exists

Note: A graph has an Euler cycle if and only if:
- All vertices have even degree
- The graph is connected");
    }
}