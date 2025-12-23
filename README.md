# Project Euler C# Solver

This project is a C# application designed to solve problems from [Project Euler](https://projecteuler.net/). It provides a framework for adding new problem solutions, benchmarking them, verifying their correctness, and generating reports.

## Project Goals

*   **Primary Goal:** Provide a performant and structured C# framework for implementing, benchmarking, and verifying Project Euler solutions.
*   **Target Users:** Developers practicing algorithms and C# enthusiasts who want to track solution performance and correctness.
*   **Key Features:**
    *   **Automated Benchmarking:** Statistical analysis of execution time with JSON/HTML reporting.
    *   **Solution Verification:** Built-in validation against known correct answers.
    *   **Interactive CLI:** Menu-driven interface for running individual or batch problems.
*   **Non-Goals:** This is a CLI-only tool, not a GUI application or a general-purpose math library.

## Features

*   **Problem Solving:** Implements solutions for a growing number of Project Euler problems.
*   **Benchmarking:** Benchmark individual problems or all solved problems; advanced stats available via `BenchmarkRunner`.
*   **Solution Verification:** Checks solutions against a list of known correct answers (from `known_answers.json` or built-in defaults).
*   **Reporting:** Generates detailed benchmark reports in text, JSON, and interactive HTML formats (HTML is generated from `template.html`).
*   **Explicit Problem Registration:** Problems are explicitly registered in `ProblemFactory` for reliability and performance.

## Installation

### Prerequisites

*   **.NET SDK 8.0+**: [Download .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) or use the included script:
    ```bash
    ./dotnet-install.sh
    ```

### Setup

1.  **Clone the repository:**
    ```bash
    git clone <repository_url>
    cd <repository_directory>
    ```
2.  **Build the solution:**
    ```bash
    dotnet build ProjectEuler.sln
    ```

### Configuration

*   **Logging:** Configured via `euler_config.json` (auto-generated on first run if missing).
*   **Data Files:** `names.txt` and `words.txt` must be present in the root (automatically copied to output on build).

### Verification

1.  **Run Unit Tests:**
    ```bash
    dotnet test
    ```
2.  **Smoke Check (Verify Known Solutions):**
    ```bash
    dotnet run --project src/ProjectEuler/ProjectEuler.csproj -- verify
    ```

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
    *   In the `tests/` directory, create a new C# file named `ProblemXXXTests.cs` (e.g., `tests/Problem051Tests.cs`).
    *   Use any existing test (e.g., `tests/Problem010Tests.cs`) as a template.
    *   Update the class name, test method name, problem instantiation (`new ProblemXXX()`), and the `expectedSolution` variable with the correct answer.
    *   Run tests:
        ```bash
        dotnet test ProjectEuler.sln
        ```
    *   Example:
        ```csharp
        // In tests/ProblemXXXTests.cs
        using Microsoft.VisualStudio.TestTools.UnitTesting;
        using Project_Euler;

        namespace Project_Euler
        {
            [TestClass]
            public class ProblemXXXTests
            {
                [TestMethod]
                public void TestProblemXXX_Solution() // Update XXX
                {
                    var problem = new ProblemXXX(); // Update XXX
                    object expectedSolution = YYY; // Replace YYY with the actual answer, ensure correct type or use string
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
