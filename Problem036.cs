namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 36: Double-base palindromes.
/// Finds the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.
/// (Leading zeros are not allowed in either base).
/// Further details can be found at https://projecteuler.net/problem=36
/// </summary>
public class Problem036 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 36.
    /// It sums all numbers less than one million that are palindromic in both base 10 and base 2.
    /// </summary>
    /// <returns>The sum of all double-base palindromes less than one million.</returns>
    public override object Solve() {
        return DoubleBasePalindromeSum(1000000);
    }

    /// <summary>
    /// Calculates the sum of all numbers less than a given <paramref name="upperLimit"/>
    /// which are palindromic in both base 10 and base 2.
    /// The method generates base 10 palindromes first, then checks if they are binary palindromes
    /// and within the specified <paramref name="upperLimit"/>.
    /// </summary>
    /// <param name="upperLimit">The exclusive upper limit for the numbers to be summed.</param>
    /// <returns>The sum of all numbers satisfying the double-base palindrome condition below <paramref name="upperLimit"/>.</returns>
    private long DoubleBasePalindromeSum(int upperLimit) {
        long sum = 0;

        // Iterate through possible lengths of decimal palindromes.
        // Numbers are less than 1,000,000. Max 6-digit palindrome is 999999.
        // A 7-digit palindrome (e.g., 1xxxxxx1) would be >= 1,000,000.
        for (int len = 1; len <= 6; len++) {
            bool isOddLength = len % 2 == 1;
            int firstHalfLength = (len + 1) / 2; // Length of the first half of digits that define the palindrome
                                               // e.g., for len=5 (abcba), firstHalfLength=3 (abc)
                                               // e.g., for len=6 (abccba), firstHalfLength=3 (abc)

            // Determine the range for the first half of the palindrome.
            // e.g., for firstHalfLength=1 (len=1 or 2), i ranges 1-9
            // e.g., for firstHalfLength=2 (len=3 or 4), i ranges 10-99
            // e.g., for firstHalfLength=3 (len=5 or 6), i ranges 100-999
            int startNumForHalf = Library.Pow10(firstHalfLength - 1);
            int endNumForHalf = Library.Pow10(firstHalfLength);

            for (int firstHalf = startNumForHalf; firstHalf < endNumForHalf; firstHalf++) {
                int decimalPalindrome = MakeDecimalPalindrome(firstHalf, isOddLength);
                if (decimalPalindrome < upperLimit && IsBinaryPalindrome(decimalPalindrome)) {
                    sum += decimalPalindrome;
                }
            }
        }
        return sum;
    }

    /// <summary>
    /// Constructs a base 10 palindrome from its first half of digits.
    /// For example, if <paramref name="firstHalfValue"/> is 123:
    /// - and <paramref name="isOddLength"/> is true, it forms 12321.
    /// - and <paramref name="isOddLength"/> is false, it forms 123321.
    /// </summary>
    /// <param name="firstHalfValue">An integer representing the first half of the digits of the palindrome.
    /// For example, for 12321, firstHalfValue is 123. For 123321, firstHalfValue is 123.</param>
    /// <param name="isOddLength">True if the palindrome to be constructed has an odd number of digits;
    /// false if it has an MenuView number of digits.</param>
    /// <returns>The constructed base 10 palindrome.</returns>
    private int MakeDecimalPalindrome(int firstHalfValue, bool isOddLength) { // Renamed parameters for clarity
        int result = firstHalfValue;
        int tempHalf = firstHalfValue;

        // If the palindrome length is odd, the middle digit of 'firstHalfValue' is already accounted for
        // in 'result'. We need to reverse the digits of 'firstHalfValue' excluding its last digit.
        // e.g., firstHalfValue = 123 (for abcba), tempHalf becomes 12 to reverse and append '21'.
        if (isOddLength) {
            tempHalf /= 10;
        }

        // Reverse tempHalf and append its digits to result.
        while (tempHalf > 0) {
            result = result * 10 + (tempHalf % 10);
            tempHalf /= 10;
        }
        return result;
    }

    /// <summary>
    /// Checks if a given integer <paramref name="n"/> is a palindrome in its binary representation.
    /// Leading zeros in the binary representation are not considered part of the palindrome test
    /// (e.g., binary 101 is a palindrome; 0101 is treated as 101).
    /// </summary>
    /// <param name="n">The integer to check. Must be non-negative.</param>
    /// <returns>True if <paramref name="n"/> is a binary palindrome; false otherwise.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if n is negative.</exception>
    private bool IsBinaryPalindrome(int n) {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "Input must be non-negative.");
        if (n == 0) return true; // 0 is palindromic in binary (0) and decimal (0).

        int reversedBits = 0;
        int originalNumber = n; // Keep original n for final comparison.

        while (originalNumber > 0) {
            reversedBits <<= 1;           // Left shift reversedBits to make space for the next bit.
            reversedBits |= (originalNumber & 1); // Take the last bit of originalNumber and set it as the last bit of reversedBits.
            originalNumber >>= 1;         // Right shift originalNumber to remove its last bit.
        }
        // Check if the original number (with leading zeros effectively removed by the loop) equals its bitwise reverse.
        return n == reversedBits;
    }
}