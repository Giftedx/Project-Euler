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
        return LargestPalindromeProduct(100, 1000);
    }

    /// <summary>
    /// Finds the largest palindrome made from the product of two numbers within a specified range.
    /// The method iterates downwards for both factors. For 6-digit palindromes (common in this problem type),
    /// at least one factor must be a multiple of 11; this optimization is applied to the first factor 'i'.
    /// An additional optimization breaks the inner loop if the current product is less than the largest palindrome found so far.
    /// </summary>
    /// <param name="minFactor">The minimum value for the factors (inclusive).</param>
    /// <param name="maxFactorExclusive">The maximum value for the factors (exclusive).</param>
    /// <returns>The largest palindrome product found, or 0 if none.</returns>
    private int LargestPalindromeProduct(int minFactor, int maxFactorExclusive) {
        int lpp = 0;
        
        // Optimization for 6-digit palindromes: one factor must be a multiple of 11.
        // Let P = abccba = 100000a + 10000b + 1000c + 100c + 10b + a
        // P = 100001a + 10010b + 1100c
        // P = 11 * (9091a + 910b + 100c)
        // So, P must be divisible by 11. If P = i * j, then either i or j (or both) must be divisible by 11.
        // We optimize by making factor 'i' iterate over multiples of 11.

        // Determine range for factor 'i', which must be a multiple of 11.
        // Max 'i' is the largest multiple of 11 less than or equal to (maxFactorExclusive - 1).
        int i_max = maxFactorExclusive - 1;
        int i_start = i_max - (i_max % 11);

        // Min 'i' is the smallest multiple of 11 greater than or equal to minFactor.
        // However, the outer loop for 'i' iterates downwards.
        // The inner loop for 'j' also iterates downwards.
        // The problem context (product of two 3-digit numbers) means minFactor is 100, maxFactorExclusive is 1000.
        // Smallest 3-digit multiple of 11 is 110. Largest is 990.
        // Smallest 3-digit number is 100. Largest is 999.

        // Iterate 'i' downwards from the largest multiple of 11 within range.
        for (int i = i_start; i >= minFactor; i -= 11) {
            // Iterate 'j' downwards from maxFactorExclusive - 1 to minFactor
            // No need for j to be a multiple of 11, as 'i' already covers that.
            for (int j = maxFactorExclusive - 1; j >= minFactor; j--) {
                // Optimization: if i*j is already less than lpp, and j is decreasing,
                // further products i*j' (where j' < j) will also be less than lpp.
                // So, we can break from the inner loop.
                // Also, if i is already smaller than j to a degree that i*i < lpp (approx),
                // we might not find a new lpp.
                // Consider the case where i * (maxFactorExclusive - 1) < lpp.
                // If this is true, then no product involving i can be greater than lpp,
                // so we could potentially break the outer loop for i as well.
                if ((long)i * (maxFactorExclusive - 1) < lpp) {
                    // If the largest possible product for this 'i' is already less than lpp,
                    // then decreasing 'i' further won't help.
                    // Exit the method early as no larger palindrome can be found.
                    return lpp;
                }

                int product = i * j;
                
                // Optimization: if current product is already less than or equal to the largest palindrome found so far,
                // no need to check smaller j for this i, as products will only get smaller.
                if (product <= lpp) {
                    break; 
                }

                if (Library.IsPalindrome(product)) {
                    lpp = product;
                }
            }
        }
        return lpp;
    }
}