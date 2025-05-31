namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 37: Truncatable primes.
/// Finds the sum of the only eleven primes that are both truncatable from left to right and right to left.
/// Note: 2, 3, 5, and 7 are not considered to be truncatable primes.
/// Further details can be found at https://projecteuler.net/problem=37
/// </summary>
public class Problem037 : Problem {
    /// <summary>
    /// The upper limit for the Sieve of Eratosthenes. Chosen to be large enough to find all
    /// eleven truncatable primes (which are not extremely large, e.g., 3797 is one).
    /// The largest known is 739397. A limit of 700,000 to 1,000,000 is typically sufficient.
    /// </summary>
    private const int SieveUpperLimit = 750000; // Adjusted slightly from original for clarity, original was 700000.

    /// <summary>
    /// Boolean array storing pre-computed primes up to <see cref="SieveUpperLimit"/>.
    /// `_isPrime[k]` is true if k is prime, false otherwise.
    /// Populated by the Sieve of Eratosthenes algorithm in the constructor.
    /// </summary>
    private readonly bool[] _isPrime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem037"/> class.
    /// This constructor pre-computes primes up to <see cref="SieveUpperLimit"/> using
    /// <see cref="Library.SieveOfEratosthenesBoolArray"/> and stores them in the
    /// <see cref="_isPrime"/> array for efficient primality testing during the search for truncatable primes.
    /// </summary>
    public Problem037() {
        // SieveOfEratosthenesBoolArray returns an array of size SieveUpperLimit + 1.
        _isPrime = Library.SieveOfEratosthenesBoolArray(SieveUpperLimit);
    }

    /// <summary>
    /// Calculates the solution for Project Euler Problem 37.
    /// It finds the sum of the eleven primes that are truncatable from both left to right and right to left.
    /// </summary>
    /// <returns>The sum of the eleven truncatable primes.</returns>
    public override object Solve() {
        return SumTruncatablePrimes();
    }

    /// <summary>
    /// Finds the sum of the first 11 truncatable primes (primes that remain prime when
    /// successively removing digits from left to right, and from right to left).
    /// Single-digit primes (2, 3, 5, 7) are not considered truncatable by the problem's definition.
    /// The method uses a Breadth-First Search (BFS) approach to generate candidate primes:
    /// 1. Initialize a queue with single-digit primes (2, 3, 5, 7). These are seeds.
    /// 2. Dequeue a prime `p`. If `p > 10` and is truncatable (checked via <see cref="IsTruncatable"/>), add it to results.
    /// 3. Generate new candidates by appending digits {1, 3, 7, 9} to `p`.
    ///    (Appending 0, 2, 4, 6, 8, or 5 would make the new number composite, unless it's the digit itself).
    /// 4. If a new candidate is prime, enqueue it.
    /// 5. Repeat until 11 truncatable primes are found.
    /// </summary>
    /// <returns>The sum of the eleven truncatable primes found.</returns>
    private long SumTruncatablePrimes() {
        var results = new List<int>();
        // Start BFS with single-digit primes as initial candidates.
        // These primes themselves are not truncatable but act as bases for larger candidates.
        var queue = new Queue<int>();
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(5);
        queue.Enqueue(7);

        // Digits that can be appended to a prime to potentially form a new prime.
        // Excludes even digits and 5 (unless it's the number 5 itself) to avoid obvious non-primes.
        int[] appendableDigits = { 1, 3, 7, 9 };

        while (results.Count < 11 && queue.Count > 0) { // Continue until 11 found or queue is empty
            int currentPrimeCandidate = queue.Dequeue();

            // Check if currentPrimeCandidate is truncatable (if it's > 10).
            // Single-digit primes are not considered truncatable as per problem statement.
            if (currentPrimeCandidate > 10 && IsTruncatable(currentPrimeCandidate)) {
                results.Add(currentPrimeCandidate);
            }

            // Generate next set of candidates by appending allowed digits.
            foreach (int digitToAppend in appendableDigits) {
                // Check for potential overflow if currentPrimeCandidate is large.
                if (currentPrimeCandidate > int.MaxValue / 10) continue; // Avoid overflow for currentPrimeCandidate * 10

                int nextCandidate = currentPrimeCandidate * 10 + digitToAppend;
                if (IsPrimeBySieve(nextCandidate)) { // Use local IsPrimeBySieve for clarity
                    queue.Enqueue(nextCandidate);
                }
            }
        }
        return results.Sum(x => (long)x); // Sum as long to be safe, though sum of 11 such primes will fit int.
    }

