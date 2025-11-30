namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 35: Circular primes.
/// Finds the number of circular primes below one million.
/// </summary>
public class Problem035 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 35: Circular primes.
    /// </summary>
    /// <returns>The number of circular primes below one million.</returns>
    public override object Solve() {
        return CountCircularPrimes(1000000);
    }

    /// <summary>
    /// Counts circular primes.
    /// </summary>
    private int CountCircularPrimes(int limit) {
        var isPrime = Library.SieveOfEratosthenesBoolArray(limit);
        int count = 0;

        for (int i = 2; i < limit; i++) {
            if (isPrime[i]) {
                if (IsCircularPrime(i, isPrime)) {
                    count++;
                }
            }
        }
        return count;
    }

    /// <summary>
    /// Checks if a number is a circular prime.
    /// </summary>
    private bool IsCircularPrime(int n, bool[] isPrime) {
        string s = n.ToString();
        int len = s.Length;

        for (int i = 0; i < len; i++) {
            int rem = n % 10;
            n = n / 10 + rem * Library.Pow10(len - 1);
            if (!isPrime[n]) return false;
        }
        return true;
    }
}
