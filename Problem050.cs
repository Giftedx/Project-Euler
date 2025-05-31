namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 50: Consecutive prime sum.
/// Finds which prime, below one million, can be written as the sum of the most consecutive primes.
/// Further details can be found at https://projecteuler.net/problem=50
/// </summary>
public class Problem050 : Problem {
    /// <summary>
    /// The upper limit (exclusive) for the prime sum. The problem asks for primes below one million.
    /// This limit is also used for the Sieve of Eratosthenes.
    /// </summary>
    private const int MaxSumLimit = 1000000;

    /// <summary>
    /// Boolean array storing pre-computed primes up to <see cref="MaxSumLimit"/>.
    /// `_isPrimeSieve[k]` is true if k is prime.
    /// Assumes Library.SieveOfEratosthenesBoolArray(N) returns bool[N+1].
    /// </summary>
    private readonly bool[] _isPrimeSieve;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem050"/> class.
    /// This constructor pre-computes primes up to <see cref="MaxSumLimit"/> using
    /// <see cref="Library.SieveOfEratosthenesBoolArray"/> for efficient primality testing.
    /// </summary>
    public Problem050() {
        _isPrimeSieve = Library.SieveOfEratosthenesBoolArray(MaxSumLimit);
    }

    /// <summary>
    /// Calculates the solution for Project Euler Problem 50.
    /// It finds the prime number below one million that can be written as the sum of the most consecutive primes.
    /// </summary>
    /// <returns>The prime sum composed of the most consecutive primes below one million.</returns>
    public override object Solve() {
        return FindPrimeWithLongestConsecutivePrimeSum(MaxSumLimit); // Renamed for clarity
    }

    /// <summary>
    /// Finds the prime number below <paramref name="upperBoundForSum"/> that can be written as the sum of the
    /// most consecutive primes.
    ///
    /// Algorithm:
    /// 1. Determine `effectiveUpperBoundPrime`: the largest prime strictly less than <paramref name="upperBoundForSum"/>.
    ///    The sum of consecutive primes must not exceed this value.
    /// 2. Calculate `maxPossibleSequenceLength`: The maximum number of consecutive primes (starting from 2)
    ///    whose sum is less than or equal to `effectiveUpperBoundPrime`. This is an optimization to bound the search for sequence lengths.
    /// 3. Iterate downwards for `currentSequenceLength` from `maxPossibleSequenceLength` to 1.
    ///    For each `currentSequenceLength`:
    ///    a. Use a sliding window (Queue) of `currentSequenceLength` primes.
    ///    b. Iterate through primes (2, then odd primes using the sieve `_isPrimeSieve`).
    ///    c. Maintain the queue: if not full, enqueue. If full, dequeue oldest and enqueue newest.
    ///    d. Calculate the `currentSum` of primes in the queue.
    ///    e. If `currentSum > effectiveUpperBoundPrime`, break from iterating through primes for this `currentSequenceLength`
    ///       (as further sums will only increase).
    ///    f. If `currentSum` is prime (check using `_isPrimeSieve` if within bounds, else `Library.IsPrime`),
    ///       this `currentSum` is the answer because we are iterating `currentSequenceLength` downwards (longest first).
    /// </summary>
    /// <param name="upperBoundForSum">The exclusive upper limit for the prime sum itself.</param>
    /// <returns>The prime number that is a sum of the most consecutive primes below <paramref name="upperBoundForSum"/>.</returns>
    private long FindPrimeWithLongestConsecutivePrimeSum(int upperBoundForSum) { // Renamed for clarity
        // Find the largest prime strictly less than upperBoundForSum. This is the effective limit for sums.
        int effectiveUpperBoundPrime = upperBoundForSum - 1;
        while (effectiveUpperBoundPrime > 1 && !IsSievePrime(effectiveUpperBoundPrime)) {
            effectiveUpperBoundPrime--;
        }
        if (effectiveUpperBoundPrime <= 1) return 0; // Should not happen for reasonable upperBoundForSum

        // Determine the maximum possible length of a sequence of consecutive primes.
        int maxPossibleSequenceLength = CalculateMaxPossibleSequenceLength(effectiveUpperBoundPrime);

        // Iterate downwards from the longest possible sequence length.
        for (int currentSequenceLength = maxPossibleSequenceLength; currentSequenceLength >= 1; currentSequenceLength--) {
            Queue<int> currentPrimesInSequence = new Queue<int>(currentSequenceLength);
            long currentSum = 0; // Use long for sum to avoid overflow before check against effectiveUpperBoundPrime

            // Initialize with the first prime, 2, if sequence length allows.
            if (2 < upperBoundForSum) { // Ensure 2 is a valid prime to consider.
                 if (currentPrimesInSequence.Count < currentSequenceLength) {
                    currentPrimesInSequence.Enqueue(2);
                    currentSum += 2;
                }
            }

            // Iterate through odd numbers to find subsequent primes for the sequence.
            for (int i = 3; i < upperBoundForSum; i += 2) {
                if (!IsSievePrime(i)) continue;

                if (currentPrimesInSequence.Count < currentSequenceLength) {
                    currentPrimesInSequence.Enqueue(i);
                    currentSum += i;
                } else { // Queue is full, implement sliding window.
                    currentSum -= currentPrimesInSequence.Dequeue(); // Remove oldest prime's contribution
                    currentPrimesInSequence.Enqueue(i);
                    currentSum += i; // Add newest prime's contribution
                }

                // Only check sum if the queue is full (i.e., we have a complete sequence of currentSequenceLength).
                if (currentPrimesInSequence.Count == currentSequenceLength) {
                    if (currentSum > effectiveUpperBoundPrime) {
                        // If current sum exceeds the limit, further primes in this sequence length
                        // will also result in sums too large. Break to try shorter sequence length.
                        break;
                    }
                    if (IsSievePrime((int)currentSum)) { // Cast to int for sieve check, assuming sum fits.
                        return currentSum; // Found the prime sum with the longest sequence.
                    }
                }
            }
        }
        return 0; // Should be found based on problem statement.
    }

