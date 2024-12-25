namespace EulerCycleFinder.Interfaces;

public interface IConsoleMenu
{
    /// <summary>
    /// Displays a formatted header in the console.
    /// </summary>
    /// <param name="text">The header text to display</param>
    void DisplayHeader(string text);

    /// <summary>
    /// Displays a regular message in the console.
    /// </summary>
    /// <param name="message">The message to display</param>
    void DisplayMessage(string message);

    /// <summary>
    /// Displays an error message in the console with appropriate formatting.
    /// </summary>
    /// <param name="message">The error message to display</param>
    void DisplayError(string message);

    /// <summary>
    /// Displays a menu option in the console.
    /// </summary>
    /// <param name="key">The key or number for the menu option</param>
    /// <param name="description">The description of the menu option</param>
    void DisplayMenuOption(string key, string description);

    /// <summary>
    /// Gets user input from the console with a prompt.
    /// </summary>
    /// <param name="prompt">The prompt to display to the user</param>
    /// <returns>The user's input as a string</returns>
    string GetUserInput(string prompt);

    /// <summary>
    /// Gets integer input from the user within a specified range.
    /// </summary>
    /// <param name="prompt">The prompt to display to the user</param>
    /// <param name="min">The minimum acceptable value</param>
    /// <param name="max">The maximum acceptable value</param>
    /// <returns>The validated integer input</returns>
    int GetIntegerInput(string prompt, int min, int max);
}