namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 40: Champernowne's constant.
/// Champernowne's constant is an irrational decimal fraction created by concatenating positive integers:
/// 0.123456789101112131415161718192021...
/// The problem asks for the value of the expression d₁ × d₁₀ × d₁₀₀ × d₁₀₀₀ × d₁₀₀₀₀ × d₁₀₀₀₀₀ × d₁₀₀₀₀₀₀,
/// where dₙ represents the nth digit of the fractional part.
/// Further details can be found at https://projecteuler.net/problem=40
/// </summary>
public class Problem040 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 40.
    /// It finds the product of specific digits (d₁, d₁₀, ..., d₁₀₀₀₀₀₀) from Champernowne's constant.
    /// </summary>
    /// <returns>The product of the specified digits of Champernowne's constant.</returns>
    public override object Solve() {
        return CalculateDigitProduct(); // Renamed for clarity
    }

    /// <summary>
    /// Calculates the product of specified digits from Champernowne's constant.
    /// The digits are d₁, d₁₀, d₁₀₀, d₁₀₀₀, d₁₀₀₀₀, d₁₀₀₀₀₀, and d₁₀₀₀₀₀₀.
    /// Each required digit is found using <see cref="FindNthDigitInChampernowneConstant"/>.
    /// </summary>
    /// <returns>The product of these digits as a long integer.</returns>
    private long CalculateDigitProduct() { // Renamed for clarity
        long product = 1;
        // Iterate through the powers of 10 for the positions of digits to find (1, 10, 100, ... , 1,000,000)
        for (int position = 1; position <= 1000000; position *= 10) {
            product *= FindNthDigitInChampernowneConstant(position);
        }
        return product;
    }

    /// <summary>
    /// Finds the digit at the <paramref name="nthPosition"/> in the fractional part of Champernowne's constant.
    /// Champernowne's constant is 0.123456789101112...
    ///
    /// Algorithm:
    /// 1. Determine the "block" of numbers containing the nth digit:
    ///    - 1-digit numbers (1-9): contribute 9 * 1 = 9 digits.
    ///    - 2-digit numbers (10-99): contribute 90 * 2 = 180 digits.
    ///    - 3-digit numbers (100-999): contribute 900 * 3 = 2700 digits, and so on.
    ///    The method iterates, subtracting these counts from <paramref name="nthPosition"/> until
    ///    <paramref name="nthPosition"/> falls within the current block of `currentLength`-digit numbers.
    /// 2. Identify the specific number within that block:
    ///    - `startNumberInBlock`: The first number in the current block (e.g., 1, 10, 100).
    ///    - `(nthPosition - 1) / currentLength`: Determines which number (0-indexed) in this block contains the digit.
    ///      This value is added to `startNumberInBlock` to get `targetNumber`.
    /// 3. Extract the specific digit from `targetNumber`:
    ///    - `(nthPosition - 1) % currentLength`: Determines the 0-indexed position of the digit within `targetNumber` (from left).
    ///    - The code then isolates this digit by repeatedly dividing `targetNumber` by 10 until the desired digit
    ///      is at the units place, then takes `targetNumber % 10`.
    /// </summary>
    /// <param name="nthPosition">The 1-based position of the desired digit in Champernowne's constant's fractional part.</param>
    /// <returns>The digit at the specified <paramref name="nthPosition"/>.</returns>
    private int FindNthDigitInChampernowneConstant(int nthPosition) { // Renamed for clarity
        int digitsInCurrentBlockLength = 1; // Length of numbers in current block (1 for 1-9, 2 for 10-99, etc.)
        long numbersInBlock = 9;          // Count of numbers in current block (9 for 1-digit, 90 for 2-digits, etc.)
        long startNumberInBlock = 1;        // Starting number of the current block (1, 10, 100, etc.)

        // Determine which block of numbers (1-digit, 2-digit, etc.) the nthPosition falls into.
        while (nthPosition > digitsInCurrentBlockLength * numbersInBlock) {
            nthPosition -= (int)(digitsInCurrentBlockLength * numbersInBlock); // Adjust position relative to start of next block
            digitsInCurrentBlockLength++;
            numbersInBlock *= 10;
            startNumberInBlock *= 10;
        }

        // Calculate the actual number that contains the nthPosition-th digit.
        // (nthPosition - 1) because nthPosition is 1-based.
        // (nthPosition - 1) / digitsInCurrentBlockLength gives the 0-indexed offset from startNumberInBlock.
        long targetNumber = startNumberInBlock + (nthPosition - 1) / digitsInCurrentBlockLength;

        // Determine which digit within targetNumber is the one we're looking for.
        // (nthPosition - 1) % digitsInCurrentBlockLength gives the 0-indexed position of the digit from the left.
        int digitIndexFromLeft = (nthPosition - 1) % digitsInCurrentBlockLength;

        // To get the digit at digitIndexFromLeft, we can convert number to string, or mathematically.
        // Mathematically: extract the digit by appropriately dividing.
        // E.g., if targetNumber=183, length=3, indexFromLeft=0 (1st digit '1').
        // We need to remove (length - 1 - indexFromLeft) digits from the right.
        // (3 - 1 - 0) = 2 digits to remove (8 and 3). 183 / 10^2 = 1. Then 1 % 10 = 1.
        int digitsToRemoveFromRight = digitsInCurrentBlockLength - 1 - digitIndexFromLeft;
        for (int i = 0; i < digitsToRemoveFromRight; i++) {
            targetNumber /= 10;
        }
        return (int)(targetNumber % 10);
    }
}