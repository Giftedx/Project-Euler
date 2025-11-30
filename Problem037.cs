namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 37: Truncatable primes.
/// Finds the sum of the only eleven primes that are both truncatable from left to right and right to left.
/// </summary>
public class Problem037 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 37: Truncatable primes.
    /// </summary>
    /// <returns>The sum of the eleven truncatable primes.</returns>
    public override object Solve() {
        return SumTruncatablePrimes();
    }

    /// <summary>
    /// Finds sum of truncatable primes.
    /// </summary>
    private int SumTruncatablePrimes() {
        int count = 0;
        int sum = 0;
        int n = 11;

        while (count < 11) {
            if (Library.IsPrime(n) && IsTruncatable(n)) {
                count++;
                sum += n;
            }
            n += 2;
        }
        return sum;
    }

    /// <summary>
    /// Checks if a prime is truncatable.
    /// </summary>
    private bool IsTruncatable(int n) {
        int temp = n;
        while (temp > 0) {
            if (!Library.IsPrime(temp)) return false;
            temp /= 10;
        }

        temp = n;
        int digits = Library.DigitCount(n);
        int divisor = Library.Pow10(digits - 1);

        while (divisor > 1) {
            temp %= divisor;
            if (!Library.IsPrime(temp)) return false;
            divisor /= 10;
        }

        return true;
    }
}
