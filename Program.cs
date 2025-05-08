using System.Collections;
using System.Diagnostics;
using System.Reflection;
namespace Project_Euler;

internal class Program {
    private ArrayList _solvedProblems = null!;

    public static void Main() {
        Program prog = new Program();
        do {
            Console.WriteLine("Project Euler Solver");
            int chosenProblem = prog.GetInput();
            if (chosenProblem < 0) prog.SolveAll();
            else prog.Solve(chosenProblem);
        } while (prog.RunAgain());
    }
    
    private int GetInput() {
        _solvedProblems = GetSolvedProblems();
        Console.WriteLine("Enter 'a' to solve all problems.");
        Console.Write("Enter Problem to solve (1 - {0}): ", _solvedProblems.Count);
        string? input = Console.ReadLine();
        Console.WriteLine(" ");
        bool success = int.TryParse(input, out int result);
        return success && result > 0 ? result : -1;
    }

    private bool RunAgain() {
        Console.WriteLine("Type 'yes' to run program again.");
        string? input = Console.ReadLine();
        return input == "yes";
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
        Problem problem = (Problem)_solvedProblems[n - 1]!;
        int problemNum = int.Parse(problem.GetType().Name.Remove(0, 7));
        Stopwatch watch = Stopwatch.StartNew();
        Console.Write("Problem {0}: ", problemNum);
        problem.Solve();
        watch.Stop();
        Console.WriteLine("Solved in {0} ms \n", watch.ElapsedMilliseconds);
    }

    private void SolveAll() {
        const string file = "log.txt";
        FileStream fs = new FileStream(file, FileMode.Create);
        TextWriter temp = Console.Out;
        StreamWriter sw = new StreamWriter(fs);
        Console.SetOut(sw);
        Stopwatch watch = Stopwatch.StartNew();
        for(int i = 1; i <= _solvedProblems.Count; i++) Solve(i);
        watch.Stop();
        Console.WriteLine("Solved all problems in {0} ms", watch.ElapsedMilliseconds);
        Console.SetOut(temp);
        Console.WriteLine("Results output to {0}", file);
        sw.Close();
    }
}