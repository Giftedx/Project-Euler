namespace Project_Euler;

public static class InputHandler {
    private const string SolveAllCommand = "a";
    private const string RunTestsCommand = "t";

    public static string GetInput(int count) {
        string input;

        while (true) {
            Library.FunPrint("Enter 'a' to solve all problems.");
            Library.FunPrint($"Enter Problem to solve (1 - {count}): ");
            input = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.WriteLine();
            if (ValidateMenu(input, count)) break;
            Library.FunPrint("Invalid input. Please try again.");
        }

        return input;
    }

    public static bool RunAgain() {
        Library.FunPrint("Press any to run program again, Space to exit.");
        var input = Console.ReadKey(true).Key;
        return input != ConsoleKey.Spacebar;
    }

    private static bool ValidateMenu(string input, int count) {
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (int.TryParse(input, out int num))
            return num > 0 && num <= count;
        return input.Equals(SolveAllCommand, StringComparison.OrdinalIgnoreCase) ||
               input.Equals(RunTestsCommand, StringComparison.OrdinalIgnoreCase);
    }
}