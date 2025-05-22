using System; // Required for Math.Sqrt in the original Tau, though Tau will be removed.

namespace Project_Euler;

public class Problem012 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 12: Highly divisible triangular number.
    /// Finds the value of the first triangle number to have over five hundred divisors.
    /// </summary>
    /// <returns>The value of the first triangle number with over 500 divisors.</returns>
    public override object Solve() {
        return HighlyDivisibleTriangle(500);
    }

    private long HighlyDivisibleTriangle(int minDivisors) {
        // A triangle number T_n = n*(n+1)/2.
        // n and (n+1) are always coprime.
        // The number of divisors function tau(k) is multiplicative for coprime integers: tau(a*b) = tau(a)*tau(b).
        // If n is even, T_n = (n/2) * (n+1). Since n/2 and n+1 are coprime, tau(T_n) = tau(n/2) * tau(n+1).
        // If n is odd, (n+1) is even. T_n = n * ((n+1)/2). Since n and (n+1)/2 are coprime, tau(T_n) = tau(n) * tau((n+1)/2).
        int n = 1;
        while (true) {
            int divisorsCount;
            if (n % 2 == 0) {
                divisorsCount = Library.CountDivisors((long)(n >> 1)) * Library.CountDivisors((long)(n + 1));
            } else {
                divisorsCount = Library.CountDivisors((long)n) * Library.CountDivisors((long)((n + 1) >> 1));
            }

            if (divisorsCount > minDivisors) {
                return ((long)n * (n + 1)) >> 1; // Calculate T_n = n*(n+1)/2
            }
            n++;
        }
    }
}