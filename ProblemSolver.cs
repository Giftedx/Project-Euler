using System.Collections.Concurrent;
using System.Diagnostics;

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

        testData.TotalTime = watch.Elapsed.TotalMilliseconds;

        if (results.Any()) {
            var slowestProblemData = results
                .Select(p => new { Problem = p, AvgTime = p.Times.Any() ? p.Times.Average() : 0 })
                .OrderByDescending(x => x.AvgTime)
                .FirstOrDefault();

            if (slowestProblemData != null) {
                testData.SlowestProblem = slowestProblemData.Problem.Index;
                testData.SlowestTime = slowestProblemData.AvgTime;
            } else {
                // This case should ideally not be hit if results.Any() is true and all problems have data,
                // but it's a safeguard.
                testData.SlowestTime = 0;
                testData.SlowestProblem = 0;
            }
        } else {
            testData.SlowestTime = 0;
            testData.SlowestProblem = 0;
        }

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

            if (i == 0) data.Result = result?.ToString() ?? string.Empty; // Handle if result itself is null
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

    public static void RunTest() {
        var watch = Stopwatch.StartNew();
        Test.Solve();
        watch.Stop();
        Console.WriteLine($"{watch.ElapsedMilliseconds} ms");
    }
}

public class ProblemData(int index, int runs) {
    public readonly int Index = index;
    public readonly List<double> Times = new(runs);
    public string Result = "";

    public double AverageTime => Times.Any() ? Times.Average() : 0.0;
    public double MinTime => Times.Any() ? Times.Min() : 0.0;
    public double MaxTime => Times.Any() ? Times.Max() : 0.0;
}

public class BenchmarkData {
    public int SlowestProblem;
    public double SlowestTime = double.MinValue;
    public double TotalTime;
}