using EulerCycleFinder.Interfaces;
using EulerCycleFinder.Models;
using EulerCycleFinder.Services;
using EulerCycleFinder.UI;

try
{
    // Получаем путь к текущему каталогу приложения
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    
    // Путь к каталогу с данными (например, файл с графами)
    string dataDirectory = Path.Combine(baseDirectory, "Data");
            
    // Путь к файлу с графами (graphs.txt)
    string graphFilePath = Path.Combine(dataDirectory, "graphs.txt");
            
    // Проверяем, существует ли файл с графами
    if (!File.Exists(graphFilePath))
    {
        // Если файл не найден, выбрасываем исключение
        throw new FileNotFoundException($"Не найден обязательный файл: {graphFilePath}");
    }

    // Создаем экземпляры сервисов для работы с файлами и графами
    var fileService = new FileService();
    var graphService = new GraphService(fileService);
    
    // Создаем и запускаем консольное меню для взаимодействия с пользователем
    var consoleMenu = new ConsoleMenu(graphService, fileService, graphFilePath);
            
    consoleMenu.Run(); // Запуск меню
}
catch (Exception ex)
{
    // Если произошла ошибка, выводим сообщение об ошибке
    Console.WriteLine($"Критическая ошибка: {ex.Message}");
    // Ожидаем нажатия клавиши для завершения программы
    Console.WriteLine("Нажмите любую клавишу для выхода...");
    Console.ReadKey();
}