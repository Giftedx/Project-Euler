namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 30: Digit fifth powers.
/// Finds the sum of all numbers that can be written as the sum of the fifth powers of their digits.
/// For example, 1634 = 1^4 + 6^4 + 3^4 + 4^4.
/// Further details can be found at https://projecteuler.net/problem=30
/// </summary>
public class Problem030 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 30.
    /// It finds all numbers that are equal to the sum of the fifth powers of their digits
    /// and returns the sum of these numbers. (Excludes 1 = 1^5).
    /// </summary>
    /// <returns>The sum of all numbers that are equal to the sum of the fifth powers of their digits.</returns>
    public override object Solve() {
        return SumAllFifthPowers();
    }

    /// <summary>
    /// Finds all numbers that can be written as the sum of the fifth powers of their own digits,
    /// and calculates the sum of these numbers. The number 1 = 1^5 is excluded by convention.
    ///
    /// The maximum possible number to check is bounded.
    /// 9^5 = 59049.
    /// For a 6-digit number, max sum = 6 * 59049 = 354294.
    /// A 7-digit number (>= 1,000,000) can have a max sum of 7 * 59049 = 413343.
    /// Since 1,000,000 > 413343, no 7-digit number can satisfy the property.
    /// Thus, we only need to check numbers up to 354294.
    /// </summary>
    /// <returns>The sum of numbers satisfying the condition.</returns>
    private long SumAllFifthPowers() {
        // Pre-calculate fifth powers of digits 0-9 for efficiency.
        int[] powers = new int[10];
        for (int i = 0; i < 10; i++) {
            powers[i] = i * i * i * i * i;
        }

        long totalSum = 0;
        // Check numbers starting from 2 (since 1 is excluded) up to the bound.
        // We use 355000 as a safe upper bound.
        for (int i = 2; i < 355000; i++) {
            if (IsSumOfFifthPowers(i, powers)) {
                totalSum += i;
            }
        }

        return totalSum;
    }

    private bool IsSumOfFifthPowers(int n, int[] powers) {
        int sum = 0;
        int temp = n;
        while (temp > 0) {
            sum += powers[temp % 10];
            temp /= 10;
        }
        return sum == n;
    }
}