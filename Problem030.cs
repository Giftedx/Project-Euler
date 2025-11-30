namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 30: Digit fifth powers.
/// Finds the sum of all numbers that can be written as the sum of the fifth powers of their digits.
/// </summary>
public class Problem030 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 30: Digit fifth powers.
    /// </summary>
    /// <returns>The sum of all numbers that are equal to the sum of the fifth powers of their digits.</returns>
    public override object Solve() {
        return SumAllFifthPowers();
    }

    /// <summary>
    /// Finds sum of numbers equal to sum of fifth powers of digits.
    /// </summary>
    private long SumAllFifthPowers() {
        long[] powers = new long[10];
        long[] diffs = new long[9];
        int[] digits = new int[100];

        for (int i = 0; i < 10; i++) {
            powers[i] = 1;
            for (int j = 0; j < 5; j++) powers[i] *= i;
        }

        for (int i = 0; i < 9; i++) diffs[i] = powers[i + 1] - powers[i];

        long totalSumOfMatchingNumbers = 0;
        long currentSumOfDigitPowers = 1;
        long currentNumberValue = 1;
        int currentNumDigits = 0;
        digits[currentNumDigits] = 1;

        while (true) {
            int[] digitCounts = new int[10];
            for (int i = 0; i <= currentNumDigits; i++) {
                digitCounts[digits[i]]++;
            }

            long tempSumPow = currentSumOfDigitPowers;
            while (tempSumPow > 0) {
                digitCounts[(int)(tempSumPow % 10)]--;
                tempSumPow /= 10;
            }

            int checkAnagram;
            for (checkAnagram = 0; checkAnagram < 10; checkAnagram++) {
                if (digitCounts[checkAnagram] != 0) {
                    break;
                }
            }

            if (checkAnagram == 10 && currentNumDigits > 0) {
                totalSumOfMatchingNumbers += currentSumOfDigitPowers;
            }

            if (currentNumberValue < 354294 && (currentNumberValue * 10 + digits[currentNumDigits] <= currentSumOfDigitPowers + powers[9])) {
                currentNumDigits++;
                digits[currentNumDigits] = digits[currentNumDigits-1];
                currentNumberValue = currentNumberValue * 10 + digits[currentNumDigits];
                currentSumOfDigitPowers += powers[digits[currentNumDigits]];
            } else {
                while (currentNumDigits >= 0 && digits[currentNumDigits] == 9) {
                    currentSumOfDigitPowers -= powers[9];
                    digits[currentNumDigits] = 0;
                    currentNumDigits--;
                    currentNumberValue /= 10;
                }

                if (currentNumDigits < 0) {
                    break;
                }

                currentNumberValue++;
                currentSumOfDigitPowers -= powers[digits[currentNumDigits]];
                digits[currentNumDigits]++;
                currentSumOfDigitPowers += powers[digits[currentNumDigits]];
            }
        }
        return totalSumOfMatchingNumbers;
    }
}
