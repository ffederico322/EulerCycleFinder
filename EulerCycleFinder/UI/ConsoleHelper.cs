using EulerCycleFinder.Services;

namespace EulerCycleFinder.UI;

// Класс для отображения сообщений и обработки пользовательского ввода в консольном интерфейсе
public class ConsoleHelper
{
    // Метод для отображения заголовка в консоли
    public void DisplayHeader(string text)
    {
        // Выводит текст в формате "=== text ==="
        Console.WriteLine($"\n=== {text} ===\n");
    }

    // Метод для отображения обычного сообщения
    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    // Метод для отображения ошибки с красным цветом текста
    public void DisplayError(string message)
    {
        // Сохраняем текущий цвет текста, чтобы восстановить его после отображения ошибки
        var originalColor = Console.ForegroundColor;
        // Меняем цвет текста на красный
        Console.ForegroundColor = ConsoleColor.Red;
        // Выводим сообщение об ошибке
        Console.WriteLine($"Ошибка: {message}");
        // Восстанавливаем оригинальный цвет текста
        Console.ForegroundColor = originalColor;
    }

    // Метод для отображения пунктов меню
    public void DisplayMenuOption(string key, string description)
    {
        // Отображаем пункт меню в формате "key. description"
        Console.WriteLine($"{key}. {description}");
    }

    // Метод для получения строки от пользователя
    public string GetUserInput(string prompt)
    {
        Console.Write(prompt); // Выводим подсказку
        return Console.ReadLine()?.Trim() ?? string.Empty; // Читаем строку и удаляем лишние пробелы
    }

    // Метод для получения целочисленного ввода от пользователя в пределах заданного диапазона
    public int GetIntegerInput(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt); // Запрашиваем ввод
            // Пытаемся преобразовать ввод в целое число и проверяем, что оно в пределах допустимого диапазона
            if (int.TryParse(Console.ReadLine(), out int result) && 
                result >= min && result <= max)
            {
                return result; // Если ввод корректен, возвращаем результат
            }
            // Если ввод некорректен, отображаем ошибку
            DisplayError($"Пожалуйста, введите число между {min} и {max}");
        }
    }
}
