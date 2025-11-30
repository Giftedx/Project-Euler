namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 28: Number spiral diagonals.
/// Finds the sum of the numbers on the diagonals in a 1001 by 1001 spiral.
/// </summary>
public class Problem028 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 28: Number spiral diagonals.
    /// </summary>
    /// <returns>The sum of the numbers on the diagonals.</returns>
    public override object Solve() {
        return SpiralSum(1001);
    }

    /// <summary>
    /// Calculates diagonal sum for size n x n spiral.
    /// </summary>
    private int SpiralSum(int size) {
        if (size <= 0 || size % 2 == 0) {
            throw new ArgumentException("Size must be an odd positive integer.", nameof(size));
        }
        if (size == 1) {
            return 1;
        }

        int currentNumber = 1;
        int step = 2;
        int sumOfDiagonals = 1;
        int cornersInCurrentRing = 0;

        while (currentNumber < size * size) {
            currentNumber += step;
            sumOfDiagonals += currentNumber;
            cornersInCurrentRing++;

            if (cornersInCurrentRing == 4) {
                step += 2;
                cornersInCurrentRing = 0;
            }
        }
        return sumOfDiagonals;
    }
}
