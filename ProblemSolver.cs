using System.Collections.Concurrent;
using System.Diagnostics;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
// ReSharper disable RedundantAssignment
namespace Project_Euler;

public static class ProblemSolver {
    private const int BenchmarkRuns = 100;

    public static void IndividualBenchmark(string problem) {
        int n = Convert.ToInt32(problem);
        var data = Run(n, 1);
        Library.FunPrint($"Problem {n}: {data.Result}");
        Library.FunPrint($"Solved in {data.Times[0]:F3} ms");
    }

    public static void FullBenchmark() {
        int problemCount = ProblemFactory.SolvedProblems();
        var testData = new BenchmarkData();
        var bag = new ConcurrentBag<ProblemData>();
        var range = Enumerable.Range(1, problemCount);

        int completedTasks = 0;
        var progressThread = new Thread(() => 
            DisplayProgressBar(() => completedTasks, problemCount));

        var watch = Stopwatch.StartNew();
        progressThread.Start();
        Parallel.ForEach(range, i => {
            bag.Add(Run(i, BenchmarkRuns));
            Interlocked.Increment(ref completedTasks);
        });

        progressThread.Join();
        var results = bag.OrderBy(p => p.Index).ToList();

        watch.Stop();
        OutputHandler.GenerateFullReport(results, testData);
        Library.FunPrint($"Results output to {OutputHandler.LogFile}, {watch.ElapsedMilliseconds} ms total");
    }

    private static ProblemData Run(int n, int runs) {
        var data = new ProblemData(n, runs);
        var problem = ProblemFactory.CreateProblem(n);

        for (int i = 0; i < runs; i++) {
            var watch = Stopwatch.StartNew();

            object result = problem.Solve();
            watch.Stop();

            if (i == 0) data.Result = result.ToString() ?? string.Empty;
            data.Times.Add(watch.Elapsed.TotalMilliseconds);
        }

        return data;
    }

    private static void DisplayProgressBar(Func<int> completedFunc, int total) {
        const int width = 50;
        const int updateInterval = 10;
        while (true) {
            int completed = completedFunc();
            double percent = (double)completed / total;
            int filled = (int)(percent * width);
            string bar = new string('█', filled) + new string('░', width - filled);

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"[{bar}] {percent * 100:F1}%");

            if (completed >= total) {
                Console.WriteLine();
                break;
            }

            Thread.Sleep(updateInterval);
        }
    }

    public static void Test() {
        var test = new Test();
        var watch = Stopwatch.StartNew();
        test.Solve();
        watch.Stop();
        Console.WriteLine($"{watch.ElapsedMilliseconds} ms");
    }
}
public class ProblemData(int index, int runs) {
    public readonly int Index = index;
    public readonly List<double> Times = new(runs);
    public string Result = "";
}

public class BenchmarkData {
    public double TotalTime;
    public double SlowestTime = double.MinValue;
    public int SlowestProblem;
}