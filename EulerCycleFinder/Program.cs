using EulerCycleFinder.Services;
using EulerCycleFinder.UI;

// Используйте относительный путь от корня проекта
var graphFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "graphs.txt");
            
var fileService = new FileService();
var graphService = new GraphService();
            
var consoleMenu = new ConsoleMenu(graphService, fileService, graphFilePath);
consoleMenu.Run();