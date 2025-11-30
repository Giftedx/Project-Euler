using System.Collections.Concurrent;
using System.Diagnostics;

// ReSharper disable RedundantAssignment
namespace Project_Euler;

/// <summary>
/// Provides functionality to solve and benchmark Project Euler problems.
/// Handles execution of individual problems or full suites, collecting timing data and results.
/// </summary>
public static class ProblemSolver {
    /// <summary>
    /// The number of times to run each problem during a full benchmark to calculate average time.
    /// </summary>
    private const int BenchmarkRuns = 100;

    /// <summary>
    /// Solves and benchmarks a single problem specified by its ID (as a string).
    /// </summary>
    /// <param name="problem">The problem ID string (e.g., "1" or "001").</param>
    public static void IndividualBenchmark(string problem) {
        int n = Convert.ToInt32(problem);
        var data = Run(n, 1);
        Library.FunPrint($"Problem {n}: {data.Result}");
        Library.FunPrint($"Solved in {data.Times[0]:F3} ms");
    }

    /// <summary>
    /// Runs a full benchmark of all registered problems.
    /// Executes problems in parallel, collects performance metrics, and generates a report.
    /// </summary>
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

    /// <summary>
    /// Runs a specific problem a specified number of times and collects timing data.
    /// </summary>
    /// <param name="n">The ID of the problem to run.</param>
    /// <param name="runs">The number of times to execute the problem's solution.</param>
    /// <returns>A <see cref="ProblemData"/> object containing the result and execution times.</returns>
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

    /// <summary>
    /// Displays a progress bar in the console to track the completion of the benchmark.
    /// </summary>
    /// <param name="completedFunc">A function that returns the number of completed tasks.</param>
    /// <param name="total">The total number of tasks to complete.</param>
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

}

/// <summary>
/// Represents the data collected from running a problem, including its index, result, and execution times.
/// </summary>
public class ProblemData(int index, int runs) {
    /// <summary>
    /// The problem number/index.
    /// </summary>
    public readonly int Index = index;
    /// <summary>
    /// A list of execution times in milliseconds for each run.
    /// </summary>
    public readonly List<double> Times = new(runs);
    /// <summary>
    /// The string representation of the solution result.
    /// </summary>
    public string Result = "";

    /// <summary>
    /// Calculated average execution time.
    /// </summary>
    public double AverageTime => Times.Any() ? Times.Average() : 0.0;
    /// <summary>
    /// Minimum execution time recorded.
    /// </summary>
    public double MinTime => Times.Any() ? Times.Min() : 0.0;
    /// <summary>
    /// Maximum execution time recorded.
    /// </summary>
    public double MaxTime => Times.Any() ? Times.Max() : 0.0;
}

/// <summary>
/// Aggregates global benchmark statistics, such as the total time and the slowest problem.
/// </summary>
public class BenchmarkData {
    /// <summary>
    /// The index of the problem that took the longest to solve (on average).
    /// </summary>
    public int SlowestProblem;
    /// <summary>
    /// The average time of the slowest problem in milliseconds.
    /// </summary>
    public double SlowestTime = double.MinValue;
    /// <summary>
    /// The total wall-clock time taken for the full benchmark suite.
    /// </summary>
    public double TotalTime;
}
