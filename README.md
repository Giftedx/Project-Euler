# Project Euler C# Solver

This repository contains a C# solution framework for solving and benchmarking problems from [Project Euler](https://projecteuler.net/). It is designed to be efficient, extensible, and easy to navigate, with a focus on clean code and performance analysis.

## Architecture

The project is structured around a central `Problem` base class, allowing for a consistent interface to solve and benchmark any implemented problem.

### Key Components

*   **`Problem` / `Problem<T>`**: The abstract base classes that all problem solutions inherit from. They define the `Solve()` contract.
*   **`ProblemFactory`**: A static factory that manages the registration and instantiation of problem classes. It uses explicit registration for type safety and performance.
*   **`ProblemSolver`**: The core engine that runs problems. It handles individual execution and full benchmarking suites, collecting timing data.
*   **`Library`**: A comprehensive utility class containing reusable mathematical functions (primes, GCD, permutations, combinatorics) and helper methods used across multiple problems.
*   **`BenchmarkRunner`**: Provides advanced statistical analysis of problem performance, including mean execution time, standard deviation, and confidence intervals.
*   **`SolutionVerifier`**: Validates the output of `Solve()` methods against a set of known correct answers (`known_answers.json`), ensuring correctness after code refactoring or optimization.
*   **`Configuration`**: A singleton manager for application settings, including logging levels and benchmark parameters, loaded from `euler_config.json`.
*   **`MemoryEfficientCache`**: specialized caching implementations for memory-intensive operations (e.g., bit arrays for prime sieves, sparse dictionaries for Collatz sequences).

### Workflows

1.  **Solving Problems**: Users interact with the console menu (`Program.cs`) to solve a specific problem or all problems.
2.  **Benchmarking**: The `ProblemSolver` executes problems multiple times to gather stable timing metrics, which are then processed by `OutputHandler` to generate reports.
3.  **Reporting**: Results are output to the console, a text log (`log.txt`), a raw JSON data file (`benchmark.json`), and an interactive HTML report (`benchmark.html`).

## Getting Started

### Prerequisites

*   .NET SDK 8.0 or higher.

### Building and Running

1.  **Build** the solution from the root directory:
    ```bash
    dotnet build src/ProjectEuler/ProjectEuler.csproj
    ```

2.  **Run** the application:
    ```bash
    dotnet run --project src/ProjectEuler/ProjectEuler.csproj
    ```

## Usage

Upon launching, the interactive menu offers the following options:

*   **Enter 'a'**: Solves all registered problems, runs the benchmark suite, and generates full reports.
*   **Enter 't'**: Verifies all currently implemented solutions against known correct answers.
*   **Enter Problem Number (e.g., '1', '50')**: Solves the specified problem and displays the result and execution time.

## Adding a New Solution

1.  Create a new class file (e.g., `Problem051.cs`) inheriting from `Problem`.
2.  Implement the `Solve()` method with your logic.
3.  Register the new class in `ProblemFactory.cs` inside the `InitializeProblemRegistry` method.
4.  (Optional) Add the known correct answer to `SolutionVerifier.cs` or `known_answers.json` for verification.

## Documentation

The codebase is fully documented with XML docstrings.
*   **Core Logic**: See `Library.cs` for math utilities.
*   **Problem Solutions**: Each `ProblemXXX.cs` file contains logic specific to that problem.
