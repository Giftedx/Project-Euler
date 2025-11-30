namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 36: Double-base palindromes.
/// Finds the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.
/// </summary>
public class Problem036 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 36: Double-base palindromes.
    /// </summary>
    /// <returns>The sum of all double-base palindromes.</returns>
    public override object Solve() {
        return SumDoubleBasePalindromes(1000000);
    }

    /// <summary>
    /// Finds sum of numbers palindromic in base 10 and base 2.
    /// </summary>
    private int SumDoubleBasePalindromes(int limit) {
        int sum = 0;
        for (int i = 1; i < limit; i++) {
            if (i % 2 == 0) continue;

            if (Library.IsPalindrome(i)) {
                string binary = Convert.ToString(i, 2);
                if (Library.IsPalindrome(binary)) {
                    sum += i;
                }
            }
        }
        return sum;
    }
}
