using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            if (File.Exists(AnswersFile))
            {
                string json = File.ReadAllText(AnswersFile);
                var answers = JsonSerializer.Deserialize<Dictionary<int, string>>(json);
                if (answers != null)
                {
                    foreach (var kvp in answers)
                    {
                        KnownAnswers[kvp.Key] = kvp.Value;
                    }
                }
            }
            else
            {
                // Fallback to hardcoded answers for backward compatibility
                LoadDefaultAnswers();
                SaveKnownAnswers(); // Save to file for future use
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Could not load answers from file: {ex.Message}");
            LoadDefaultAnswers();
        }
    }

    private static void LoadDefaultAnswers()
    {
        // Legacy hardcoded answers
        var defaultAnswers = new Dictionary<int, string>
        {
            { 1, "233168" },
            { 2, "4613732" },
            { 3, "6857" },
            { 4, "906609" },
            { 5, "232792560" },
            { 6, "25164150" },
            { 7, "104743" },
            { 8, "23514624000" },
            { 9, "31875000" },
            { 10, "142913828922" },
            { 11, "70600674" },
            { 12, "76576500" },
            { 13, "5537376230" },
            { 14, "837799" },
            { 15, "137846528820" },
            { 16, "1366" },
            { 17, "21124" },
            { 18, "1074" },
            { 19, "171" },
            { 20, "648" },
            { 21, "31626" },
            { 22, "871198282" },
            { 23, "4179871" },
            { 24, "2783915460" },
            { 25, "4782" },
            { 26, "983" },
            { 27, "-59231" },
            { 28, "669171001" },
            { 29, "9183" },
            { 30, "443839" },
            { 31, "73682" },
            { 32, "45228" },
            { 33, "100" },
            { 34, "40730" },
            { 35, "55" },
            { 36, "872187" },
            { 37, "748317" },
            { 38, "932718654" },
            { 39, "840" },
            { 40, "210" },
            { 41, "7652413" },
            { 42, "162" },
            { 43, "16695334890" },
            { 44, "5482660" },
            { 45, "1533776805" },
            { 46, "5777" },
            { 47, "134043" },
            { 48, "9110846700" },
            { 49, "296962999629" },
            { 50, "997651" }
        };

        foreach (var kvp in defaultAnswers)
        {
            KnownAnswers[kvp.Key] = kvp.Value;
        }
    }

    private static void SaveKnownAnswers()
    {
        try
        {
            string json = JsonSerializer.Serialize(KnownAnswers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(AnswersFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: Could not save answers to file: {ex.Message}");
        }
    }

    public static bool VerifySolution(int problemId)
    {
        if (!KnownAnswers.TryGetValue(problemId, out var expectedAnswer))
        {
            Console.WriteLine($"Warning: No known answer for Problem {problemId}. Cannot verify.");
            return false; // Or throw an exception, or return a specific status
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
            // This case should ideally be caught by the exception in ProblemFactory or the one above,
            // but as a fallback:
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
    
    // Method to add more answers, perhaps from a file or embedded resource later
    public static void AddKnownAnswer(int problemId, string answer)
    {
        KnownAnswers[problemId] = answer;
    }
}
