namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 1: Multiples of 3 or 5.
/// Finds the sum of all the multiples of 3 or 5 below 1000.
/// </summary>
public class Problem001 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 1: Multiples of 3 or 5.
    /// Finds the sum of all the multiples of 3 or 5 below 1000.
    /// </summary>
    /// <returns>The sum of all multiples of 3 or 5 below 1000.</returns>
    public override object Solve() {
        return SumMultiples(3, 5, 1000);
    }

    /// <summary>
    /// Calculates the sum of all multiples of m1 or m2 that are less than n.
    /// This method uses the principle of inclusion-exclusion to avoid double-counting multiples of both m1 and m2.
    /// </summary>
    /// <param name="m1">The first multiple.</param>
    /// <param name="m2">The second multiple.</param>
    /// <param name="n">The upper limit (exclusive).</param>
    /// <returns>The sum of multiples of m1 or m2 below n.</returns>
    private int SumMultiples(int m1, int m2, int n) {
        return SumDivisibleBy(m1, n - 1) +
               SumDivisibleBy(m2, n - 1) -
               SumDivisibleBy(Lcm(m1, m2), n - 1);
    }

    /// <summary>
    /// Calculates the sum of all numbers divisible by m up to a given limit.
    /// It uses the formula for the sum of an arithmetic series.
    /// </summary>
    /// <param name="m">The divisor.</param>
    /// <param name="limit">The upper limit (inclusive).</param>
    /// <returns>The sum of all numbers divisible by m up to the limit.</returns>
    private int SumDivisibleBy(int m, int limit) {
        int p = limit / m;
        return m * p * (p + 1) / 2;
    }

    /// <summary>
    /// Calculates the Least Common Multiple (LCM) of two integers a and b.
    /// Uses the formula LCM(a,b) = |a*b| / GCD(a,b).
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    /// <returns>The LCM of a and b.</returns>
    private int Lcm(int a, int b) {
        return a / Library.Gcd(a, b) * b;
    }
}