namespace Project_Euler;

public class Problem010 : Problem {
    private const int Limit = 2000000;
    private readonly bool[] _isPrime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem010"/> class.
    /// Pre-calculates primes up to the limit using a sieve.
    /// </summary>
    public Problem010() {
        _isPrime = Library.SieveOfEratosthenesBoolArray(Limit);
    }

    /// <summary>
    /// Solves Project Euler Problem 10: Summation of primes.
    /// Finds the sum of all the primes below two million.
    /// </summary>
    /// <returns>The sum of all primes below two million.</returns>
    public override object Solve() {
        return SumPrimesBelow();
    }

    private long SumPrimesBelow() {
        long sum = 0;
        // _isPrime array is of size Limit + 1. Indices from 0 to Limit.
        // Primes start from 2.
        for (int i = 2; i <= Limit; i++) {
            if (_isPrime[i]) {
                sum += i;
            }
        }
        return sum;
    }
}