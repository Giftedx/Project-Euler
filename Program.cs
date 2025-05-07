using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace Project_Euler;

internal class Program {
    private ArrayList _solvedProblems = null!;
    public static void Main(string[] args) {
        Program prog = new Program();
        Console.WriteLine("Project Euler Solver");
        int chosenProblem = prog.GetInput();
        if (chosenProblem < 0) prog.SolveAll();
        else prog.Solve(chosenProblem);
    }
    
    private int GetInput() {
        _solvedProblems ??= GetSolvedProblems();
        Console.WriteLine("Enter 'a' to solve all problems.");
        Console.Write("Enter Problem to solve (1 - {0}): ", _solvedProblems.Count);
        string? input = Console.ReadLine();
        bool success = int.TryParse(input, out int result);
        return success && result > 0 ? result : -1;
    }

    private ArrayList GetSolvedProblems() {
        ArrayList solvedProblems = new ArrayList();
        Type[]? allTypes = Assembly.GetAssembly(typeof(Problem))?.GetTypes();
        foreach (Type type in allTypes!) 
            if (type.IsSubclassOf(typeof(Problem)))
                solvedProblems.Add((Problem)Activator.CreateInstance(type)!);
        return solvedProblems;
    }

    private void Solve(int n) {
        Problem problem = (Problem)_solvedProblems[n-1]!;
        Stopwatch watch = Stopwatch.StartNew();
        problem.Solve();
        watch.Stop();
        Console.WriteLine("Solved in {0} ms", watch.ElapsedMilliseconds);
    }

    private void SolveAll() {
        Stopwatch watch = Stopwatch.StartNew();
        for(int i = 1; i <= _solvedProblems.Count; i++) Solve(i);
        watch.Stop();
        Console.WriteLine("Solved all problems in {0} ms", watch.ElapsedMilliseconds);
    }
}