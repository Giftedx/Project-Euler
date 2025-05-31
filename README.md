# Project Euler C# Solver

This project is a C# application designed to solve problems from [Project Euler](https://projecteuler.net/). It provides a framework for adding new problem solutions, benchmarking them, verifying their correctness, and generating reports.

## Features

*   **Problem Solving:** Implements solutions for a growing number of Project Euler problems.
*   **Benchmarking:** Allows benchmarking of individual problems or all solved problems to measure performance.
*   **Solution Verification:** Checks solutions against a list of known correct answers.
*   **Reporting:** Generates detailed benchmark reports in text, JSON, and interactive HTML formats.
*   **Dynamic Problem Discovery:** Uses reflection to automatically discover and instantiate problem classes.

## Getting Started

### Prerequisites

*   .NET SDK (Version 8.0 or higher recommended, as per the `.csproj` file).

### Building the Project

1.  Clone the repository.
2.  Navigate to the root directory of the project.
3.  Run the build command:
    ```bash
    dotnet build
    ```

### Running the Application

1.  After building, run the application from the root directory:
    ```bash
    dotnet run
    ```
    Alternatively, you can run the executable directly from the output directory (e.g., `bin/Debug/net8.0/Project Euler`).

## Menu Navigation

Upon running the application, you will be presented with a menu:

*   **Enter 'a' to solve all problems:** This option will run the solver for all implemented problems, benchmark their performance, and generate output reports (`log.txt`, `benchmark.json`, `benchmark.html`).
*   **Enter 't' to verify all known solutions:** This option will run the `SolutionVerifier` to check all problems that have known answers stored within the system. It will output whether each solution is correct or incorrect.
*   **Enter Problem to solve (1 - N):** You can enter a specific problem number to solve and benchmark that single problem. The range of available problems (N) will be displayed.

After an action is completed, you'll be prompted to press any key to run again or Space to exit.

## Adding a New Problem

To add a solution for a new Project Euler problem (e.g., Problem X):

1.  **Create the Problem Class:**
    *   In the root directory, create a new C# file named `ProblemXXX.cs` (e.g., `Problem051.cs`).
    *   The class should inherit from `Project_Euler.Problem` and implement the `Solve()` method.
    *   Example structure:
        ```csharp
        namespace Project_Euler;

        public class ProblemXXX : Problem
        {
            public override object Solve()
            {
                // Your solution logic here
                // return the_solution;
            }
        }
        ```
    *   Add an XML comment to the class briefly describing the Project Euler problem.
    *   Add an XML comment to the `Solve()` method if the solution approach is non-trivial.

2.  **Add the Solution to Verifier:**
    *   Calculate or find the correct answer to the problem.
    *   Open `SolutionVerifier.cs`.
    *   Add a new entry to the `KnownAnswers` dictionary:
        ```csharp
        { XXX, "YourAnswerAsString" },
        ```

3.  **Add a Unit Test:**
    *   In the `ProjectEuler.Tests` directory, create a new C# file named `ProblemXXXTests.cs` (e.g., `Problem051Tests.cs`).
    *   Use `Problem001Tests.cs` as a template.
    *   Update the class name, test method name, problem instantiation (`new ProblemXXX()`), and the `expectedSolution` variable with the correct answer.
    *   Example:
        ```csharp
        // In ProjectEuler.Tests/ProblemXXXTests.cs
        using Microsoft.VisualStudio.TestTools.UnitTesting;
        using Project_Euler;

        namespace ProjectEuler.Tests
        {
            [TestClass]
            public class ProblemXXXTests
            {
                [TestMethod]
                public void TestProblemXXX_Solution() // Update XXX
                {
                    var problem = new ProblemXXX(); // Update XXX
                    object expectedSolution = YYY; // Replace YYY with the actual answer, ensure correct type or use string
                    // For numeric answers, you can use:
                    // long expectedSolution = 12345L;
                    // string expectedSolution = "verylargenumber";

                    var actualSolution = problem.Solve();
                    Assert.AreEqual(expectedSolution.ToString(), actualSolution.ToString(), $"The solution for Problem XXX is incorrect.");
                }
            }
        }
        ```
        *(Note: Using `.ToString()` for assertion provides robustness if `Solve()` returns various numeric types like int, long, BigInteger).*

## Reporting

The application generates the following reports in the execution directory (e.g., `bin/Debug/netX.Y/`) when the "solve all" option is used:

*   `log.txt`: A text-based summary of results and timings.
*   `benchmark.json`: A JSON file containing detailed benchmark data.
*   `benchmark.html`: An interactive HTML report with charts and sortable tables, visualizing the benchmark data from `benchmark.json`. It uses `template.html` as its basis.
