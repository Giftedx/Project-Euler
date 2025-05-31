// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

namespace Project_Euler;

public class Problem023 : Problem {
    private const int Limit = 28123;
    private readonly int[] _properDivisorSum = new int[Limit];

    public Problem023() {
        for (int i = 1; i < Limit; i++)
        for (int j = 2 * i; j < Limit; j += i)
            _properDivisorSum[j] += i;
    }

    public override object Solve() {
        return SumOfNonAbundantBelow();
    }

    private int SumOfNonAbundantBelow() {
        // Initialize with a capacity based on the known approximate count of abundant numbers.
        List<int> abundantNumbers = new List<int>(7000);
        // Smallest abundant number is 12.
        // Populate list of abundant numbers up to Limit - 1.
        // Inlined IsAbundant(n) logic: _properDivisorSum[n] > n
        // n is always within bounds [12, Limit-1] here, so no need for n <= 0 || n >= Limit check.
        for (int n = 12; n < Limit; n++) {
            if (_properDivisorSum[n] > n) {
                abundantNumbers.Add(n);
            }
        }

        bool[] isSumOfTwoAbundants = new bool[Limit]; // Default false

        for (int i = 0; i < abundantNumbers.Count; i++) {
            int a = abundantNumbers[i];
            // Optimization: if a + a (smallest sum with current 'a' and another abundant number starting from a) >= Limit
            if (a + a >= Limit) {
                 break; // All further 'a' will also result in sums >= Limit with any b >= a.
            }
            for (int j = i; j < abundantNumbers.Count; j++) {
                int b = abundantNumbers[j];
                int currentSum = a + b;

                if (currentSum < Limit) {
                    isSumOfTwoAbundants[currentSum] = true;
                } else {
                    // Since abundantNumbers is sorted, if a+b >= Limit,
                    // then a + (any subsequent b) will also be >= Limit.
                    // So we can break the inner loop for 'b'.
                    break;
                }
            }
        }

        int totalSum = 0;
        // Sum all positive integers up to Limit-1 that cannot be written as the sum of two abundant numbers.
        for (int i = 1; i < Limit; i++) {
            if (!isSumOfTwoAbundants[i]) {
                totalSum += i;
            }
        }
        return totalSum;
    }
}