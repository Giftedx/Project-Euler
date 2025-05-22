namespace Project_Euler;

public class Problem004 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 4: Largest palindrome product.
    /// Finds the largest palindrome made from the product of two 3-digit numbers.
    /// </summary>
    /// <returns>The largest palindrome product of two 3-digit numbers.</returns>
    public override object Solve() {
        return LargestPalindromeProduct(100, 1000);
    }

    private int LargestPalindromeProduct(int minFactor, int maxFactorExclusive) {
        int lpp = 0;
        
        // Determine range for factor 'i', which must be a multiple of 11.
        // Max 'i' is the largest multiple of 11 <= (maxFactorExclusive - 1).
        int i_start = (maxFactorExclusive - 1) / 11 * 11;
        // Min 'i' is the smallest multiple of 11 >= minFactor.
        int i_end = (minFactor % 11 == 0) ? minFactor : (minFactor / 11 + 1) * 11;
        // Ensure i_end is at least the smallest 3-digit multiple of 11 if problem context implies 3-digit numbers.
        // For this problem (3-digit numbers), minFactor is 100, so i_end will be 110.
        if (minFactor <= 110 && i_end < 110) { // Adjust if minFactor is low but i_end calculation is lower than 110
            i_end = 110;
        }


        for (int i = i_start; i >= i_end; i -= 11) {
            // Iterate 'j' downwards from maxFactorExclusive - 1 to minFactor
            for (int j = maxFactorExclusive - 1; j >= minFactor; j--) {
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