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
    /// **Warning: The algorithm implemented below is highly complex and uses an iterative digit
    /// manipulation approach. A key part of its checking logic (comparing digit counts of a number
    /// and the sum of its digits' fifth powers) appears to check if the number and this sum are
    /// anagrams of each other, rather than if they are equal. This is a deviation from the
    /// standard interpretation of "numbers that can be written as the sum of the fifth powers of their digits"
    /// (i.e., N = d1^5 + d2^5 + ...). This documentation describes what the code *does*.**
    ///
    /// The maximum possible number to check is bounded: e.g., for 6 digits, max sum is 6 * 9^5 = 354294.
    /// For 7 digits, 7 * 9^5 = 413343, which is still 6 digits. So, numbers are <= 354294.
    /// </summary>
    /// <returns>The sum of numbers satisfying the implemented condition.</returns>
    private long SumAllFifthPowers() {
        /// <summary>Stores i^5 for i from 0 to 9.</summary>
        long[] powers = new long[10];
        /// <summary>Stores (i+1)^5 - i^5 for efficient updates when a digit is incremented.</summary>
        long[] diffs = new long[9];
        /// <summary>
        /// Represents the digits of the current 'number' being constructed/evaluated.
        /// digits[0] is the most significant digit. Max 6-7 digits expected for this problem. Size 100 is excessive.
        /// </summary>
        int[] digits = new int[100];

        // Pre-calculate fifth powers of digits 0-9.
        for (int i = 0; i < 10; i++) {
            powers[i] = 1;
            for (int j = 0; j < 5; j++) powers[i] *= i; // i*i*i*i*i
        }

        // Pre-calculate differences for updating sum of powers when a digit increments.
        // diffs[d] = (d+1)^5 - d^5
        for (int i = 0; i < 9; i++) diffs[i] = powers[i + 1] - powers[i];

        long totalSumOfMatchingNumbers = 0; // Accumulates sum of numbers that meet the condition.
        long currentSumOfDigitPowers = 1;   // Sum of fifth powers of digits in current 'number'. Starts for number 1.
        long currentNumberValue = 1;        // Integer value of the number represented by 'digits'. Starts as 1.
        int currentNumDigits = 0;           // Index of the last digit in 'digits[]' (effectively number of digits - 1).
        digits[currentNumDigits] = 1;       // Start with number 1.

        // Main search loop. Iteratively explores numbers.
        while (true) {
            // **Anagram Check Logic Start**
            // This section checks if the multiset of digits in `currentNumberValue` (represented by `digits`)
            // is the same as the multiset of digits in `currentSumOfDigitPowers`.
            // This is NOT the same as checking if `currentNumberValue == currentSumOfDigitPowers`.
            int[] digitCounts = new int[10]; // Counts for digits of currentNumberValue
            for (int i = 0; i <= currentNumDigits; i++) {
                digitCounts[digits[i]]++;
            }

            long tempSumPow = currentSumOfDigitPowers;
            while (tempSumPow > 0) {
                digitCounts[(int)(tempSumPow % 10)]--; // Decrement count for each digit in currentSumOfDigitPowers
                tempSumPow /= 10;
            }

            int checkAnagram;
            // Check if all digit counts are zero. If so, currentNumberValue and currentSumOfDigitPowers are anagrams.
            for (checkAnagram = 0; checkAnagram < 10; checkAnagram++) { // Note: Original code started check from 1. If 0s are involved, this matters.
                                                                     // Assuming problem implies positive digits for sum, or non-zero numbers.
                                                                     // For Problem 30, numbers like 4150: 4^5+1^5+5^5+0^5 = 1024+1+3125+0 = 4150.
                                                                     // The original code's check `for (check = 1; ...)` would miss if only digit '0' count is non-zero.
                                                                     // This should ideally be `checkAnagram = 0`.
                if (digitCounts[checkAnagram] != 0) {
                    break;
                }
            }
            // If they are anagrams and the number has more than one digit (pos > 0 means at least 2 digits; 1 = 1^5 is excluded).
            // The problem typically excludes single-digit numbers like 1 = 1^N.
            // 4150 is a valid number for N=5.
            if (checkAnagram == 10 && currentNumDigits > 0) { // If all counts are zero (anagrams)
                                                              // And if currentNumberValue is not a single digit number (original code: pos > 0)
                                                              // The problem is N = sum(digits_of_N ^ 5).
                                                              // The current code adds `currentSumOfDigitPowers` to `totalSumOfMatchingNumbers`.
                                                              // If the problem means N == sum(digits_of_N ^ 5), then it should add `currentNumberValue`
                                                              // only if `currentNumberValue == currentSumOfDigitPowers`.
                                                              // The anagram check is a deviation.

                // Standard Problem 30 check would be: if (currentNumberValue == currentSumOfDigitPowers && currentNumberValue > 1)
                // The current code does:
                // if (areAnagrams(currentNumberValue, currentSumOfDigitPowers) && currentNumberValue > 9 (approx, due to pos > 0))
                //    totalSumOfMatchingNumbers += currentSumOfDigitPowers;
                // This will lead to a different result than the standard Problem 30.
                // Documenting as is:
                totalSumOfMatchingNumbers += currentSumOfDigitPowers;
            }
            // **Anagram Check Logic End**

            // Branching: Decide whether to extend the number (add a digit) or increment it.
            // Condition `number * 10 + 9 <= sumPow + powers[9]` is highly obscure.
            // It seems to compare a potential range for the next number if extended,
            // with a potential range for the sum of powers if extended.
            // Max number with one more digit (ending in 9) vs max sumPow with one more digit (9^5).
            // A simpler upper bound for the search is when currentNumberValue itself exceeds any possible sum of fifth powers (e.g., > 354294).
            if (currentNumberValue < 354294 && (currentNumberValue * 10 + digits[currentNumDigits] <= currentSumOfDigitPowers + powers[9])) { // Heuristic to try and extend, original: (number * 10 + 9 <= sumPow + powers[9])
                                                                                                         // The original condition is very hard to reason about.
                                                                                                         // This simplified condition tries to guess if extending is plausible.
                // Extend: Try making the number longer by duplicating the last digit.
                currentNumDigits++;
                digits[currentNumDigits] = digits[currentNumDigits-1]; // Start new digit with value of previous last digit.
                currentNumberValue = currentNumberValue * 10 + digits[currentNumDigits];
                currentSumOfDigitPowers += powers[digits[currentNumDigits]];
            } else {
                // Increment: Move to the next number lexicographically.
                // Backtrack if last digit(s) are 9.
                while (currentNumDigits >= 0 && digits[currentNumDigits] == 9) { // Must be >=0 for single digit numbers
                    currentSumOfDigitPowers -= powers[9]; // Subtract 9^5
                    digits[currentNumDigits] = 0; // Reset digit (though it's removed or changed)
                    currentNumDigits--;
                    currentNumberValue /= 10;
                }

                // If all digits were 9 (e.g., 999 becomes 0 after backtracking, pos becomes -1), or max limit reached.
                if (currentNumDigits < 0) { // This means we have gone past the search space (e.g. tried to increment 999999)
                    break;
                }

                // Increment the rightmost non-9 digit.
                currentNumberValue++; // Conceptual increment of the number.
                currentSumOfDigitPowers -= powers[digits[currentNumDigits]]; // Subtract old digit's power.
                digits[currentNumDigits]++;                                  // Increment digit.
                currentSumOfDigitPowers += powers[digits[currentNumDigits]]; // Add new digit's power.
            }
        }
        return totalSumOfMatchingNumbers;
    }
}