using System.Collections;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 50: Consecutive prime sum.
/// Finds which prime, below one million, can be written as the sum of the most consecutive primes.
/// </summary>
public class Problem050 : Problem {
    private const int Limit = 1000000;
    private readonly BitArray _isPrime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem050"/> class.
    /// </summary>
    public Problem050() {
        _isPrime = Library.SieveOfEratosthenesBitArray(Limit);
    }

    /// <summary>
    /// Solves Project Euler Problem 50: Consecutive prime sum.
    /// </summary>
    /// <returns>The prime sum of the most consecutive primes.</returns>
    public override object Solve() {
        return LongestConsecutivePrimeSum();
    }

    /// <summary>
    /// Finds longest consecutive prime sum.
    /// </summary>
    private int LongestConsecutivePrimeSum() {
        var primes = Library.SievePrimesList(Limit);
        int maxLen = 0;
        int maxPrime = 0;

        long[] primeSum = new long[primes.Count + 1];
        primeSum[0] = 0;
        for (int i = 0; i < primes.Count; i++) {
            primeSum[i + 1] = primeSum[i] + primes[i];
        }

        for (int i = 0; i < primes.Count; i++) {
            for (int j = i - (maxLen + 1); j >= 0; j--) {
                long currentSum = primeSum[i + 1] - primeSum[j];
                if (currentSum >= Limit) break;

                if (_isPrime[(int)currentSum]) {
                    if (i - j + 1 > maxLen) {
                        maxLen = i - j + 1;
                        maxPrime = (int)currentSum;
                    }
                }
            }
        }
        return maxPrime;
    }
}
