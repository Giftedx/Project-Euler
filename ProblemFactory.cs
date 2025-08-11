using System.Diagnostics;
using static System.Reflection.Assembly;

namespace Project_Euler;

public static class ProblemFactory {
    /// <summary>
    /// Maximum problem ID to include. Problems with IDs >= MaxProblemId are excluded 
    /// (e.g., Problem999 and similar test problems).
    /// </summary>
    private const int MaxProblemId = 900;
    
    private static readonly Dictionary<int, Type> ProblemTypes = new();
    private static readonly Dictionary<int, Func<Problem>> ProblemFactories = new();
    private static bool _isInitialized = false;

    static ProblemFactory() {
        InitializeProblemRegistry();
    }

    private static void InitializeProblemRegistry()
    {
        if (_isInitialized) return;

        // Register all problems explicitly for better performance and type safety
        RegisterProblem<Problem001>();
        RegisterProblem<Problem002>();
        RegisterProblem<Problem003>();
        RegisterProblem<Problem004>();
        RegisterProblem<Problem005>();
        RegisterProblem<Problem006>();
        RegisterProblem<Problem007>();
        RegisterProblem<Problem008>();
        RegisterProblem<Problem009>();
        RegisterProblem<Problem010>();
        RegisterProblem<Problem011>();
        RegisterProblem<Problem012>();
        RegisterProblem<Problem013>();
        RegisterProblem<Problem014>();
        RegisterProblem<Problem015>();
        RegisterProblem<Problem016>();
        RegisterProblem<Problem017>();
        RegisterProblem<Problem018>();
        RegisterProblem<Problem019>();
        RegisterProblem<Problem020>();
        RegisterProblem<Problem021>();
        RegisterProblem<Problem022>();
        RegisterProblem<Problem023>();
        RegisterProblem<Problem024>();
        RegisterProblem<Problem025>();
        RegisterProblem<Problem026>();
        RegisterProblem<Problem027>();
        RegisterProblem<Problem028>();
        RegisterProblem<Problem029>();
        RegisterProblem<Problem030>();
        RegisterProblem<Problem031>();
        RegisterProblem<Problem032>();
        RegisterProblem<Problem033>();
        RegisterProblem<Problem034>();
        RegisterProblem<Problem035>();
        RegisterProblem<Problem036>();
        RegisterProblem<Problem037>();
        RegisterProblem<Problem038>();
        RegisterProblem<Problem039>();
        RegisterProblem<Problem040>();
        RegisterProblem<Problem041>();
        RegisterProblem<Problem042>();
        RegisterProblem<Problem043>();
        RegisterProblem<Problem044>();
        RegisterProblem<Problem045>();
        RegisterProblem<Problem046>();
        RegisterProblem<Problem047>();
        RegisterProblem<Problem048>();
        RegisterProblem<Problem049>();
        RegisterProblem<Problem050>();

        _isInitialized = true;
    }

    private static void RegisterProblem<T>() where T : Problem, new()
    {
        var problem = new T();
        int? problemId = ExtractProblemId(typeof(T).Name);
        
        if (problemId.HasValue && problemId.Value < MaxProblemId)
        {
            ProblemTypes[problemId.Value] = typeof(T);
            ProblemFactories[problemId.Value] = () => new T();
        }
    }

    public static Problem CreateProblem(int id) {
        if (ProblemFactories.TryGetValue(id, out var factory)) {
            return factory();
        }

        throw new ArgumentOutOfRangeException(nameof(id), $"Problem with ID {id} not found.");
    }

    public static int SolvedProblems() {
        return ProblemTypes.Count;
    }

    private static int? ExtractProblemId(string typeName) {
        if (typeName.StartsWith("Problem") && int.TryParse(typeName.AsSpan(7), out int id)) return id;

        return null;
    }
}