namespace Project_Euler;

internal static class Program {
    private static readonly Dictionary<string, (string Description, Action Action)> MenuActions =
        new(StringComparer.OrdinalIgnoreCase) {
            { "a", ("solve all problems", ProblemSolver.FullBenchmark) },
            { "t", ("verify all known solutions", SolutionVerifier.VerifyAllKnownSolutions) }
        };

    public static void Main(string[] args) {
        // Initialize Configuration and Logger
        var config = Configuration.Instance;
        Logger.SetLogLevel(config.Logging.MinimumLevel);
        Logger.Info("Application started");

        if (args.Length > 0) {
            HandleCommandLineArguments(args);
        } else {
            RunInteractionLoop();
        }

        Logger.Info("Application shutting down");
    }

    private static void HandleCommandLineArguments(string[] args) {
        string command = args[0].ToLowerInvariant();

        switch (command) {
            case "verify":
                SolutionVerifier.VerifyAllKnownSolutions();
                break;
            case "solve-all":
                ProblemSolver.FullBenchmark();
                break;
            case "solve":
                if (args.Length > 1) {
                    ProblemSolver.IndividualBenchmark(args[1]);
                } else {
                    Console.WriteLine("Error: Please provide a problem ID (e.g., 'solve 1').");
                }
                break;
            default:
                Console.WriteLine($"Unknown command: {command}");
                Console.WriteLine("Available commands: verify, solve-all, solve <id>");
                break;
        }
    }

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
        if (MenuActions.TryGetValue(input, out var action))
            action.Action.Invoke();
        else
            ProblemSolver.IndividualBenchmark(input);
    }
}
