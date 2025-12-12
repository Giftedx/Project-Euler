using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project_Euler;

public class BenchmarkRunner
{
    public class BenchmarkResult
    {
        public int ProblemId { get; set; }
        public string Result { get; set; } = string.Empty;
        public double MeanTime { get; set; }
        public double MedianTime { get; set; }
        public double StandardDeviation { get; set; }
        public double MinTime { get; set; }
        public double MaxTime { get; set; }
        public int TotalRuns { get; set; }
        public double ConfidenceInterval { get; set; }
        public List<double> Times { get; set; } = new();
    }

    private static BenchmarkSettings Settings => Configuration.Instance.Benchmark;

    public static BenchmarkResult RunBenchmark(int problemId, Problem problem)
    {
        var result = new BenchmarkResult { ProblemId = problemId };
        
        // Warm-up phase
        for (int i = 0; i < Settings.WarmupRuns; i++)
        {
            problem.Solve();
        }

        // Benchmark phase with adaptive number of runs
        var times = new List<double>();
        var stopwatch = new Stopwatch();
        
        // Initial runs to get a baseline
        for (int i = 0; i < Settings.MinBenchmarkRuns; i++)
        {
            stopwatch.Restart();
            var solution = problem.Solve();
            stopwatch.Stop();
            
            if (i == 0)
            {
                result.Result = solution?.ToString() ?? string.Empty;
            }
            
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
        }

        // Calculate initial statistics
        var stats = CalculateStatistics(times);
        
        // Determine if we need more runs for statistical significance
        int additionalRuns = CalculateRequiredRuns(stats.StandardDeviation, stats.Mean, Settings.ConfidenceLevel);
        additionalRuns = Math.Min(additionalRuns, Settings.MaxBenchmarkRuns - Settings.MinBenchmarkRuns);
        
        // Run additional iterations if needed
        for (int i = 0; i < additionalRuns; i++)
        {
            stopwatch.Restart();
            problem.Solve();
            stopwatch.Stop();
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
        }

        // Final statistics
        var finalStats = CalculateStatistics(times);
        result.MeanTime = finalStats.Mean;
        result.MedianTime = finalStats.Median;
        result.StandardDeviation = finalStats.StandardDeviation;
        result.MinTime = finalStats.Min;
        result.MaxTime = finalStats.Max;
        result.TotalRuns = times.Count;
        result.ConfidenceInterval = CalculateConfidenceInterval(finalStats.StandardDeviation, times.Count, Settings.ConfidenceLevel);
        result.Times = times;

        return result;
    }

    private static (double Mean, double Median, double StandardDeviation, double Min, double Max) CalculateStatistics(List<double> values)
    {
        if (values.Count == 0) return (0, 0, 0, 0, 0);

        var sorted = values.OrderBy(x => x).ToList();
        var mean = values.Average();
        var median = sorted[sorted.Count / 2];
        var min = sorted[0];
        var max = sorted[sorted.Count - 1];
        
        var variance = values.Sum(x => Math.Pow(x - mean, 2)) / values.Count;
        var standardDeviation = Math.Sqrt(variance);

        return (mean, median, standardDeviation, min, max);
    }

    private static int CalculateRequiredRuns(double standardDeviation, double mean, double confidenceLevel)
    {
        if (mean == 0) return 0;
        
        var tValue = GetTValue(confidenceLevel);
        var coefficientOfVariation = standardDeviation / mean;
        var marginOfError = Settings.MarginOfError;
        
        var requiredRuns = (int)Math.Ceiling(Math.Pow(tValue * coefficientOfVariation / marginOfError, 2));
        return Math.Max(0, requiredRuns - Settings.MinBenchmarkRuns);
    }

    private static double CalculateConfidenceInterval(double standardDeviation, int sampleSize, double confidenceLevel)
    {
        if (sampleSize <= 1) return 0;
        
        var tValue = GetTValue(confidenceLevel);
        return tValue * standardDeviation / Math.Sqrt(sampleSize);
    }

    private static double GetTValue(double confidenceLevel)
    {
        // Simple lookup for common confidence levels (approximate for large samples, Z-score)
        if (confidenceLevel >= 0.99) return 2.576;
        if (confidenceLevel >= 0.95) return 1.96;
        if (confidenceLevel >= 0.90) return 1.645;
        return 1.96; // Default to 95%
    }

    public static List<BenchmarkResult> RunAllBenchmarks(Action<int, int>? progressCallback = null)
    {
        var problemCount = ProblemFactory.SolvedProblems();
        
        // Disable console logging to avoid interference with progress bar
        Logger.SetConsoleLogging(false);
        Logger.Info($"Starting benchmark of {problemCount} problems");
        
        var results = new ConcurrentBag<BenchmarkResult>();
        int completedCount = 0;

        try
        {
            if (Settings.EnableParallelExecution)
            {
                var options = new ParallelOptions { MaxDegreeOfParallelism = Settings.MaxParallelThreads };
                Parallel.For(1, problemCount + 1, options, i =>
                {
                    RunSingleBenchmark(i, results);
                    int currentCount = Interlocked.Increment(ref completedCount);
                    progressCallback?.Invoke(currentCount, problemCount);
                });
            }
            else
            {
                for (int i = 1; i <= problemCount; i++)
                {
                    RunSingleBenchmark(i, results);
                    progressCallback?.Invoke(i, problemCount);
                }
            }
        }
        finally
        {
            Logger.Info($"Completed benchmark of {results.Count} problems");
            // Re-enable console logging
            Logger.SetConsoleLogging(true);
        }
        
        return results.OrderBy(r => r.ProblemId).ToList();
    }

    private static void RunSingleBenchmark(int i, ConcurrentBag<BenchmarkResult> results)
    {
        try
        {
            using (Logger.CreateScope($"Problem {i}"))
            {
                var problem = ProblemFactory.CreateProblem(i);
                var result = RunBenchmark(i, problem);
                results.Add(result);

                Logger.Info($"Problem {i}: {result.MeanTime:F3}ms ± {result.ConfidenceInterval:F3}ms ({result.TotalRuns} runs)");
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to benchmark problem {i}", ex);
        }
    }
}
