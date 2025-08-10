# Contributing

Thanks for considering a contribution! This repo solves Project Euler problems in C# and includes benchmarking and verification tools.

## Repository structure
- `src/ProjectEuler` — Console app and core code
- `tests` — MSTest unit tests
- `template.html`, `names.txt`, `words.txt` — Runtime assets copied to output

## Prerequisites
- .NET SDK 8.0+

## Build & Run
- Build all: `dotnet build ProjectEuler.sln`
- Run app: `dotnet run --project src/ProjectEuler/ProjectEuler.csproj`
- Run tests: `dotnet test ProjectEuler.sln`

## Adding a new problem
1. Create `ProblemXXX.cs` in the source (kept under `src/ProjectEuler` via link include from repo root). Use existing problems as examples. Inherit from `Project_Euler.Problem` (legacy) for compatibility with current harness.
2. Add expected answer to `known_answers.json` (auto-generated on first run) or extend `SolutionVerifier.LoadDefaultAnswers()`.
3. Register the problem in `ProblemFactory.InitializeProblemRegistry()` via `RegisterProblem<ProblemXXX>();`.
4. Add a test: create `tests/ProblemXXXTests.cs` using an existing test as a template.

## Style
- Follow existing naming and layout; prefer clear, descriptive identifiers
- Avoid deep nesting; use early returns
- Keep comments focused on "why" for complex logic

## Benchmarks
- Solve all and generate reports from the app menu (option `a`)
- HTML report is generated from `template.html`

## Commits & PRs
- Keep edits focused; include rationale in the PR description
- Ensure `dotnet build` and `dotnet test` pass

## Notes
- `Configuration.cs` stores runtime settings in `euler_config.json`
- Logs are written to `euler_solver.log`