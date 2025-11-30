namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 40: Champernowne's constant.
/// Finds the value of the expression d1 * d10 * d100 * d1000 * d10000 * d100000 * d1000000.
/// </summary>
public class Problem040 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 40: Champernowne's constant.
    /// </summary>
    /// <returns>The product of the specified digits.</returns>
    public override object Solve() {
        return CalculateDigitProduct();
    }

    /// <summary>
    /// Calculates product of digits at indices 1, 10, 100, etc.
    /// </summary>
    private long CalculateDigitProduct() {
        long product = 1;
        for (int position = 1; position <= 1000000; position *= 10) {
            product *= FindNthDigitInChampernowneConstant(position);
        }
        return product;
    }

    /// <summary>
    /// Finds the nth digit of Champernowne's constant.
    /// </summary>
    private int FindNthDigitInChampernowneConstant(int nthPosition) {
        int digitsInCurrentBlockLength = 1;
        long numbersInBlock = 9;
        long startNumberInBlock = 1;

        while (nthPosition > digitsInCurrentBlockLength * numbersInBlock) {
            nthPosition -= (int)(digitsInCurrentBlockLength * numbersInBlock);
            digitsInCurrentBlockLength++;
            numbersInBlock *= 10;
            startNumberInBlock *= 10;
        }

        long targetNumber = startNumberInBlock + (nthPosition - 1) / digitsInCurrentBlockLength;
        int digitIndexFromLeft = (nthPosition - 1) % digitsInCurrentBlockLength;

        int digitsToRemoveFromRight = digitsInCurrentBlockLength - 1 - digitIndexFromLeft;
        for (int i = 0; i < digitsToRemoveFromRight; i++) {
            targetNumber /= 10;
        }
        return (int)(targetNumber % 10);
    }
}
