using System; // Required for Math.Log and ArgumentOutOfRangeException
using System.Collections.Generic; // Required for List<int>

namespace Project_Euler;

public class Problem007 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 7: 10001st prime.
    /// Finds the 10001st prime number.
    /// </summary>
    /// <returns>The 10001st prime number.</returns>
    public override object Solve() {
        return NthPrime(10001);
    }

    private int NthPrime(int n) {
        if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "n must be positive.");
        // Handle first few primes directly for simplicity and to avoid Log(Log(n)) issues for small n.
        if (n == 1) return 2;
        if (n == 2) return 3;
        if (n == 3) return 5;
        if (n == 4) return 7;
        if (n == 5) return 11;

        // Estimate upper bound for the n-th prime using Rosser's theorem variant: p_n < n(ln n + ln ln n) for n >= 6
        // Add a safety margin.
        double estimatedPn = n * (Math.Log(n) + Math.Log(Math.Log(n)));
        int limit = (int)(estimatedPn * 1.2); // Add 20% buffer

        // Ensure limit is reasonably above a simpler estimate like n * log(n) or n * k as a fallback for smaller n if formula is problematic
        // For n=6, est = 6*(ln6+lnln6) = 6*(1.79+0.58) = 6*2.37 = 14.22. limit = 17. (Actual 6th prime is 13)
        // A simple n*15 estimate for n=10001 is 150015.
        // The more complex estimate for n=10001 is ~114318, with 20% buffer ~137182. This is a good estimate.
        // Let's ensure limit is not excessively small for intermediate values of n.
        if (n > 5 && limit < n * Math.Log(n) * 1.5) { // A simpler lower bound for safety.
             limit = (int)(n * Math.Log(n) * 1.5);
             if (limit < 30) limit = 30; // Minimum sieve size for small n > 5
        }


        List<int> primes = Library.SievePrimesList(limit);

        if (primes.Count < n) {
            // This might happen if the estimate is too low.
            // For this problem's constraints (n=10001), the estimate should be sufficient.
            // If it does happen, a larger sieve or a different prime-counting method would be needed.
            throw new InvalidOperationException($"Sieve limit of {limit} was too low to find the {n}-th prime. Found {primes.Count} primes. True value for 10001st prime is 104743.");
        }

        return primes[n - 1];
    }
}