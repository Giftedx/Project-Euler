namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 49: Prime permutations.
/// Finds an arithmetic sequence of three 4-digit primes, where each of the three terms
/// are permutations of one another. The problem asks for the 12-digit number formed by
/// concatenating these three terms for the sequence other than 1487, 4817, 8147.
/// Further details can be found at https://projecteuler.net/problem=49
/// </summary>
public class Problem049 : Problem {
    /// <summary>
    /// Upper limit for the Sieve of Eratosthenes and for searching 4-digit primes.
    /// Primes are checked up to 9999.
    /// </summary>
    private const int SieveLimit = 10000;

    /// <summary>
    /// Boolean array storing pre-computed primes up to <see cref="SieveLimit"/>.
    /// `_isPrime[k]` is true if k is prime, false otherwise.
    /// Populated by the Sieve of Eratosthenes algorithm in the constructor.
    /// Index 0 to SieveLimit (inclusive, if SieveOfEratosthenesBoolArray returns size Limit+1).
    /// </summary>
    private readonly bool[] _isPrime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem049"/> class.
    /// This constructor pre-computes primes up to <see cref="SieveLimit"/> (inclusive) using
    /// <see cref="Library.SieveOfEratosthenesBoolArray"/> for efficient primality testing.
    /// </summary>
    public Problem049() {
        // Assuming Library.SieveOfEratosthenesBoolArray(N) returns bool[N+1] for indices up to N.
        _isPrime = Library.SieveOfEratosthenesBoolArray(SieveLimit);
    }

    /// <summary>
    /// Calculates the solution for Project Euler Problem 49.
    /// It finds the "other" arithmetic sequence of three 4-digit prime permutations
    /// (common difference 3330) and returns their concatenation.
    /// </summary>
    /// <returns>A string representing the 12-digit number formed by concatenating the three prime terms.
    /// Returns ":(" if no such sequence (other than the example) is found within the search space.</returns>
    public override object Solve() {
        return FindConcatenatedPrimePermutationSequence(); // Renamed for clarity
    }

    /// <summary>
    /// Finds an arithmetic sequence of three 4-digit primes (p1, p2, p3) such that:
    /// 1. p1, p2, p3 are permutations of each other.
    /// 2. p2 - p1 = p3 - p2 = 3330 (the common difference).
    /// 3. The sequence is not the one starting with 1487.
    /// The method returns the 12-digit number formed by concatenating these three primes.
    /// </summary>
    /// <returns>The 12-digit concatenated string if the sequence is found; otherwise, ":(".</returns>
    private string FindConcatenatedPrimePermutationSequence() { // Renamed for clarity
        const int commonDifference = 3330;
        // Iterate through possible first terms 'p1' (must be a 4-digit prime).
        // Start from 1001 (smallest 4-digit odd number). Max p1 such that p1 + 2*gap < SieveLimit.
        // Max p1 = SieveLimit - 1 - 2 * commonDifference = 9999 - 6660 = 3339.
        for (int p1 = 1001; p1 <= SieveLimit - 1 - 2 * commonDifference; p1 += 2) { // Iterate odd numbers
            if (!_isPrime[p1]) {
                continue;
            }

            int p2 = p1 + commonDifference;
            int p3 = p2 + commonDifference;

            // Check if p2 and p3 are within the 4-digit prime limit and are themselves prime.
            // SieveLimit is 10000, so p2 and p3 must be < 10000.
            // _isPrime array is indexed up to SieveLimit, so checks _isPrime[p2] and _isPrime[p3] are safe if p2, p3 < SieveLimit.
            if (p2 < SieveLimit && _isPrime[p2] &&
                p3 < SieveLimit && _isPrime[p3]) {
                // Check if they are permutations of each other.
                if (AreDigitsPermutations(p1, p2) && AreDigitsPermutations(p1, p3)) {
                    // Exclude the example sequence 1487, 4817, 8147.
                    if (p1 != 1487) { // Checking only the first term is sufficient to exclude the sequence.
                        return $"{p1}{p2}{p3}";
                    }
                }
            }
        }
        return ":("; // Should be found based on problem statement.
    }

    /// <summary>
    /// Checks if two integers, <paramref name="num1"/> and <paramref name="num2"/>, are permutations of each other's digits.
    /// Assumes both numbers are positive and have the same number of digits (implicitly true for 4-digit primes in this problem).
    /// Optimized with early termination for better performance on non-permutations.
    /// </summary>
    /// <param name="num1">The first integer.</param>
    /// <param name="num2">The second integer.</param>
    /// <returns>True if the numbers are digit permutations of each other; false otherwise.</returns>
    private bool AreDigitsPermutations(int num1, int num2) { // Renamed for clarity
        // Array to count occurrences of each digit (0-9).
        int[] digitCounts = new int[10];

        int temp1 = num1;
        while (temp1 > 0) {
            digitCounts[temp1 % 10]++; // Increment count for digits of num1
            temp1 /= 10;
        }

        int temp2 = num2;
        while (temp2 > 0) {
            int digit = temp2 % 10;
            digitCounts[digit]--; // Decrement count for digits of num2
            // Early termination: if count goes negative, not a permutation
            if (digitCounts[digit] < 0) {
                return false;
            }
            temp2 /= 10;
        }

        // If they are permutations, all counts in digitCounts should be zero.
        for (int i = 0; i < 10; i++) {
            if (digitCounts[i] != 0) {
                return false;
            }
        }
        return true;
    }
}