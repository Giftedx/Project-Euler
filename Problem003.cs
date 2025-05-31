namespace Project_Euler;

public class Problem003 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 3: Largest prime factor.
    /// Finds the largest prime factor of the number 600851475143.
    /// </summary>
    /// <returns>The largest prime factor of 600851475143.</returns>
    public override object Solve() {
        return LargestPrimeFactor(600851475143);
    }

    private long LargestPrimeFactor(long n) {
        long largestPrimeFactor = 0;

        while ((n & 1) == 0) { // Check for factors of 2
            largestPrimeFactor = 2;
            n >>= 1; // Equivalent to n /= 2
        }

        while (n % 3 == 0) { // Check for factors of 3
            largestPrimeFactor = 3;
            n /= 3;
        }

        // All primes greater than 3 can be expressed in the form 6k ± 1.
        // So, we only need to check divisibility by i and i+2 for i starting from 5 with a step of 6.
        for (int i = 5; (long)i * i <= n; i += 6) {
            while (n % i == 0) {
                largestPrimeFactor = i;
                n /= i;
            }

            while (n % (i + 2) == 0) {
                largestPrimeFactor = i + 2;
                n /= i + 2;
            }
        }

        // If n is still greater than 1 at this point, then n itself must be prime.
        // This handles cases where the remaining n is a prime number larger than any factor found so far.
        // For example, if n was initially prime, or if after dividing by smaller primes, the remainder is prime.
        if (n > 1) largestPrimeFactor = n;
        return largestPrimeFactor;
    }
}