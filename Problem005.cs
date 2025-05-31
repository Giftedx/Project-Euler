namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 5: Smallest multiple.
/// Further details can be found at https://projecteuler.net/problem=5
/// </summary>
public class Problem005 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 5: Smallest multiple.
    /// Finds the smallest positive number that is evenly divisible by all of the numbers from 1 to 20.
    /// </summary>
    /// <returns>The smallest positive number evenly divisible by all numbers from 1 to 20, as a string.</returns>
    public override object Solve() {
        return MinimumEvenlyDivisibleByRange(1, 20);
    }

    /// <summary>
    /// Calculates the smallest positive number that is evenly divisible by all integers in the range [min, max].
    /// It does this by iteratively calculating the Least Common Multiple (LCM).
    /// The result starts at 1, and for each number 'i' in the range, it updates the result to LCM(result, i).
    /// </summary>
    /// <param name="min">The minimum number in the range (inclusive).</param>
    /// <param name="max">The maximum number in the range (inclusive).</param>
    /// <returns>A string representation of the smallest number evenly divisible by all numbers in the specified range.</returns>
    private string MinimumEvenlyDivisibleByRange(int min, int max) {
        ulong result = 1;
        for (int i = min; i <= max; i++) result = LeastCommonMultiple((ulong)i, result);
        return result.ToString();
    }

    /// <summary>
    /// Calculates the Least Common Multiple (LCM) of two unsigned long integers 'a' and 'b'.
    /// Uses the formula: LCM(a, b) = (|a * b|) / GCD(a, b).
    /// Since a and b are unsigned, |a*b| is just a*b.
    /// </summary>
    /// <param name="a">The first unsigned long integer.</param>
    /// <param name="b">The second unsigned long integer.</param>
    /// <returns>The LCM of a and b as an unsigned long integer.</returns>
    private ulong LeastCommonMultiple(ulong a, ulong b) {
        if (a == 0 || b == 0) return 0; // LCM with 0 is typically 0
        // Prevent overflow by dividing first, then multiplying: (a / GCD(a,b)) * b
        return (a / GreatestCommonDivisor(a, b)) * b;
    }

    /// <summary>
    /// Calculates the Greatest Common Divisor (GCD) of two unsigned long integers 'a' and 'b'.
    /// Implements the Euclidean algorithm.
    /// </summary>
    /// <param name="a">The first unsigned long integer.</param>
    /// <param name="b">The second unsigned long integer.</param>
    /// <returns>The GCD of a and b as an unsigned long integer.</returns>
    private ulong GreatestCommonDivisor(ulong a, ulong b) {
        while (true) {
            if (a == 0) return b;
            ulong temp_a = a;
            a = b % a;
            b = temp_a;
        }
    }
}