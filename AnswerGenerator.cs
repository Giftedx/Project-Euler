using System;
using System.Diagnostics;

namespace Project_Euler
{
    public class AnswerGenerator
    {
        public static void Generate(string[] args)
        {
            int startId = 1;
            int endId = 50;

            if (args.Length == 2)
            {
                if (!int.TryParse(args[0], out startId) || !int.TryParse(args[1], out endId))
                {
                    Console.Error.WriteLine("Invalid arguments. Usage: AnswerGenerator <startId> <endId>");
                    return;
                }
            }

            for (int i = startId; i <= endId; i++)
            {
                try
                {
                    Problem? problemInstance = ProblemFactory.CreateProblem(i);
                    if (problemInstance == null)
                    {
                        Console.Error.WriteLine($"Error: Could not create instance for Problem {i} (returned null).");
                        continue;
                    }

                    object? solution = problemInstance.Solve();
                    string? solutionString = solution?.ToString();

                    if (solutionString != null)
                    {
                        Console.WriteLine($"{i}:{solutionString}");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Error: Solution for Problem {i} was null or could not be converted to string.");
                    }
                }
                catch (NotImplementedException)
                {
                    Console.Error.WriteLine($"Error: Problem {i} is not implemented.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error solving Problem {i}: {ex.GetType().Name} - {ex.Message}");
                }
            }
        }
    }
}
