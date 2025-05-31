namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 7: 10001st prime.
/// Further details can be found at https://projecteuler.net/problem=7
/// </summary>
public class Problem007 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 7: 10001st prime.
    /// Determines the 10001st prime number.
    /// </summary>
    /// <returns>The 10001st prime number.</returns>
    public override object Solve() {
        return NthPrime(10001);
    }

    /// <summary>
    /// Finds the n-th prime number.
    /// It iterates through natural numbers starting from 2, checking each for primality using
    /// the Library.IsPrime() method. It counts primes found until the n-th prime is reached.
    /// </summary>
    /// <param name="n">The position of the desired prime number (e.g., n=1 for the 1st prime (2), n=6 for the 6th prime (13)).</param>
    /// <returns>The n-th prime number.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if n is not a positive integer.</exception>
    private int NthPrime(int n) {
        if (n <= 0) {
            throw new ArgumentOutOfRangeException(nameof(n), "Input must be a positive integer.");
        }
        if (n == 1) return 2; // First prime is 2

        int count = 1; // We already counted '2' as the first prime.
        int num = 3;   // Start checking from 3

        while (count < n) {
            if (Library.IsPrime(num)) {
                count++;
            }
            if (count < n) { // Avoid incrementing num if the nth prime was just found
                num += 2; // Only check odd numbers
            }
        }
        return num;
    }
}