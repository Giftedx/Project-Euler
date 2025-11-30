namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 3: Largest prime factor.
/// Finds the largest prime factor of the number 600851475143.
/// </summary>
public class Problem003 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 3: Largest prime factor.
    /// Finds the largest prime factor of the number 600851475143.
    /// </summary>
    /// <returns>The largest prime factor of 600851475143.</returns>
    public override object Solve() {
        return LargestPrimeFactor(600851475143);
    }

    /// <summary>
    /// Calculates the largest prime factor of a given number n.
    /// It repeatedly divides n by its smallest prime factor until n becomes 1.
    /// The last factor used in the division is the largest prime factor.
    /// </summary>
    /// <param name="n">The number to factorize.</param>
    /// <returns>The largest prime factor of n.</returns>
    private long LargestPrimeFactor(long n) {
        long largestFactor = -1;

        // Check for factor 2
        while (n % 2 == 0) {
            largestFactor = 2;
            n /= 2;
        }

        // Check for odd factors starting from 3
        for (long i = 3; i * i <= n; i += 2) {
            while (n % i == 0) {
                largestFactor = i;
                n /= i;
            }
        }

        // If n is still greater than 2, then n itself is a prime factor
        if (n > 2) {
            largestFactor = n;
        }

        return largestFactor;
    }
}
