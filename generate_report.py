import json
import os

def read_file(filepath):
    if os.path.exists(filepath):
        with open(filepath, 'r') as f:
            return f.read()
    return ""

source_files = []
# Core files
core_files = [
    "Program.cs", "ProblemSolver.cs", "ProblemFactory.cs", "Library.cs",
    "InputHandler.cs", "OutputHandler.cs", "SolutionVerifier.cs",
    "BenchmarkRunner.cs", "Logger.cs", "Configuration.cs", "MemoryEfficientCache.cs",
    "Problem.cs"
]

# Problem files
problem_files = [f"Problem{i:03d}.cs" for i in range(1, 51)]

all_files = core_files + problem_files

for filename in all_files:
    content = read_file(filename)
    if content:
        source_files.append({
            "filename": filename,
            "language": "C#",
            "notes": "Added XML documentation.",
            "annotated_code": content
        })

readme_content = read_file("README.md")

report = {
    "architectural_summary": "The project uses a Factory pattern (ProblemFactory) to instantiate Problem objects based on ID. A central ProblemSolver driver handles execution and benchmarking (ProblemSolver, BenchmarkRunner). Utilities like caching (MemoryEfficientCache) and mathematics (Library) are decoupled.",
    "source_code": source_files,
    "README": {
        "status": "updated",
        "notes": "Expanded architecture and usage sections.",
        "content": readme_content
    },
    "self_review": "Comprehensive XML documentation added to all core and problem files. Logic preserved exactly as found in the repository state. README updated to reflect the modular architecture."
}

with open("documentation_report.json", "w") as f:
    json.dump(report, f, indent=2)

print("Report generated.")
