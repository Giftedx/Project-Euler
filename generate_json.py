import re
import json
import os
import sys

def main():
    """
    Parses 'log.txt' to extract problem results and generates 'known_answers.json'.

    This script is used to bootstrap or restore the known answers file.
    It overrides specific problem IDs with hardcoded correct answers where the current
    implementation is known to be incorrect or incomplete.
    """
    log_file = "log.txt"
    output_file = "known_answers.json"

    # Overrides for problems that are currently failing or known to be incorrect in the local implementation
    # P18 is overridden because the local implementation modifies state, leading to inconsistent results in solve-all vs verify.
    # P30, P35, P44, P45 are overridden because the current logic is buggy.
    # P42 is overridden to match the standard Project Euler answer (requires standard words.txt).
    # P22 is overridden to match the standard Project Euler answer (requires standard names.txt).
    known_overrides = {
        18: "1074",
        30: "443839",
        35: "55",
        42: "162",
        44: "5482660",
        45: "1533776805",
        22: "871198282"
    }

    if not os.path.exists(log_file):
        print(f"Error: '{log_file}' not found. Please run the solver (e.g., 'dotnet run -- solve-all > log.txt') first.")
        sys.exit(1)

    answers = {}

    try:
        with open(log_file, "r") as f:
            content = f.read()

            # Regex to find "Problem XX: RESULT"
            # Matches "Problem 01: 233168"
            matches = re.findall(r"Problem (\d+): ([\d-]+)", content)

            if not matches:
                print("Warning: No problem results found in log file.")

            for problem_id, result in matches:
                pid = int(problem_id)
                if pid in known_overrides:
                    answers[pid] = known_overrides[pid]
                else:
                    answers[pid] = result

        # Ensure overrides are applied even if not found in log (though they should be)
        for pid, ans in known_overrides.items():
            answers[pid] = ans

        print(f"Parsed {len(answers)} answers.")

        with open(output_file, "w") as f:
            json.dump(answers, f, indent=4)

        print(f"Successfully created '{output_file}'.")

    except Exception as e:
        print(f"An error occurred: {e}")
        sys.exit(1)

if __name__ == "__main__":
    main()
