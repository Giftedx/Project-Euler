namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 43: Sub-string divisibility.
/// Finds the sum of all 0 to 9 pandigital numbers with a specific sub-string divisibility property.
/// Let d1 be the 1st digit, d2 be the 2nd digit, and so on. The sub-string properties are:
/// - d2d3d4 is divisible by 2
/// - d3d4d5 is divisible by 3
/// - d4d5d6 is divisible by 5
/// - d5d6d7 is divisible by 7
/// - d6d7d8 is divisible by 11
/// - d7d8d9 is divisible by 13
/// - d8d9d10 is divisible by 17
/// Further details can be found at https://projecteuler.net/problem=43
/// </summary>
public class Problem043 : Problem {
    /// <summary>
    /// Array of prime divisors for substring checks.
    /// _tests[0]=2 corresponds to d2d3d4, _tests[1]=3 to d3d4d5, and so on.
    /// </summary>
    private readonly int[] _tests = [2, 3, 5, 7, 11, 13, 17];
    /// <summary>
    /// Boolean array used as a frequency map or flag to track used digits (0-9)
    /// during the generation of pandigital numbers. _used[i] is true if digit 'i' is currently used.
    /// </summary>
    private readonly bool[] _used = new bool[10];

    /// <summary>
    /// Calculates the solution for Project Euler Problem 43.
    /// It finds all 0-9 pandigital numbers that satisfy the sub-string divisibility rules
    /// and sums them.
    /// </summary>
    /// <returns>The sum of all such pandigital numbers.</returns>
    public override object Solve() {
        return CalculateSubStringDivisiblePandigitalSum(); // Renamed for clarity
    }

    /// <summary>
    /// Initiates the search for and calculates the sum of all 0 to 9 pandigital numbers
    /// that satisfy the specific sub-string divisibility properties.
    /// It uses a recursive helper method <see cref="BuildPandigitalNumberAndSum"/> to construct and test numbers.
    /// </summary>
    /// <returns>The sum of all qualifying pandigital numbers.</returns>
    private long CalculateSubStringDivisiblePandigitalSum() { // Renamed for clarity
        long totalSum = 0;
        char[] currentNumberBuffer = new char[10]; // Buffer to build the 10-digit pandigital number
        BuildPandigitalNumberAndSum(0, currentNumberBuffer, ref totalSum);
        return totalSum;
    }

    /// <summary>
    /// Recursively constructs 0-9 pandigital numbers digit by digit and checks for sub-string divisibility rules.
    /// If a fully constructed 10-digit number satisfies all rules, it's added to the <paramref name="currentTotalSum"/>.
    /// </summary>
    /// <param name="depth">The current digit position being filled in the <paramref name="numberBuffer"/> (0-indexed, from d1 to d10).</param>
    /// <param name="numberBuffer">A character array used to build the pandigital number one digit at a time.</param>
    /// <param name="currentTotalSum">A reference to a long integer that accumulates the sum of all valid pandigital numbers found.</param>
    private void BuildPandigitalNumberAndSum(int depth, char[] numberBuffer, ref long currentTotalSum) { // Renamed params for clarity
        // Substring divisibility check:
        // When depth is 4, we have d1d2d3d4, so we can check d2d3d4. This corresponds to _tests[0].
        // The check is performed for depth from 4 (d2d3d4) up to 10 (d8d9d10).
        // `depth - 4` maps depth {4..10} to _tests index {0..6}.
        // `depth - 3` is the start index in numberBuffer for the 3-digit substring (e.g., if depth=4, start index is 1 for d2).
        if (depth >= 4) { // No check needed until at least d4 is placed.
            // Extract the 3-digit number: d(depth-2)d(depth-1)d(depth). buffer indices are depth-3, depth-2, depth-1.
            // Example: depth=4 (d4 placed). Substring is d2d3d4. Indices in buffer: 1,2,3. (depth-3=1, depth-2=2, depth-1=3).
            // The bitwise '& 15' or `& 0x0F` converts char digit '0'-'9' to int 0-9.
            int subStringValue = (numberBuffer[depth - 3] & 15) * 100 +
                                 (numberBuffer[depth - 2] & 15) * 10 +
                                 (numberBuffer[depth - 1] & 15);

            if (subStringValue % _tests[depth - 4] != 0) {
                return; // Substring divisibility rule failed, backtrack.
            }
        }

        // Base Case: If a 10-digit number is fully constructed and all prior divisibility checks passed.
        if (depth == 10) {
            currentTotalSum += long.Parse(new string(numberBuffer)); // Add valid pandigital number to sum.
            return;
        }

        // Recursive Step: Try placing the next digit.
        // For the first digit (d1, depth=0), it cannot be '0'. For others, '0' is allowed.
        char startDigit = (depth == 0) ? '1' : '0';
        for (char digitChar = startDigit; digitChar <= '9'; digitChar++) {
            int digitValue = digitChar - '0'; // Convert char '0'-'9' to int 0-9.

            if (!_used[digitValue]) { // If this digit has not been used yet in the current permutation
                _used[digitValue] = true;       // Mark as used
                numberBuffer[depth] = digitChar; // Place digit in current position

                BuildPandigitalNumberAndSum(depth + 1, numberBuffer, ref currentTotalSum); // Recurse for next digit

                _used[digitValue] = false;      // Backtrack: unmark digit for use in other permutations
            }
        }
    }
}