    /// <summary>
    /// Calculates the maximum possible length of a sequence of consecutive primes (starting from 2)
    /// whose sum does not exceed <paramref name="sumLimit"/>.
    /// </summary>
    /// <param name="sumLimit">The maximum allowable sum for the sequence of consecutive primes.</param>
    /// <returns>The maximum number of terms in such a sequence.</returns>
    private int CalculateMaxPossibleSequenceLength(int sumLimit) { // Renamed for clarity
        int sequenceLength = 0;
        long currentSum = 0; // Use long for sum to avoid overflow.

        if (2 <= sumLimit) { // Start with prime 2
            currentSum += 2;
            sequenceLength++;
        } else {
            return 0; // Cannot even include 2.
        }

        for (int i = 3; i <= sumLimit; i += 2) { // Iterate through odd numbers
            if (IsSievePrime(i)) {
                if (currentSum + i > sumLimit) { // If adding next prime exceeds limit
                    break; // Current sequenceLength is the max possible.
                }
                currentSum += i;
                sequenceLength++;
            }
        }
        return sequenceLength;
    }

    /// <summary>
    /// Helper to check primality using the pre-computed sieve, for numbers within its range.
    /// </summary>
    private bool IsSievePrime(int n) {
        if (n >= 0 && n <= MaxSumLimit) { // MaxSumLimit is the size of the sieve array _isPrimeSieve (actually MaxSumLimit+1)
            return _isPrimeSieve[n];
        }
        // For numbers outside the sieve range, this problem implies they are not relevant,
        // or Library.IsPrime would be needed if sums could exceed sieve range but still be valid.
        // Given problem constraints, sums should be < MaxSumLimit.
        return false;
    }
}