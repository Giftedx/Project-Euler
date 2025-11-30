using System.Collections;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 10: Summation of primes.
/// Finds the sum of all the primes below two million.
/// </summary>
public class Problem010 : Problem {
    /// <summary>
    /// The limit for the sum of primes.
    /// </summary>
    private const int Limit = 2000000;

    /// <summary>
    /// Pre-computed sieve of primes.
    /// </summary>
    private readonly BitArray _isPrime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem010"/> class.
    /// Pre-computes primes using a sieve.
    /// </summary>
    public Problem010() {
        _isPrime = Library.SieveOfEratosthenesBitArray(Limit);
    }

    /// <summary>
    /// Solves Project Euler Problem 10: Summation of primes.
    /// </summary>
    /// <returns>The sum of all primes below two million.</returns>
    public override object Solve() {
        return SumPrimesBelow();
    }

    /// <summary>
    /// Calculates the sum of primes up to Limit.
    /// </summary>
    /// <returns>The sum of primes.</returns>
    private long SumPrimesBelow() {
        long sum = 0;
        for (int i = 2; i <= Limit; i++) {
            if (_isPrime[i]) {
                sum += i;
            }
        }
        return sum;
    }
}
