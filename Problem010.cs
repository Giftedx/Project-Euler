using System.Collections; // Required for BitArray

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 10: Summation of primes.
/// Further details can be found at https://projecteuler.net/problem=10
/// </summary>
public class Problem010 : Problem {
    /// <summary>
    /// The upper limit (inclusive) for summing primes. For this problem, it is two million.
    /// </summary>
    private const int Limit = 2000000;

    /// <summary>
    /// A BitArray representing primality of numbers up to Limit.
    /// _isPrime[i] is true if i is prime, false otherwise.
    /// Populated by the Sieve of Eratosthenes algorithm in the constructor.
    /// </summary>
    private readonly BitArray _isPrime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem010"/> class.
    /// This constructor pre-computes all primes up to <see cref="Limit"/>
    /// using the Sieve of Eratosthenes algorithm implemented in <see cref="Library.SieveOfEratosthenesBitArray"/>.
    /// The results are stored in the <see cref="_isPrime"/> BitArray for efficient lookup.
    /// </summary>
    public Problem010() {
        // The SieveOfEratosthenesBitArray method returns a BitArray where index i is true if i is prime.
        // The size of the BitArray will be Limit + 1 to include the number Limit itself.
        _isPrime = Library.SieveOfEratosthenesBitArray(Limit);
    }

    /// <summary>
    /// Solves Project Euler Problem 10: Summation of primes.
    /// Calculates the sum of all the primes below two million.
    /// </summary>
    /// <returns>The sum of all primes below two million.</returns>
    public override object Solve() {
        return SumPrimesBelow();
    }

    /// <summary>
    /// Calculates the sum of all prime numbers up to and including the <see cref="Limit"/>.
    /// It iterates from 2 up to <see cref="Limit"/> and uses the pre-computed <see cref="_isPrime"/>
    /// BitArray to check for primality. If a number is prime, it's added to the total sum.
    /// </summary>
    /// <returns>The sum of all primes up to and including <see cref="Limit"/>.</returns>
    private long SumPrimesBelow() {
        long sum = 0;
        // _isPrime BitArray has indices from 0 to Limit.
        // Primes are checked starting from 2.
        for (int i = 2; i <= Limit; i++) { // Iterate up to Limit (inclusive)
            if (_isPrime[i]) { // Check primality using the pre-computed sieve
                sum += i;
            }
        }
        return sum;
    }
}