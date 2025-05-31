namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 21: Amicable numbers.
/// Evaluates the sum of all the amicable numbers under 10000.
/// An amicable pair (a, b) exists if the sum of proper divisors of a is b, and the sum of proper divisors of b is a, where a ≠ b.
/// Further details can be found at https://projecteuler.net/problem=21
/// </summary>
public class Problem021 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 21.
    /// It finds all amicable numbers under 10000 and sums them.
    /// </summary>
    /// <returns>The sum of all amicable numbers under 10000.</returns>
    public override object Solve() {
        return AmicableSumBelow(10000);
    }

    /// <summary>
    /// Calculates the sum of all amicable numbers below a given limit <paramref name="limit"/>.
    /// It first pre-computes the sum of proper divisors for all numbers up to <paramref name="limit"/>.
    /// Then, it iterates through numbers, checking for amicable pairs (a, b) where a ≠ b,
    /// d(a) = b, and d(b) = a. Each pair is added to the sum once.
    /// </summary>
    /// <param name="limit">The upper limit (exclusive) for finding amicable numbers.</param>
    /// <returns>The sum of all amicable numbers below <paramref name="limit"/>.</returns>
    private int AmicableSumBelow(int limit) {
        // The FillDivisors method expects n to be limit + 1 for array indexing up to 'limit'.
        FillDivisors(limit + 1, out int[] sumOfProperDivisors);
        int totalSumOfAmicableNumbers = 0;

        for (int a = 1; a < limit; ++a) { // Iterate up to limit (exclusive)
            int b = sumOfProperDivisors[a]; // b = d(a)

            // Check for amicable pair conditions:
            // 1. a != b (implied by j > i, here b > a for counting pairs once)
            // 2. d(a) = b and d(b) = a
            // We also need b to be within the range [1, limit-1] to look up sumOfProperDivisors[b].
            if (b > a && b < limit && sumOfProperDivisors[b] == a) {
                totalSumOfAmicableNumbers += a + b; // Add both members of the pair to the sum
            }
        }
        return totalSumOfAmicableNumbers;
    }

    /// <summary>
    /// Pre-computes the sum of proper divisors for all numbers up to <paramref name="size"/> - 1.
    /// The result is stored in the <paramref name="divisorsSumArray"/> where divisorsSumArray[k]
    /// will hold the sum of proper divisors of k.
    /// A proper divisor of a number is a positive divisor other than the number itself.
    /// </summary>
    /// <param name="size">The size of the array to be populated. Sums will be computed for numbers from 1 to size-1.</param>
    /// <param name="divisorsSumArray">An output array where divisorsSumArray[k] is the sum of proper divisors of k.</param>
    private void FillDivisors(int size, out int[] divisorsSumArray) {
        divisorsSumArray = new int[size]; // divisorsSumArray[0] is unused.
                                        // divisorsSumArray[1] should be 0 (sum of proper divisors of 1).
                                        // Initialize all to 0, which is correct for sum of proper divisors of 1.

        // Iterate through all possible divisors 'i'.
        for (int i = 1; i < size; ++i) {
            // For each 'i', iterate through its multiples 'j = k*i' where k > 1.
            // 'i' is a proper divisor of 'j'.
            for (int j = 2 * i; j < size; j += i) {
                divisorsSumArray[j] += i;
            }
        }
    }
}