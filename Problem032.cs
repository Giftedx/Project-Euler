namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 32: Pandigital products.
/// Finds the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.
/// For example, 39 × 186 = 7254 uses each digit from 1 to 9 exactly once.
/// Further details can be found at https://projecteuler.net/problem=32
/// </summary>
public class Problem032 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 32.
    /// It identifies all products `c = a * b` where `a`, `b`, and `c` together form a 1-9 pandigital set of digits,
    /// and then sums these unique products.
    /// </summary>
    /// <returns>The sum of all unique pandigital products.</returns>
    public override object Solve() {
        return SumPandigitalProducts();
    }

    /// <summary>
    /// Finds all products `c = a * b` such that the collective digits of `a`, `b`, and `c` are
    /// a permutation of digits 1 through 9 exactly once. It then sums these unique products.
    /// The search considers two main cases for the number of digits in `a` and `b`:
    /// 1. `a` has 1 digit, `b` has 4 digits, product `c` must have 4 digits (1 + 4 + 4 = 9 total digits).
    /// 2. `a` has 2 digits, `b` has 3 digits, product `c` must have 4 digits (2 + 3 + 4 = 9 total digits).
    /// Other cases (e.g., 1x3=5) are not possible or are covered by symmetry.
    /// </summary>
    /// <returns>The sum of all unique products `c` that satisfy the pandigital condition.</returns>
    private int SumPandigitalProducts() {
        // HashSet to store unique products that satisfy the pandigital condition.
        HashSet<int> pandigitalProducts = [];

        // Case 1: a (1 digit) * b (4 digits) = c (4 digits)
        // 'a' ranges from 1 to 9.
        // 'b' ranges to ensure 4 digits, e.g., 1234 to 9876.
        // Product 'c' must be 4 digits (max 9 * 9876 = 88884, so c <= 9876).
        for (int a = 1; a <= 9; a++) { // Single digit multiplicand
            for (int b = 1234; b <= 9876; b++) { // Four-digit multiplier
                int product = a * b;
                if (product > 9876) continue; // Product must also be 4 digits for 1+4+4=9.
                                            // Also, if product is too large, the pandigital check might be slow or fail early.
                                            // A tighter upper bound for b could be 9876/a.
                if (IsPandigital(a, b, product)) {
                    pandigitalProducts.Add(product);
                }
            }
        }

        // Case 2: a (2 digits) * b (3 digits) = c (4 digits)
        // 'a' ranges from 12 to 98.
        // 'b' ranges from 123 to 987.
        // Product 'c' must be 4 digits (max 98 * 987 = 96726, so c <= 9876).
        for (int a = 12; a <= 98; a++) { // Two-digit multiplicand
            // Smallest 'a' is 12. Smallest 'b' is 123. 12*123 = 1476.
            // Largest 'a' is 98. Largest 'b' is 987. 98*987 = 96726 (5 digits - too many).
            // So, product 'c' must be 4 digits. Max c is 9876.
            // Max b is 9876/a.
            for (int b = 123; b <= 9876 / a && b <= 987 ; b++) { // Three-digit multiplier
                int product = a * b;
                if (product > 9876) continue; // Product must be 4 digits for 2+3+4=9.
                                            // This also ensures c doesn't get too large.
                if (IsPandigital(a, b, product)) {
                    pandigitalProducts.Add(product);
                }
            }
        }
        return pandigitalProducts.Sum();
    }

    /// <summary>
    /// Checks if the collective digits of integers a, b, and c form a 1-9 pandigital set.
    /// This means every digit from 1 to 9 must be used exactly once among a, b, and c,
    /// and the digit 0 must not be used.
    /// </summary>
    /// <param name="a">The first integer (multiplicand).</param>
    /// <param name="b">The second integer (multiplier).</param>
    /// <param name="c">The third integer (product).</param>
    /// <returns>True if a, b, and c together are 1-9 pandigital; false otherwise.</returns>
    private bool IsPandigital(int a, int b, int c) {
        // Span to act as a frequency map for digits 1-9. Index 0 is unused.
        // digits[d] will be 1 if digit 'd' is seen, >1 if repeated.
        Span<byte> digitsOccurrence = stackalloc byte[10];
        int distinctDigitCount = 0; // Counts the number of unique digits from 1-9 encountered.

        // Process digits for a, b, and c. If CheckDigits returns false at any point,
        // it means a zero was found or a digit was repeated, so it's not pandigital.
        if (!CheckDigits(a, digitsOccurrence, ref distinctDigitCount)) return false;
        if (!CheckDigits(b, digitsOccurrence, ref distinctDigitCount)) return false;
        if (!CheckDigits(c, digitsOccurrence, ref distinctDigitCount)) return false;

        // For a 1-9 pandigital number, exactly 9 distinct non-zero digits must be present.
        return distinctDigitCount == 9;
    }

    /// <summary>
    /// Processes the digits of a given <paramref name="number"/>, updating a frequency map
    /// and a count of total distinct digits encountered so far across multiple numbers.
    /// It ensures that no digit is 0 and that no digit (from 1-9) is repeated
    /// (by checking against the <paramref name="digitsFrequencyMap"/> which is shared across calls for a,b,c).
    /// </summary>
    /// <param name="number">The integer whose digits are to be checked.</param>
    /// <param name="digitsFrequencyMap">A Span of bytes acting as a frequency map for digits 0-9.
    /// `digitsFrequencyMap[d]` is incremented when digit `d` is found.
    /// Index 0 is for digit 0; indices 1-9 for digits 1-9.</param>
    /// <param name="totalDistinctDigits">A reference to an integer that counts the total number of
    /// distinct non-zero digits processed across all calls for a given set (a,b,c).</param>
    /// <returns>True if all digits in <paramref name="number"/> are non-zero and not previously seen
    /// (i.e., their count in <paramref name="digitsFrequencyMap"/> was 0 before processing this digit).
    /// False if a 0 is encountered or a digit is repeated.</returns>
    private bool CheckDigits(int number, Span<byte> digitsFrequencyMap, ref int totalDistinctDigits) {
        if (number == 0) return false; // Number itself cannot be 0 if it contributes digits.

        while (number > 0) {
            int digit = number % 10;
            // Condition for pandigital 1-9: digit 0 is not allowed.
            // Also, if digitsFrequencyMap[digit] is already 1 (or more), this digit is a repeat.
            if (digit == 0 || digitsFrequencyMap[digit] > 0) { // Check before incrementing
                return false; // Digit is 0 or is a repeat.
            }
            digitsFrequencyMap[digit]++; // Mark this digit as seen once.
            number /= 10;
            totalDistinctDigits++; // Increment count of distinct digits found so far.
        }
        return true;
    }
}