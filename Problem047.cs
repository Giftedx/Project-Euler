namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 47: Distinct primes factors.
/// Finds the first four consecutive integers to have four distinct prime factors each.
/// </summary>
public class Problem047 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 47: Distinct primes factors.
    /// </summary>
    /// <returns>The first of the four consecutive integers.</returns>
    public override object Solve() {
        return FirstConsecutiveIntegers(4);
    }

    /// <summary>
    /// Finds consecutive integers with n distinct prime factors.
    /// </summary>
    private int FirstConsecutiveIntegers(int target) {
        int consecutive = 0;
        int i = 2;
        while (true) {
            if (CountDistinctPrimeFactors(i) == target) {
                consecutive++;
                if (consecutive == target) {
                    return i - target + 1;
                }
            } else {
                consecutive = 0;
            }
            i++;
        }
    }

    /// <summary>
    /// Counts distinct prime factors.
    /// </summary>
    private int CountDistinctPrimeFactors(int n) {
        int count = 0;
        for (int i = 2; i * i <= n; i++) {
            if (n % i == 0) {
                count++;
                while (n % i == 0) n /= i;
            }
        }
        if (n > 1) count++;
        return count;
    }
}
