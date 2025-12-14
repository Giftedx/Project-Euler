using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project_Euler.Tests;

[TestClass]
public class AllProblemsTests
{
    private static Dictionary<int, string> _knownAnswers = new();

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        string json = File.ReadAllText("known_answers.json");
        _knownAnswers = JsonSerializer.Deserialize<Dictionary<int, string>>(json) ?? new Dictionary<int, string>();
    }

    public static IEnumerable<object[]> GetProblems()
    {
        // Load answers if not already loaded (for safety, though ClassInit should handle it)
        if (_knownAnswers == null || _knownAnswers.Count == 0)
        {
            string json = File.ReadAllText("known_answers.json");
            _knownAnswers = JsonSerializer.Deserialize<Dictionary<int, string>>(json) ?? new Dictionary<int, string>();
        }

        foreach (var kvp in _knownAnswers)
        {
            yield return new object[] { kvp.Key, kvp.Value };
        }
    }

    [DataTestMethod]
    [DynamicData(nameof(GetProblems), DynamicDataSourceType.Method)]
    public void VerifyProblemSolution(int problemId, string expectedAnswer)
    {
        // Act
        var problem = ProblemFactory.CreateProblem(problemId);
        object result = problem.Solve();
        string resultString = result?.ToString() ?? string.Empty;

        // Assert
        Assert.AreEqual(expectedAnswer, resultString, $"Problem {problemId} failed.");
    }
}
