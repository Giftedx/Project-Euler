namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 26: Reciprocal cycles.
/// Finds the value of d < 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.
/// Further details can be found at https://projecteuler.net/problem=26
/// </summary>
public class Problem026 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 26.
    /// It finds the denominator `d` less than 1000 such that the fraction 1/d has the longest
    /// recurring cycle in its decimal representation.
    /// </summary>
    /// <returns>The denominator `d` that produces the longest recurring cycle.</returns>
    public override object Solve() {
        return GetLongestCycleDenominator(1000);
    }

    /// <summary>
    /// Finds the denominator `d` (where `2 <= d < limit`) for which the fraction 1/d has the
    /// longest recurring cycle in its decimal representation.
    /// The method includes optimizations:
    /// 1. Iterates `d` downwards. If the current `maxCycleLength` found is already greater than or equal to `d`,
    ///    no smaller `d` can produce a longer cycle (max cycle length for 1/d is d-1), so the loop breaks.
    /// 2. Skips `d` if it's divisible by 2 or 5, as these factors contribute to non-repeating parts or termination.
    /// 3. Skips `d` if it's not prime (using `Library.IsPrime(d)`). This is a strong heuristic, as the longest
    ///    cycles are typically found when `d` is prime and 10 is a primitive root modulo `d`.
    ///    While composite numbers can have recurring cycles, their length is related to factors.
    /// </summary>
    /// <param name="limit">The upper limit (exclusive) for the denominator `d`.</param>
    /// <returns>The denominator `d` that results in the longest recurring decimal cycle for 1/d.</returns>
    private int GetLongestCycleDenominator(int limit) {
        int maxCycleLength = 0;
        int denominatorWithMaxCycle = 0;

        // Iterate downwards from limit - 1. This allows early exit if maxCycleLength >= d.
        for (int d = limit - 1; d >= 2; d--) {
            // Optimization: If the longest cycle found so far is greater than or equal to the current d,
            // no number smaller than or equal to d can have a longer cycle (max cycle length for 1/d is d-1).
            if (maxCycleLength >= d) {
                break;
            }

            // Optimization: Factors of 2 and 5 in d lead to terminating decimal parts.
            // The problem focuses on the length of the recurring cycle.
            // The condition `!Library.IsPrime(d)` is a heuristic; the longest cycles are often found for prime d.
            // For 1/d, the cycle length is the order of 10 modulo d' (where d' is d after removing factors of 2 and 5).
            if (d % 2 == 0 || d % 5 == 0 || !Library.IsPrime(d)) {
                continue;
            }

            int cycleLength = GetCycleLength(d);
            if (cycleLength > maxCycleLength) { // Note: ">" ensures smaller d is chosen if lengths are equal, due to downward loop.
                                                // However, problem asks for the d, not smallest d. So ">" is fine.
                maxCycleLength = cycleLength;
                denominatorWithMaxCycle = d;
            }
        }
        return denominatorWithMaxCycle;
    }

    /// <summary>
    /// Calculates the length of the recurring cycle in the decimal representation of 1/d.
    /// This is done by simulating the long division process and finding when a remainder repeats.
    /// The length of the cycle is the number of steps (digits) until a remainder is encountered
    /// for the second time. For 1/d, the first remainder in the cycle calculation is effectively 1 (from 10/d, 100/d etc.).
    /// The cycle ends when this remainder 1 is seen again.
    /// If a remainder of 0 is encountered, the decimal terminates, and the cycle length is 0.
    /// </summary>
    /// <param name="d">The denominator of the fraction 1/d. Must be greater than 1.</param>
    /// <returns>The length of the recurring cycle, or 0 if the decimal representation terminates.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if d is less than or equal to 1.</exception>
    private int GetCycleLength(int d) {
        if (d <= 1) {
            throw new ArgumentOutOfRangeException(nameof(d), "Denominator must be greater than 1.");
        }

        // This method relies on finding the order of 10 modulo d,
        // but only for d coprime to 10. If d has factors of 2 or 5, they should be handled by the caller
        // or this method should strip them and calculate for d'.
        // The caller GetLongestCycleDenominator already filters out d divisible by 2 or 5.

        int remainder = 1; // Start with the first effective remainder for 10^k mod d.
        int position = 0;  // Tracks the number of digits in the cycle.

        // We are looking for the smallest k > 0 such that 10^k ≡ 1 (mod d).
        // This is found by tracking remainders of (10^position * 1) % d.
        // The loop simulates (remainder_i * 10) % d = remainder_{i+1}.
        // It starts with remainder_0 = 1.
        do {
            remainder = (remainder * 10) % d; // Calculate next remainder.
            position++;                       // Increment length of cycle.
            // If remainder is 0, decimal terminates.
            // If remainder is 1, the cycle has completed (since we started with an effective remainder of 1).
        } while (remainder != 1 && remainder != 0);

        // If remainder is 0, it's a terminating decimal, so cycle length is 0.
        // Otherwise, position is the length of the cycle.
        return remainder == 0 ? 0 : position;
    }
}