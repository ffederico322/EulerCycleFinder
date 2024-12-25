using EulerCycleFinder.Services;

namespace EulerCycleFinder.UI;

public class ConsoleHelper
{
    public void DisplayHeader(string text)
    {
        Console.WriteLine($"\n=== {text} ===\n");
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayError(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {message}");
        Console.ForegroundColor = originalColor;
    }

    public void DisplayMenuOption(string key, string description)
    {
        Console.WriteLine($"{key}. {description}");
    }

    public string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public int GetIntegerInput(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result) && 
                result >= min && result <= max)
            {
                return result;
            }
            DisplayError($"Please enter a number between {min} and {max}");
        }
    }
}