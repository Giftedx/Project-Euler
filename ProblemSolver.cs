using System.Diagnostics;

namespace Project_Euler;

public static class ProblemSolver {

    private static readonly object _consoleLock = new object();

    public static void IndividualBenchmark(string problemIdStr) {
        if (!int.TryParse(problemIdStr, out int problemId))
        {
            Console.WriteLine($"Invalid problem ID: {problemIdStr}");
            return;
        }

        try
        {
            var problem = ProblemFactory.CreateProblem(problemId);
            var result = BenchmarkRunner.RunBenchmark(problemId, problem);

            Console.WriteLine($"Problem {problemId}: {result.Result}");
            Console.WriteLine($"Mean Time: {result.MeanTime:F3} ms");
            Console.WriteLine($"Confidence: ±{result.ConfidenceInterval:F3} ms");
            Console.WriteLine($"Runs: {result.TotalRuns} (Min: {result.MinTime:F3} ms, Max: {result.MaxTime:F3} ms)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error solving problem {problemId}: {ex.Message}");
            Logger.Error($"Error solving problem {problemId}", ex);
        }
    }

    public static void FullBenchmark() {
        var watch = Stopwatch.StartNew();

        // Use a progress reporting mechanism
        var results = BenchmarkRunner.RunAllBenchmarks((completed, total) => {
             DisplayProgressBar(completed, total);
        });

        watch.Stop();
        Console.WriteLine(); // New line after progress bar

        var testData = new BenchmarkData
        {
            TotalTime = watch.Elapsed.TotalMilliseconds
        };

        if (results.Any()) {
            var slowestProblem = results.OrderByDescending(r => r.MeanTime).First();
            testData.SlowestProblem = slowestProblem.ProblemId;
            testData.SlowestTime = slowestProblem.MeanTime;
        }

        // Convert BenchmarkResult to ProblemData for OutputHandler compatibility
        var problemDataList = results.Select(r => new ProblemData(r.ProblemId, r.TotalRuns)
        {
            Result = r.Result,
            // ProblemData.Times is a List<double>. We need to populate it.
        }).ToList();

        // Manually copy times
        for(int i=0; i<results.Count; i++)
        {
            problemDataList[i].Times.AddRange(results[i].Times);
        }

        OutputHandler.GenerateFullReport(problemDataList, testData);
        Console.WriteLine($"Results output to {OutputHandler.LogFile}, {watch.ElapsedMilliseconds} ms total");
    }

    private static void DisplayProgressBar(int completed, int total) {
        lock (_consoleLock)
        {
            const int width = 50;
            double percent = (double)completed / total;
            int filled = (int)(percent * width);
            string bar = new string('█', filled) + new string('░', width - filled);

            try
            {
                // Ensure we don't crash if console is redirected or not available
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"[{bar}] {percent * 100:F1}%");
            }
            catch (IOException)
            {
                // Ignore console errors (e.g. if redirected)
            }
        }
    }
}
