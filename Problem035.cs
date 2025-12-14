namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 35: Circular primes.
/// The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.
/// There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
/// How many circular primes are there below one million?
/// </summary>
public class Problem035 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 35.
    /// </summary>
    /// <returns>The count of circular primes below one million.</returns>
    public override object Solve() {
        return CircularPrimeCount();
    }

    /// <summary>
    /// Calculates the number of circular primes below one million.
    /// A circular prime is a prime number that remains prime under cyclic shifts of its digits.
    /// For example, 197, 971, and 719 are all prime.
    /// </summary>
    /// <returns>The total count of circular primes below one million.</returns>
    private int CircularPrimeCount() {
        int limit = 1000000;
        bool[] isPrime = Library.SieveOfEratosthenesBoolArray(limit);
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

    private bool IsCircularPrime(int n, bool[] isPrime) {
        int digits = (int)Math.Floor(Math.Log10(n)) + 1;
        int temp = n;

        for (int i = 0; i < digits - 1; i++) {
            // Rotate number
            int lastDigit = temp % 10;
            int remaining = temp / 10;
            temp = lastDigit * (int)Math.Pow(10, digits - 1) + remaining;

            if (!isPrime[temp]) {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Checks if a number is prime.
    /// Note: This uses the Library for efficiency in the loop above, but kept here for fallback or specific checks if needed.
    /// </summary>
    private bool IsPrime(int n) {
        if (n <= 1) return false;
        if (n == 2) return true;
        if (n % 2 == 0) return false;
        for (int i = 3; i * i <= n; i += 2) {
            if (n % i == 0) return false;
        }
        return true;
    }
}