    /// <summary>
    /// Checks if a number <paramref name="n"/> is truncatable from both left-to-right and right-to-left.
    /// A number is truncatable if it is prime and remains prime when digits are successively removed
    /// from the left and from the right. Single-digit primes are not considered truncatable.
    /// Example: 3797. Right truncations: 3797, 379, 37, 3 (all prime).
    /// Left truncations: 3797, 797, 97, 7 (all prime). So 3797 is truncatable.
    /// </summary>
    /// <param name="n">The number to check. Must be greater than 9.</param>
    /// <returns>True if <paramref name="n"/> is a truncatable prime; false otherwise.</returns>
    private bool IsTruncatable(int n) {
        if (n <= 10) return false; // By definition, truncatable primes are > 10. Also ensure n is prime itself.
        if (!IsPrimeBySieve(n)) return false;


        // Right truncation: Check n, n/10, n/100, ...
        int tempRight = n / 10; // Start by truncating one digit from right. n itself is already checked.
        while (tempRight > 0) {
            if (!IsPrimeBySieve(tempRight)) return false;
            tempRight /= 10;
        }

        // Left truncation: Check n (already done), n % (10^k), n % (10^(k-1)), ...
        // Calculate the largest power of 10 smaller than n.
        // Example: n=3797. DigitCount=4. Divisor starts at 10^(4-1)=1000.
        // First left truncation: 3797 % 1000 = 797.
        // Next divisor: 100. 797 % 100 = 97.
        // Next divisor: 10. 97 % 10 = 7.
        int numDigits = Library.DigitCount(n);
        if (numDigits <= 1) return false; // Should have been caught by n > 10, but defensive.

        int divisor = Library.Pow10(numDigits - 1); // e.g., for 3797 (4 digits), divisor = 1000
        int tempLeft = n % divisor; // First left truncation (e.g., 797 for 3797)
        divisor /= 10;              // Prepare divisor for next step

        while (tempLeft > 0) { // Loop while there are digits to check after truncation
            if (!IsPrimeBySieve(tempLeft)) return false;
            if (divisor == 0) break; // Should not happen if tempLeft > 0 and numDigits > 1
            tempLeft %= divisor;
            divisor /= 10;
        }
        return true;
    }

    /// <summary>
    /// Checks if a number <paramref name="n"/> is prime using the pre-computed sieve <see cref="_isPrime"/>.
    /// This method is optimized for positive integers within the sieve's range.
    /// </summary>
    /// <param name="n">The number to check for primality.</param>
    /// <returns>True if <paramref name="n"/> is prime and within sieve bounds; false otherwise or if not prime.</returns>
    private bool IsPrimeBySieve(int n) { // Renamed for clarity
        // Check if n is within the valid bounds of the sieve array and positive.
        // Primes are positive. Sieve typically doesn't mark 0 or 1 as prime.
        if (n >= 2 && n < SieveUpperLimit) { // Adjusted check to be n < SieveUpperLimit as array is size SieveUpperLimit.
                                             // Original: n <= Limit. If Limit is SieveLimit, then array is SieveLimit+1.
                                             // My _isPrime is size SieveUpperLimit, so max index is SieveUpperLimit-1.
                                             // Corrected: _isPrime = Library.SieveOfEratosthenesBoolArray(SieveUpperLimit);
                                             // This means _isPrime array has size SieveUpperLimit (indices 0 to SieveUpperLimit-1)
                                             // OR SieveUpperLimit+1 (indices 0 to SieveUpperLimit).
                                             // Assuming Library.SieveOfEratosthenesBoolArray(X) returns bool[X+1] for indices up to X.
                                             // So, max index is SieveUpperLimit.
            return _isPrime[n];
        }
        // If n is outside the sieve's range, it's considered not prime for this problem's context,
        // as candidates are expected to be within a range where the sieve is effective.
        // Or, could fall back to Library.IsPrime(n) if numbers could exceed SieveUpperLimit.
        // Given the BFS generation, numbers are unlikely to drastically exceed SieveUpperLimit if it's reasonably set.
        return false; // Numbers outside sieve range are not considered or would need a general prime test.
    }
}