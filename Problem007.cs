namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 7: 10001st prime.
/// Finds the 10 001st prime number.
/// </summary>
public class Problem007 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 7: 10001st prime.
    /// Finds the 10 001st prime number.
    /// </summary>
    /// <returns>The 10 001st prime number.</returns>
    public override object Solve() {
        return NthPrime(10001);
    }

    /// <summary>
    /// Finds the nth prime number.
    /// Uses the trial division method (via Library.IsPrime) to find primes sequentially.
    /// </summary>
    /// <param name="n">The rank of the prime to find (e.g., 1 for 2, 2 for 3).</param>
    /// <returns>The nth prime number.</returns>
    private int NthPrime(int n) {
        if (n == 1) return 2;
        int count = 1; // We have found '2'
        int candidate = 1;

        while (count < n) {
            candidate += 2; // Check only odd numbers
            if (Library.IsPrime(candidate)) {
                count++;
            }
        }
        return candidate;
    }
}
