namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 43: Sub-string divisibility.
/// Finds the sum of all 0 to 9 pandigital numbers with a specific sub-string divisibility property.
/// </summary>
public class Problem043 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 43: Sub-string divisibility.
    /// </summary>
    /// <returns>The sum of all numbers with the property.</returns>
    public override object Solve() {
        return SumSubStringDivisible();
    }

    /// <summary>
    /// Finds sum of 0-9 pandigital numbers satisfying divisibility rules.
    /// </summary>
    private long SumSubStringDivisible() {
        int[] digits = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        long sum = 0;
        int[] divisors = { 2, 3, 5, 7, 11, 13, 17 };

        do {
            if (digits[0] == 0) continue;

            bool ok = true;
            for (int i = 0; i < 7; i++) {
                int n = digits[i + 1] * 100 + digits[i + 2] * 10 + digits[i + 3];
                if (n % divisors[i] != 0) {
                    ok = false;
                    break;
                }
            }

            if (ok) {
                long num = 0;
                foreach (int d in digits) num = num * 10 + d;
                sum += num;
            }
        } while (Library.Permute(digits));

        return sum;
    }
}
