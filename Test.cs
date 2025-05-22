using System;

namespace Project_Euler;

public static class Test
{
    public static void Solve()
    {
        Console.WriteLine("Running Solution Verifier for problems with known answers...");
        SolutionVerifier.VerifyAllKnownSolutions();
        
        // Example of how to add more answers if needed dynamically (though better to add to KnownAnswers dictionary directly)
        // SolutionVerifier.AddKnownAnswer(4, "906609"); 
        // Console.WriteLine("\nRunning verification for Problem 4 after adding its answer:");
        // SolutionVerifier.VerifySolution(4);
    }
}