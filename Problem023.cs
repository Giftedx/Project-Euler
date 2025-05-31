// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 23: Non-abundant sums.
/// Finds the sum of all positive integers which cannot be written as the sum of two abundant numbers.
/// An abundant number is a number for which the sum of its proper divisors is greater than the number itself.
/// All integers greater than 28123 can be written as the sum of two abundant numbers.
/// Further details can be found at https://projecteuler.net/problem=23
/// </summary>
public class Problem023 : Problem {
    /// <summary>
    /// The upper limit for this problem. All integers greater than 28123 can be written as the sum of two abundant numbers.
    /// The search for numbers not expressible as the sum of two abundant numbers is performed up to this limit.
    /// </summary>
    private const int Limit = 28123;

    /// <summary>
    /// Array storing the sum of proper divisors for numbers up to <see cref="Limit"/>-1.
    /// `_properDivisorSum[k]` holds the sum of proper divisors of k.
    /// This is pre-computed in the constructor.
    /// </summary>
    private readonly int[] _properDivisorSum = new int[Limit];

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem023"/> class.
    /// This constructor pre-computes the sum of proper divisors for all numbers
    /// from 1 up to <see cref="Limit"/>-1. This is done efficiently by iterating
    /// through potential divisors and adding them to their multiples.
    /// </summary>
    public Problem023() {
        // _properDivisorSum[0] is unused. _properDivisorSum[1] (sum of proper divisors of 1) is 0.
        for (int i = 1; i < Limit; i++) { // 'i' is the potential divisor
            for (int j = 2 * i; j < Limit; j += i) { // 'j' is a multiple of 'i'
                _properDivisorSum[j] += i; // Add 'i' to the sum of proper divisors for 'j'
            }
        }
    }

    /// <summary>
    /// Calculates the solution for Project Euler Problem 23.
    /// It finds the sum of all positive integers up to <see cref="Limit"/> (exclusive)
    /// which cannot be written as the sum of two abundant numbers.
    /// </summary>
    /// <returns>The sum of all positive integers not expressible as the sum of two abundant numbers.</returns>
    public override object Solve() {
        return SumOfNonAbundantBelow();
    }

    /// <summary>
    /// Calculates the sum of all positive integers below <see cref="Limit"/> that cannot be
    /// expressed as the sum of two abundant numbers.
    /// The method first identifies all abundant numbers up to <see cref="Limit"/>.
    /// Then, it marks all numbers that *can* be expressed as the sum of two abundant numbers.
    /// Finally, it sums up all numbers that were not marked.
    /// </summary>
    /// <returns>The sum of integers not expressible as the sum of two abundant numbers.</returns>
    private int SumOfNonAbundantBelow() {
        /// <summary>
        /// Determines if a number 'n' is abundant.
        /// A number is abundant if the sum of its proper divisors is greater than the number itself.
        /// Uses the pre-computed <see cref="_properDivisorSum"/> array.
        /// </summary>
        /// <param name="n">The number to check for abundancy.</param>
        /// <returns>True if 'n' is abundant, false otherwise.</returns>
        bool IsAbundant(int n) {
            // Ensure n is within the valid range for the _properDivisorSum array.
            // Smallest abundant number is 12. Max n to check is Limit - 1.
            if (n <= 0 || n >= Limit) return false; // Numbers outside this range are not considered or not in table.
            return _properDivisorSum[n] > n;
        }

        var abundantNumbers = new List<int>();
        // Populate the list of abundant numbers. Smallest abundant number is 12.
        for (int n = 12; n < Limit; n++) {
            if (IsAbundant(n)) {
                abundantNumbers.Add(n);
            }
        }

        // Boolean array to mark numbers that are sums of two abundant numbers.
        // isSumOfTwoAbundants[k] will be true if k can be written as a + b where a,b are abundant.
        bool[] isSumOfTwoAbundants = new bool[Limit]; // Initialized to false by default.

        // Iterate through all pairs of abundant numbers (a, b)
        for (int i = 0; i < abundantNumbers.Count; i++) {
            int a = abundantNumbers[i];
            // Optimization: If a + a (smallest possible sum with current 'a') is already >= Limit,
            // then any sum a + b (where b >= a) will also be >= Limit. So, we can break the outer loop.
            if (a + a >= Limit) {
                 break;
            }
            // Inner loop starts from 'i' to consider pairs (a,b) where b >= a, avoiding duplicate sums (a+b vs b+a)
            // and ensuring each sum is generated efficiently.
            for (int j = i; j < abundantNumbers.Count; j++) {
                int b = abundantNumbers[j];
                int currentSum = a + b;

                if (currentSum < Limit) {
                    isSumOfTwoAbundants[currentSum] = true;
                } else {
                    // Since abundantNumbers is sorted, if a+b >= Limit for the current 'b',
                    // then for any subsequent 'b' (which will be larger), a+b' will also be >= Limit.
                    // Thus, we can break the inner loop for the current 'a'.
                    break;
                }
            }
        }

        int totalSumOfNonAbundantSums = 0;
        // Sum all positive integers up to Limit-1 (i.e., < Limit)
        // that cannot be written as the sum of two abundant numbers.
        for (int i = 1; i < Limit; i++) {
            if (!isSumOfTwoAbundants[i]) {
                totalSumOfNonAbundantSums += i;
            }
        }
        return totalSumOfNonAbundantSums;
    }
}