namespace Project_Euler;

/// <summary>
/// The main entry point for the Project Euler solver application.
/// </summary>
internal static class Program {
    private static readonly Dictionary<string, (string Description, Action Action)> MenuActions =
        new(StringComparer.OrdinalIgnoreCase) {
            { "a", ("solve all problems", ProblemSolver.FullBenchmark) },
            { "t", ("verify all known solutions", SolutionVerifier.VerifyAllKnownSolutions) }
        };

    /// <summary>
    /// The application entry point.
    /// </summary>
    public static void Main() {
        RunInteractionLoop();
    }

    /// <summary>
    /// Runs the main interaction loop, displaying the menu and handling user input.
    /// </summary>
    private static void RunInteractionLoop() {
        do {
            PrintMenu();
            string input = InputHandler.GetMenuSelection();
            HandleMenuSelection(input);
        } while (InputHandler.ShouldRunAgain());
    }

    /// <summary>
    /// Prints the main menu to the console.
    /// </summary>
    private static void PrintMenu() {
        Console.Clear();
        Library.FunPrint("Project Euler Solver");
        Console.WriteLine();
        foreach ((string key, (string description, _)) in MenuActions)
            Library.FunPrint($"Enter '{key}' to {description}.");

        Library.FunPrint($"Enter Problem to solve (1 - {ProblemFactory.SolvedProblems()}): ");
    }

    /// <summary>
    /// Handles the user's menu selection.
    /// </summary>
    /// <param name="input">The user's input string.</param>
    private static void HandleMenuSelection(string input) {
        if (MenuActions.TryGetValue(input, out var action))
            action.Action.Invoke();
        else
            ProblemSolver.IndividualBenchmark(input);
    }
}
