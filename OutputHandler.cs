using System.Text;
using System.Text.Json;

namespace Project_Euler;

internal static class OutputHandler {
    public const string LogFile = "log.txt";
    private const string JsonFile = "benchmark.json";
    private const string HtmlFile = "benchmark.html";
    private const string HtmlTemplate = "template.html";
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static void GenerateFullReport(List<ProblemData> results, BenchmarkData testData) {
        double sumOfAverageProblemTimes = 0;
        if (results.Any()) {
            // Ensure Times list is not empty before calling Average to prevent InvalidOperationException
            sumOfAverageProblemTimes = results.Sum(r => r.Times.Any() ? r.Times.Average() : 0.0);
        }

        // Note: testData.TotalTime is the total wall clock time for the benchmark run.
        // testData.SlowestTime is the average time of the slowest problem (calculated in ProblemSolver).

        WriteBenchmarkReport(results, testData, sumOfAverageProblemTimes);
        WriteBenchmarkJson(results, testData, sumOfAverageProblemTimes);
        WriteBenchmarkHtml(results, testData, sumOfAverageProblemTimes);
    }

    private static void WriteBenchmarkReport(List<ProblemData> results, BenchmarkData testData, double sumOfAverageProblemTimes) {
        var fileContent = new StringBuilder();

        foreach (var result in results) {
            // Ensure Times list is not empty to prevent InvalidOperationException
            double best = result.Times.Any() ? result.Times.Min() : 0.0;
            double worst = result.Times.Any() ? result.Times.Max() : 0.0;
            double avg = result.Times.Any() ? result.Times.Average() : 0.0;

            fileContent.AppendLine($"Problem {result.Index:D2}: {result.Result}");
            fileContent.AppendLine($"    Best:   {best:F3} ms");
            fileContent.AppendLine($"    Worst:  {worst:F3} ms");
            fileContent.AppendLine($"    Avg:    {avg:F3} ms");
            fileContent.AppendLine();
        }

        fileContent.AppendLine($"Total wall-clock benchmark time: {testData.TotalTime:F3} ms");
        if (results.Any()) {
            fileContent.AppendLine($"Sum of average problem solution times: {sumOfAverageProblemTimes:F3} ms");
            fileContent.AppendLine($"Average problem solution time: {(results.Count > 0 ? sumOfAverageProblemTimes / results.Count : 0):F3} ms");
        } else {
            fileContent.AppendLine($"Sum of average problem solution times: 0.000 ms");
            fileContent.AppendLine($"Average problem solution time: 0.000 ms");
        }
        fileContent.AppendLine($"Slowest Problem (by avg time): {testData.SlowestProblem} with {testData.SlowestTime:F3} ms");
        File.WriteAllText(LogFile, fileContent.ToString());
    }

    private static void WriteBenchmarkJson(List<ProblemData> results, BenchmarkData testData, double sumOfAverageProblemTimes) {
        var summaryJson = new {
            totalProblems = results.Count,
            totalWallClockTimeMs = testData.TotalTime, // Renamed for clarity
            sumOfAverageProblemTimesMs = sumOfAverageProblemTimes, // Added
            averageProblemSolutionTimeMs = results.Count > 0 ? sumOfAverageProblemTimes / results.Count : 0, // Renamed and logic updated
            slowestProblem = new {
                index = testData.SlowestProblem,
                averageTimeMs = testData.SlowestTime // Renamed for clarity
            }
        };

        var problemsJson = results.Select(r => new {
            index = r.Index,
            result = r.Result,
            // times = r.Times, // Optionally include all times if needed, or just stats
            bestTimeMs = r.Times.Any() ? r.Times.Min() : 0.0,
            worstTimeMs = r.Times.Any() ? r.Times.Max() : 0.0,
            averageTimeMs = r.Times.Any() ? r.Times.Average() : 0.0
        });
        
        var jsonOutput = new {
            summary = summaryJson,
            problems = problemsJson
        };

        string json = JsonSerializer.Serialize(jsonOutput, JsonOptions);
        File.WriteAllText(JsonFile, json);
    }

    private static void WriteBenchmarkHtml(List<ProblemData> results, BenchmarkData testData, double sumOfAverageProblemTimes) {
        var problemsHtml = results.Select(r => new {
            index = r.Index,
            result = r.Result,
            // times = r.Times, // Optionally include all times
            bestTimeMs = r.Times.Any() ? r.Times.Min() : 0.0,
            worstTimeMs = r.Times.Any() ? r.Times.Max() : 0.0,
            averageTimeMs = r.Times.Any() ? r.Times.Average() : 0.0
        }).ToList();

        var summaryHtml = new {
            totalProblems = results.Count,
            totalWallClockTimeMs = testData.TotalTime, // Renamed
            sumOfAverageProblemTimesMs = sumOfAverageProblemTimes, // Added
            averageProblemSolutionTimeMs = results.Count > 0 ? sumOfAverageProblemTimes / results.Count : 0, // Renamed and logic updated
            slowestProblem = new {
                index = testData.SlowestProblem,
                averageTimeMs = testData.SlowestTime // Renamed
            }
        };

        string jsonData = JsonSerializer.Serialize(new { summary = summaryHtml, problems = problemsHtml });
        string htmlTemplate = File.ReadAllText(HtmlTemplate);
        string finalHtml = htmlTemplate.Replace("{{DATA}}", jsonData);

        File.WriteAllText(HtmlFile, finalHtml);
    }
}