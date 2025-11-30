namespace Project_Euler;

/// <summary>
/// Handles user input for the console application.
/// Responsible for validating menu selections and managing the application loop flow.
/// </summary>
public static class InputHandler {
    private const string SolveAllCommand = "a";
    private const string RunTestsCommand = "t";

    /// <summary>
    /// Prompts the user for a menu selection and validates the input.
    /// Blocks until a valid selection is made.
    /// </summary>
    /// <returns>A valid menu selection string (either a command or a problem number).</returns>
    public static string GetMenuSelection() {
        string input;

        while (true) {
            input = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.WriteLine();
            if (IsValidSelection(input)) break;
            Library.FunPrint("Invalid input. Please try again.");
        }

        return input;
    }

    /// <summary>
    /// Checks if the user wants to run the application loop again.
    /// </summary>
    /// <returns>True if the user pressed any key other than Space; false if Space was pressed.</returns>
    public static bool ShouldRunAgain() {
        Library.FunPrint("Press any key to run program again, Space to exit.");
        var input = Console.ReadKey(true).Key;
        return input != ConsoleKey.Spacebar;
    }

    /// <summary>
    /// Validates the user's input string against the allowed commands and available problem range.
    /// </summary>
    /// <param name="input">The input string to validate.</param>
    /// <returns>True if the input is a valid command ('a', 't') or a valid problem number; otherwise, false.</returns>
    private static bool IsValidSelection(string input) {
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (int.TryParse(input, out int num))
            return num > 0 && num <= ProblemFactory.SolvedProblems();
        return input.Equals(SolveAllCommand, StringComparison.OrdinalIgnoreCase) ||
               input.Equals(RunTestsCommand, StringComparison.OrdinalIgnoreCase);
    }
}
