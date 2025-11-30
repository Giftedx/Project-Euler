namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 23: Non-abundant sums.
/// Finds the sum of all positive integers which cannot be written as the sum of two abundant numbers.
/// </summary>
public class Problem023 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 23: Non-abundant sums.
    /// </summary>
    /// <returns>The sum of all positive integers which cannot be written as the sum of two abundant numbers.</returns>
    public override object Solve() {
        return SumNonAbundantSums();
    }

    /// <summary>
    /// Finds sum of non-abundant sums.
    /// </summary>
    private int SumNonAbundantSums() {
        const int limit = 28123;
        List<int> abundantNumbers = new List<int>();

        // Find all abundant numbers
        for (int i = 12; i <= limit; i++) {
            if (IsAbundant(i)) {
                abundantNumbers.Add(i);
            }
        }

        bool[] canBeWrittenAsAbundantSum = new bool[limit + 1];
        for (int i = 0; i < abundantNumbers.Count; i++) {
            for (int j = i; j < abundantNumbers.Count; j++) {
                int sum = abundantNumbers[i] + abundantNumbers[j];
                if (sum <= limit) {
                    canBeWrittenAsAbundantSum[sum] = true;
                } else {
                    break;
                }
            }
        }

        int totalSum = 0;
        for (int i = 1; i <= limit; i++) {
            if (!canBeWrittenAsAbundantSum[i]) {
                totalSum += i;
            }
        }
        return totalSum;
    }

    /// <summary>
    /// Checks if a number is abundant (sum of proper divisors > n).
    /// </summary>
    private bool IsAbundant(int n) {
        return Library.CreateDivisorSumCache(n).GetDivisorSum(n) > n;
    }
}
