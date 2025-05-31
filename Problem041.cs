namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 41: Pandigital prime.
/// Finds the largest n-digit pandigital prime that exists. An n-digit number is pandigital if it makes use of all the digits 1 to n exactly once.
/// For example, 2143 is a 4-digit pandigital and is also prime.
/// Further details can be found at https://projecteuler.net/problem=41
/// </summary>
public class Problem041 : Problem {
    /// <summary>
    /// Flag to indicate if the largest pandigital prime has been found by the recursive search.
    /// Helps in early exit from permutation generation once the target is found (since searching from highest n downwards).
    /// </summary>
    private bool _found;
    /// <summary>
    /// Stores the largest n-digit pandigital prime found.
    /// </summary>
    private int _result;

    /// <summary>
    /// Calculates the solution for Project Euler Problem 41.
    /// It finds the largest n-digit pandigital prime.
    /// </summary>
    /// <returns>The largest n-digit pandigital prime found; returns -1 if none are found (though one exists).</returns>
    public override object Solve() {
        return FindMaxPandigitalPrime(); // Renamed for clarity
    }

    /// <summary>
    /// Finds the largest n-digit pandigital prime.
    /// It iterates downwards for the number of digits `n` (from 9 down to 1 or 2).
    /// For each `n`, it first checks if the sum of digits 1 to `n` is divisible by 3.
    /// If so, any permutation of these digits will also be divisible by 3 and thus not prime (unless it's 3 itself,
    /// but n-digit pandigital means n > 1 for non-trivial primes). This skips `n=9,8,6,5,3,2`.
    /// It then generates permutations of digits (n, n-1, ..., 1) in reverse lexicographical order (effectively).
    /// The first prime found will be the largest for that `n`, and since `n` decreases, this will be the overall largest.
    /// </summary>
    /// <returns>The largest n-digit pandigital prime. Returns -1 if no such prime is found (theoretically, problem implies one exists).</returns>
    private int FindMaxPandigitalPrime() { // Renamed for clarity
        _found = false;
        _result = 0;

        // Iterate from n=9 digits down to n=2 (or n=1 for completeness, though 1-digit pandigital primes are just 2,3,5,7).
        // The problem asks for the largest n-digit pandigital prime.
        // The largest known is 7-digit (7652413). 8-digit and 9-digit pandigitals are divisible by 3.
        for (int numTotalDigits = 9; numTotalDigits >= 2; numTotalDigits--) {
            // Optimization: Sum of digits 1 to N is N*(N+1)/2. If this sum is divisible by 3,
            // then any number formed by these digits is divisible by 3.
            // If N*(N+1)/2 % 3 == 0, and the number is > 3, it cannot be prime.
            // This applies to N=2 (sum=3), N=3 (sum=6), N=5 (sum=15), N=6 (sum=21), N=8 (sum=36), N=9 (sum=45).
            // So, we only need to check N=1 (primes 2,3,5,7 - but problem implies n-digit uses 1 to n), N=4 (sum=10), N=7 (sum=28).
            // The code's condition `digits * (digits + 1) / 2 % 3 == 0` correctly identifies these.
            // It will check n=7, then n=4. Largest n-digit pandigital prime is sought.
            if (numTotalDigits * (numTotalDigits + 1) / 2 % 3 == 0 && numTotalDigits > 3) { // numTotalDigits=3 (sum=6) is skipped if >3 is not there.
                                                                                          // Actually, if sum is 3 (for N=2), number could be 21 (div by 3), 12 (div by 3). It only means it's div by 3.
                                                                                          // If the number itself *is* 3, it is prime. But an N-digit pandigital number cannot be 3 if N > 1.
                continue;
            }

            // Initialize permutation array with digits {N, N-1, ..., 1} to start search from largest permutations.
            int[] permDigits = new int[numTotalDigits];
            for (int i = 0; i < numTotalDigits; i++) {
                permDigits[i] = numTotalDigits - i; // e.g., for numTotalDigits=4, permDigits = [4,3,2,1]
            }

            GeneratePrimePermutations(permDigits, 0);
            if (_found) { // If a prime was found for this `numTotalDigits`
                return _result; // This will be the largest as we iterate numTotalDigits downwards.
            }
        }
        return -1; // Should be found, e.g., 7652413 for n=7. Or 2143 for n=4.
    }

    /// <summary>
    /// Recursively generates permutations of the digits in <paramref name="permDigits"/>,
    /// starting from index <paramref name="currentIndex"/>.
    /// When a full permutation is formed (currentIndex reaches end), it constructs the number,
    /// checks if it's prime. If prime, it's stored in <see cref="_result"/>, <see cref="_found"/> is set to true,
    /// and the recursion unwinds due to the _found flag.
    /// The permutations are generated in an order that, combined with the decreasing N in the calling method,
    /// ensures the first prime found is the largest n-digit pandigital prime.
    /// </summary>
    /// <param name="permDigits">The array of digits to permute.</param>
    /// <param name="currentIndex">The current starting index in <paramref name="permDigits"/> for generating permutations.
    /// Represents the current digit position being filled in the permutation.</param>
    private void GeneratePrimePermutations(int[] permDigits, int currentIndex) {
        if (_found) { // If already found the largest one (from a higher N or earlier permutation for current N)
            return;
        }

        int numElements = permDigits.Length;

        if (currentIndex == numElements -1) { // Base case: a full permutation is formed
            long number = 0;
            for (int i = 0; i < numElements; i++) {
                number = number * 10 + permDigits[i];
            }

            // Check if the formed number is prime.
            // Assumes Library.IsPrime is efficient enough (e.g., uses a sieve for smaller numbers if appropriate).
            if (Library.IsPrime(number)) {
                _result = (int)number; // Cast to int, assuming pandigital primes fit. Largest is 7-digit.
                _found = true;
            }
            return;
        }

        // Recursive step: generate permutations for remaining digits
        for (int i = currentIndex; i < numElements; i++) {
            // Swap current element with element at currentIndex
            (permDigits[currentIndex], permDigits[i]) = (permDigits[i], permDigits[currentIndex]);

            GeneratePrimePermutations(permDigits, currentIndex + 1);
            if (_found) return; // Early exit if found in a deeper recursive call

            // Backtrack: swap back to restore original array state for next iteration
            (permDigits[currentIndex], permDigits[i]) = (permDigits[i], permDigits[currentIndex]);
        }
    }
}