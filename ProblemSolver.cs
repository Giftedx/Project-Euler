using System.Diagnostics;
using static System.Reflection.Assembly;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
// ReSharper disable RedundantAssignment
namespace Project_Euler;

public class ProblemSolver {
    private readonly List<Problem> _solvedProblems;

    public ProblemSolver() {
        _solvedProblems = GetSolvedProblems();
    }

    public int GetProblemCount() => _solvedProblems.Count;
    

    private List<Problem> GetSolvedProblems() {
        var allTypes = GetAssembly(typeof(Problem))?.GetTypes() ?? Type.EmptyTypes;

        return allTypes
            .Where(t => t.IsSubclassOf(typeof(Problem)))
            .Select(t => Activator.CreateInstance(t) as Problem)
            .Where(p => p != null)
            .ToList();
    }

    public void SolveIndividual(string problem) {
        int n = Convert.ToInt32(problem);
        Library.FunPrint($"Problem {n}: ");
        double time = Solve(n, runs: 1);
        Console.WriteLine($"Solved in {time} ms");
    }

    public void SolveAll() {
        const string file = "log.txt";
        StreamWriter sw = null;
        TextWriter originalOut = Console.Out;
        
        try
        {
            sw = new StreamWriter(new FileStream(file, FileMode.Create));
            Console.SetOut(sw);

            double totalTime = 0;
            double slowestTime = 0;
            int slowestProblem = 0;
            

            for (int i = 1; i <= _solvedProblems.Count; i++) {
                Console.Write($"Problem {i}: ");
                double minTime = Solve(i, runs: 100);
                Console.WriteLine($"Best of 100: {minTime:F3} ms\n");

                totalTime += minTime;

                if (!(minTime > slowestTime)) continue;
                slowestTime = minTime;
                slowestProblem = i;
            }

            double averageTime = totalTime / _solvedProblems.Count;
            Console.WriteLine($"Solved all problems in {totalTime:F3} ms");
            Console.WriteLine($"Average best solution time: {averageTime:F3} ms");
            Console.WriteLine($"Slowest best time: Problem {slowestProblem}, {slowestTime:F3} ms");

            Console.SetOut(originalOut);
            Library.FunPrint($"Results output to {file}, {totalTime:F3} ms total");
        }finally {
            Console.SetOut(originalOut);
            sw?.Dispose();
        }
    }
    
    private double Solve(int n, int runs) {
        double minTime = double.MaxValue;
        var originalOut = Console.Out;
        using var sw = new StringWriter();
        for (int i = 0; i < runs; i++) {
            var problem = _solvedProblems[n - 1];
            if (i != 0) Console.SetOut(sw);
            
            var watch = Stopwatch.StartNew();
            problem.Solve();
            watch.Stop();
            
            double time = watch.Elapsed.TotalMilliseconds;
            if (time < minTime) minTime = time;
            if (i != 0) Console.SetOut(originalOut);
        }
        return minTime;
    }
}