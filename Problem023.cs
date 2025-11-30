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
    private readonly int[] _properDivisorSum;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem023"/> class.
    /// This constructor pre-computes the sum of proper divisors for all numbers
    /// from 1 up to <see cref="Limit"/>-1. This is done efficiently by using the shared Library.
    /// </summary>
    public Problem023() {
        _properDivisorSum = Library.GetProperDivisorSums(Limit);
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
    /// Estimates the count of abundant numbers below Limit.
    /// Based on empirical data, approximately 25% of numbers below 28123 are abundant.
    /// </summary>
    /// <returns>Estimated count of abundant numbers below Limit.</returns>
    private int EstimateAbundantCount() {
        // Empirically, about 25% of numbers below 28123 are abundant
        // Starting from 12 (first abundant), we have (Limit - 12) candidates
        return (int)((Limit - 12) * 0.25);
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
        // Calculate the approximate count of abundant numbers below Limit.
        int abundantCountEstimate = EstimateAbundantCount();
        // Initialize with a capacity based on the calculated estimate.
        List<int> abundantNumbers = new List<int>(abundantCountEstimate);
        // Smallest abundant number is 12.
        // Populate list of abundant numbers up to Limit - 1.
        // Inlined IsAbundant(n) logic: _properDivisorSum[n] > n
        // n is always within bounds [12, Limit-1] here, so no need for n <= 0 || n >= Limit check.
        for (int n = 12; n < Limit; n++) {
            if (_properDivisorSum[n] > n) {
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