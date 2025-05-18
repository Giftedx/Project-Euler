using System.Reflection;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace Project_Euler;

public static class ProblemFactory {
    private static readonly List<Type> Types;

    static ProblemFactory() {
        Types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Problem)) && !t.IsAbstract).ToList();
    }

    public static Problem CreateProblem(int id) {
        return (Problem)Activator.CreateInstance(Types[id]) ?? new Problem001();
    }

    public static int SolvedProblems() {
        return Types.Count;
    }
}