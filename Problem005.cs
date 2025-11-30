namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 5: Smallest multiple.
/// Finds the smallest positive number that is evenly divisible by all of the numbers from 1 to 20.
/// </summary>
public class Problem005 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 5: Smallest multiple.
    /// Finds the smallest positive number that is evenly divisible by all of the numbers from 1 to 20.
    /// </summary>
    /// <returns>The smallest multiple of numbers from 1 to 20.</returns>
    public override object Solve() {
        return SmallestMultiple(20);
    }

    /// <summary>
    /// Calculates the smallest number divisible by all numbers from 1 to k.
    /// This is equivalent to finding the Least Common Multiple (LCM) of the numbers 1, 2, ..., k.
    /// LCM(1, ..., k) = LCM(LCM(1, ..., k-1), k).
    /// </summary>
    /// <param name="k">The upper limit of the range [1, k].</param>
    /// <returns>The smallest multiple of 1 through k.</returns>
    private long SmallestMultiple(int k) {
        long lcm = 1;
        for (int i = 2; i <= k; i++) {
            lcm = Lcm(lcm, i);
        }
        return lcm;
    }

    /// <summary>
    /// Calculates the Least Common Multiple (LCM) of two numbers a and b.
    /// LCM(a, b) = |a * b| / GCD(a, b).
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>The LCM of a and b.</returns>
    private long Lcm(long a, long b) {
        // Use Library.Gcd but handle long types
        // GCD(a, b) fits in int if inputs were int, but here inputs are long.
        // We can implement a local LongGcd or cast if we are sure of range.
        // For Problem 5, result fits in long, inputs are small.
        // Let's implement LongGcd locally to be safe.
        return (a * b) / Gcd(a, b);
    }

    /// <summary>
    /// Calculates GCD of two long integers.
    /// </summary>
    private long Gcd(long a, long b) {
        while (b != 0) {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }
}
