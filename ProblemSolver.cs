using System.Collections.Concurrent;
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

    public int GetProblemCount() {
        return _solvedProblems.Count;
    }


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
        var data = Solve(n, 1);
        Library.FunPrint($"Problem {n}: {data.Result}");
        Library.FunPrint($"Solved in {data.Times[0]:F3} ms");
    }

    public void SolveAll() {
        const string file = "log.txt";
        StreamWriter sw = null;
        var originalOut = Console.Out;
        double totalTime = 0;
        try {
            sw = new StreamWriter(new FileStream(file, FileMode.Create));
            Console.SetOut(sw);

            var bag = new ConcurrentBag<ProblemData>();
            var range = Enumerable.Range(1, _solvedProblems.Count);
            Parallel.ForEach(range, i => { bag.Add(Solve(i, 100)); });
            var results = bag.OrderBy(p => p.Index).ToList();
            double slowestTime = double.MinValue;
            int slowestProblem = 0;

            foreach (var result in results) {
                double best = result.Times.Min();
                double worst = result.Times.Max();
                double average = result.Times.Average();
                totalTime += best;
                if (worst > slowestTime) {
                    slowestTime = worst;
                    slowestProblem = result.Index;
                }

                string probNumber = result.Index < 10 ? "0" + result.Index : result.Index.ToString();
                Console.WriteLine($"Problem {probNumber}: {result.Result}");
                Console.WriteLine($"    Best:   {best:F3} ms");
                Console.WriteLine($"    Worst:  {worst:F3} ms");
                Console.WriteLine($"    Avg:    {average:F3} ms");
                Console.WriteLine();
            }

            Console.WriteLine($"Total Time: {totalTime:F3} ms");
            Console.WriteLine($"Average solution time: {totalTime / results.Count:F3} ms");
            Console.WriteLine($"Slowest Problem: {slowestProblem} with {slowestTime:F3} ms");
        }
        finally {
            Console.SetOut(originalOut);
            Library.FunPrint($"Results output to {file}, {totalTime:F3} ms total");
            sw?.Dispose();
        }
    }

    private ProblemData Solve(int n, int runs) {
        var data = new ProblemData(n, runs);
        var problem = _solvedProblems[n - 1];

        for (int i = 0; i < runs; i++) {
            var watch = Stopwatch.StartNew();

            object result = problem.Solve();
            if (i == 0) data.Result = result.ToString() ?? string.Empty;
            /*if (i == 0) data.Result = problem.Solve().ToString() ?? string.Empty;
            else _ = problem.Solve();*/

            watch.Stop();
            data.Times.Add(watch.Elapsed.TotalMilliseconds);
        }

        return data;
    }

    public static void Test() {
        var test = new Test();
        var watch = Stopwatch.StartNew();
        test.Solve();
        watch.Stop();
        Console.WriteLine($"{watch.ElapsedMilliseconds} ms");
    }

    private struct ProblemData {
        public readonly int Index;
        public readonly List<double> Times = [];
        public string Result;

        public ProblemData(int index, int runs) {
            Result = "";
            Index = index;
            Times = new List<double>(runs);
        }
    }
}