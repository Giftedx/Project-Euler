using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Project_Euler;

public static class SolutionVerifier
{
    private static readonly Dictionary<int, string> KnownAnswers = new();
    private const string AnswersFile = "known_answers.json";

    static SolutionVerifier()
    {
        LoadKnownAnswers();
    }

    private static void LoadKnownAnswers()
    {
        try
        {
            // First, try loading from the external file
            if (File.Exists(AnswersFile))
            {
                string json = File.ReadAllText(AnswersFile);
                if (TryLoadFromJson(json))
                {
                    return;
                }
            }

            // Fallback: Load from Embedded Resource
            var assembly = Assembly.GetExecutingAssembly();
            // The resource name is defined in .csproj as "Project_Euler.known_answers.json"
            using Stream? stream = assembly.GetManifestResourceStream("Project_Euler.known_answers.json");
            if (stream != null)
            {
                using StreamReader reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                if (TryLoadFromJson(json))
                {
                    return;
                }
            }

            Console.WriteLine($"Warning: Could not load known answers from file or embedded resource.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Error loading known answers: {ex.Message}");
        }
    }

    private static bool TryLoadFromJson(string json)
    {
        try
        {
            var answers = JsonSerializer.Deserialize<Dictionary<int, string>>(json);
            if (answers != null)
            {
                foreach (var kvp in answers)
                {
                    KnownAnswers[kvp.Key] = kvp.Value;
                }
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
        }
        return false;
    }

    public static bool VerifySolution(int problemId)
    {
        if (!KnownAnswers.TryGetValue(problemId, out var expectedAnswer))
        {
            Console.WriteLine($"Warning: No known answer for Problem {problemId}. Cannot verify.");
            return false;
        }

        Problem? problemInstance = null;
        try
        {
            problemInstance = ProblemFactory.CreateProblem(problemId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating instance for Problem {problemId}: {ex.Message}");
            return false;
        }

        if (problemInstance == null)
        {
            Console.WriteLine($"Error: Could not create instance for Problem {problemId} (returned null).");
            return false;
        }
        
        object? actualResultObject = null;
        string actualResultString = string.Empty;
        
        try
        {
            var stopwatch = Stopwatch.StartNew();
            actualResultObject = problemInstance.Solve();
            stopwatch.Stop();
            actualResultString = actualResultObject?.ToString() ?? string.Empty;

            if (actualResultString == expectedAnswer)
            {
                Console.WriteLine($"Problem {problemId}: Correct. (Result: {actualResultString}, Time: {stopwatch.ElapsedMilliseconds} ms)");
                return true;
            }
            else
            {
                Console.WriteLine($"Problem {problemId}: Incorrect. (Expected: {expectedAnswer}, Actual: {actualResultString}, Time: {stopwatch.ElapsedMilliseconds} ms)");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error solving Problem {problemId}: {ex.Message}");
            Console.WriteLine($"Actual result at point of error (if any): '{actualResultString}'");
            return false;
        }
    }

    public static void VerifyAllKnownSolutions()
    {
        int verifiedCount = 0;
        int failedCount = 0;
        var problemIdsToVerify = new List<int>(KnownAnswers.Keys);
        problemIdsToVerify.Sort(); // Verify in order

        Console.WriteLine($"Starting verification for {problemIdsToVerify.Count} problems with known answers...");

        foreach (int problemId in problemIdsToVerify)
        {
            if (VerifySolution(problemId))
            {
                verifiedCount++;
            }
            else
            {
                failedCount++;
            }
        }

        Console.WriteLine($"Verification Complete. {verifiedCount} correct, {failedCount} failed (out of {problemIdsToVerify.Count} checked).");
    }
    
    public static void AddKnownAnswer(int problemId, string answer)
    {
        KnownAnswers[problemId] = answer;
    }
}
