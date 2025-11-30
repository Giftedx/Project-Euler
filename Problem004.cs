namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 4: Largest palindrome product.
/// Finds the largest palindrome made from the product of two 3-digit numbers.
/// </summary>
public class Problem004 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 4: Largest palindrome product.
    /// Finds the largest palindrome made from the product of two 3-digit numbers.
    /// </summary>
    /// <returns>The largest palindrome product of two 3-digit numbers.</returns>
    public override object Solve() {
        return LargestPalindromeProduct(3);
    }

    /// <summary>
    /// Finds the largest palindrome made from the product of two numbers with the specified number of digits.
    /// Iterates downwards from the largest possible n-digit numbers to find the maximum palindrome product.
    /// </summary>
    /// <param name="digits">The number of digits for the factors (e.g., 3 for 3-digit numbers).</param>
    /// <returns>The largest palindrome product.</returns>
    private int LargestPalindromeProduct(int digits) {
        int max = Library.Pow10(digits) - 1;
        int min = Library.Pow10(digits - 1);
        int largestPalindrome = 0;

        for (int i = max; i >= min; i--) {
            // Optimization: If i * max is less than the current largest palindrome, we can stop the inner loop/search.
            // But strict optimization isn't critical for 3 digits.
            if (i * max <= largestPalindrome) break;

            for (int j = i; j >= min; j--) {
                int product = i * j;
                if (product <= largestPalindrome) break; // Since j is decreasing, subsequent products will be smaller.

                if (Library.IsPalindrome(product)) {
                    largestPalindrome = product;
                }
            }
        }
        return largestPalindrome;
    }
}
