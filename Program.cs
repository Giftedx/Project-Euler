namespace Project_Euler;

internal static class Program {
    private static readonly Dictionary<string, (string Description, Action Action)> MenuActions = 
        new(StringComparer.OrdinalIgnoreCase) {
        { "a", ("solve all problems", ProblemSolver.FullBenchmark) },
        { "t", ("run test routine", ProblemSolver.Test) }
    };

    public static void Main() => RunInteractionLoop();

    private static void RunInteractionLoop() {
        do {
            PrintMenu();
            string input = InputHandler.GetMenuSelection();
            HandleMenuSelection(input);
        } while (InputHandler.ShouldRunAgain());
    }

    private static void PrintMenu() {
        Console.Clear();
        Library.FunPrint("Project Euler Solver");
        Console.WriteLine();
        foreach ((string key, (string description, _)) in MenuActions)
            Library.FunPrint($"Enter '{key}' to {description}.");
        
        Library.FunPrint($"Enter Problem to solve (1 - {ProblemFactory.SolvedProblems()}): ");
    }

    private static void HandleMenuSelection(string input) {
        if (MenuActions.TryGetValue(input, out var action)) {
            action.Action.Invoke();
        } else {
            ProblemSolver.IndividualBenchmark(input);
        }
    }
}