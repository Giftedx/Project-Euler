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
        WriteBenchmarkReport(results, testData);
        WriteBenchmarkJson(results, testData);
        WriteBenchmarkHtml(results, testData);
    }

    private static void WriteBenchmarkReport(List<ProblemData> results, BenchmarkData testData) {
        var fileContent = new StringBuilder();

        foreach (var result in results) {
            double best = result.Times.Min();
            double worst = result.Times.Max();
            double avg = result.Times.Average();

            testData.TotalTime += best;
            if (worst > testData.SlowestTime) {
                testData.SlowestTime = worst;
                testData.SlowestProblem = result.Index;
            }

            fileContent.AppendLine($"Problem {result.Index:D2}: {result.Result}");
            fileContent.AppendLine($"    Best:   {best:F3} ms");
            fileContent.AppendLine($"    Worst:  {worst:F3} ms");
            fileContent.AppendLine($"    Avg:    {avg:F3} ms");
            fileContent.AppendLine();
        }

        fileContent.AppendLine($"Total Time: {testData.TotalTime:F3} ms");
        fileContent.AppendLine($"Average solution time: {testData.TotalTime / results.Count:F3} ms");
        fileContent.AppendLine($"Slowest Problem: {testData.SlowestProblem} with {testData.SlowestTime:F3} ms");
        File.WriteAllText(LogFile, fileContent.ToString());
    }

    private static void WriteBenchmarkJson(List<ProblemData> results, BenchmarkData testData) {
        var jsonOutput = new {
            summary = new {
                totalProblems = results.Count,
                totalTimeMs = testData.TotalTime,
                averageTimeMs = testData.TotalTime / results.Count,
                slowestProblem = new {
                    index = testData.SlowestProblem,
                    timeMs = testData.SlowestTime
                }
            },
            problems = results.Select(r =>
                (index: r.Index, result: r.Result, times: r.Times,
                    bestTimeMs: r.Times.Min(), worstTimeMs: r.Times.Max(),
                    averageTimeMs: r.Times.Average()))
        };


        string json = JsonSerializer.Serialize(jsonOutput, JsonOptions);
        File.WriteAllText(JsonFile, json);
    }

    private static void WriteBenchmarkHtml(List<ProblemData> results, BenchmarkData testData) {
        var problems = results.Select(r => new {
            index = r.Index,
            result = r.Result,
            times = r.Times,
            bestTimeMs = r.Times.Min(),
            worstTimeMs = r.Times.Max(),
            averageTimeMs = r.Times.Average()
        }).ToList();

        var summary = new {
            totalProblems = results.Count,
            totalTimeMs = testData.TotalTime,
            averageTimeMs = testData.TotalTime / results.Count,
            slowestProblem = new {
                index = testData.SlowestProblem,
                timeMs = testData.SlowestTime
            }
        };

        string jsonData = JsonSerializer.Serialize(new { summary, problems });
        string htmlTemplate = File.ReadAllText(HtmlTemplate);
        string finalHtml = htmlTemplate.Replace("{{DATA}}", jsonData);

        File.WriteAllText(HtmlFile, finalHtml);
    }
}