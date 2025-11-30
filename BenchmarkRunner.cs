using System.Diagnostics;

namespace Project_Euler;

/// <summary>
/// Provides advanced benchmarking capabilities with statistical analysis.
/// Calculates mean, median, standard deviation, and confidence intervals for problem execution times.
/// </summary>
public class BenchmarkRunner
{
    private const int WarmupRuns = 10;
    private const int MinBenchmarkRuns = 30;
    private const int MaxBenchmarkRuns = 1000;
    private const double ConfidenceLevel = 0.95; // 95% confidence interval

    /// <summary>
    /// Represents the results of a benchmark run for a specific problem.
    /// </summary>
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

    public static BenchmarkResult RunBenchmark(int problemId, Problem problem)
    {
        var result = new BenchmarkResult { ProblemId = problemId };
        
        // Warm-up phase
        for (int i = 0; i < WarmupRuns; i++)
        {
            problem.Solve();
        }

        // Benchmark phase with adaptive number of runs
        var times = new List<double>();
        var stopwatch = new Stopwatch();
        
        // Initial runs to get a baseline
        for (int i = 0; i < MinBenchmarkRuns; i++)
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
        int additionalRuns = CalculateRequiredRuns(stats.StandardDeviation, stats.Mean, ConfidenceLevel);
        additionalRuns = Math.Min(additionalRuns, MaxBenchmarkRuns - MinBenchmarkRuns);
        
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
        result.ConfidenceInterval = CalculateConfidenceInterval(finalStats.StandardDeviation, times.Count, ConfidenceLevel);
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
        
        // Using t-distribution approximation for sample size calculation
        // For 95% confidence level, t ≈ 1.96
        var tValue = 1.96;
        var coefficientOfVariation = standardDeviation / mean;
        var marginOfError = 0.05; // 5% margin of error
        
        var requiredRuns = (int)Math.Ceiling(Math.Pow(tValue * coefficientOfVariation / marginOfError, 2));
        return Math.Max(0, requiredRuns - MinBenchmarkRuns);
    }

    private static double CalculateConfidenceInterval(double standardDeviation, int sampleSize, double confidenceLevel)
    {
        if (sampleSize <= 1) return 0;
        
        // Using t-distribution for small samples
        var tValue = 1.96; // Approximation for large samples
        return tValue * standardDeviation / Math.Sqrt(sampleSize);
    }

    public static List<BenchmarkResult> RunAllBenchmarks()
    {
        var results = new List<BenchmarkResult>();
        var problemCount = ProblemFactory.SolvedProblems();
        
        Logger.Info($"Starting benchmark of {problemCount} problems");
        
        for (int i = 1; i <= problemCount; i++)
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
        
        Logger.Info($"Completed benchmark of {results.Count} problems");
        return results;
    }
}
