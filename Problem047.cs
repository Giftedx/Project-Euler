namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 47: Distinct primes factors.
/// Finds the first four consecutive integers to have four distinct prime factors each.
/// The problem asks for the first of these numbers.
/// Further details can be found at https://projecteuler.net/problem=47
/// </summary>
public class Problem047 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 47.
    /// It searches for the first of four consecutive integers that each have four distinct prime factors.
    /// </summary>
    /// <returns>The first of the four consecutive integers satisfying the condition. Returns -1 if not found within the hardcoded search limit.</returns>
    public override object Solve() {
        return FindStartOfConsecutiveFourDistinctPrimeFactors(); // Renamed for clarity
    }

    /// <summary>
    /// Finds the first integer that is the start of a sequence of four consecutive integers,
    /// each having exactly four distinct prime factors.
    ///
    /// Algorithm:
    /// 1. Define a search `searchLimit` (e.g., 200,000).
    /// 2. Create an array `distinctPrimeFactorCounts` of size `searchLimit`. `distinctPrimeFactorCounts[i]` will store
    ///    the number of distinct prime factors for integer `i`.
    /// 3. Populate `distinctPrimeFactorCounts` using a sieve-like method:
    ///    - Iterate `i` from 2 up to `searchLimit`.
    ///    - If `distinctPrimeFactorCounts[i]` is 0, it means `i` is a prime number.
    ///    - For this prime `i`, iterate through its multiples `j = i, 2i, 3i, ...` up to `searchLimit`.
    ///    - For each multiple `j`, increment `distinctPrimeFactorCounts[j]`.
    /// 4. Search for the consecutive sequence:
    ///    - Iterate `i` from 2 up to `searchLimit - 3`.
    ///    - Check if `distinctPrimeFactorCounts[i]`, `distinctPrimeFactorCounts[i+1]`,
    ///      `distinctPrimeFactorCounts[i+2]`, and `distinctPrimeFactorCounts[i+3]` are all equal to 4.
    ///    - If they are, `i` is the first number in the sequence, and it is returned.
    /// 5. If no such sequence is found within the `searchLimit`, return -1.
    /// </summary>
    /// <returns>The first integer of the four consecutive integers, or -1 if not found within the search limit.</returns>
    private int FindStartOfConsecutiveFourDistinctPrimeFactors() { // Renamed for clarity
        const int searchLimit = 200000; // Upper bound for the search.
        // Stores the count of distinct prime factors for each number up to searchLimit.
        byte[] distinctPrimeFactorCounts = new byte[searchLimit];

        // Sieve-like process to count distinct prime factors.
        for (int i = 2; i < searchLimit; i++) {
            // If distinctPrimeFactorCounts[i] is 0, then 'i' is a prime number.
            // (It hasn't been marked by any smaller prime factor).
            if (distinctPrimeFactorCounts[i] == 0) {
                // 'i' is prime. Iterate through all multiples of 'i' and increment their distinct prime factor count.
                for (int j = i; j < searchLimit; j += i) {
                    distinctPrimeFactorCounts[j]++;
                }
            }
        }

        // Search for four consecutive integers each having exactly 4 distinct prime factors.
        // We need to check up to searchLimit - 4 to allow for i, i+1, i+2, i+3.
        for (int i = 2; i <= searchLimit - 4; i++) {
            if (distinctPrimeFactorCounts[i] == 4 &&
                distinctPrimeFactorCounts[i + 1] == 4 &&
                distinctPrimeFactorCounts[i + 2] == 4 &&
                distinctPrimeFactorCounts[i + 3] == 4) {
                return i; // Found the first integer of the sequence.
            }
        }
        return -1; // Should not be reached if limit is high enough, as problem implies a solution exists.
    }
}