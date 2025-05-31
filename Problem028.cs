namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 28: Number spiral diagonals.
/// Calculates the sum of the numbers on the diagonals in a square spiral formed by starting with the number 1
/// and moving to the right in a clockwise direction.
/// Further details can be found at https://projecteuler.net/problem=28
/// </summary>
public class Problem028 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 28.
    /// It finds the sum of the numbers on the diagonals in a 1001x1001 spiral
    /// formed by starting with 1 and spiralling outwards clockwise.
    /// </summary>
    /// <returns>The sum of the numbers on the diagonals of the 1001x1001 spiral.</returns>
    public override object Solve() {
        return SpiralSum(1001); // For a 1001x1001 spiral
    }

    /// <summary>
    /// Calculates the sum of the numbers on the diagonals of a square spiral of a given odd <paramref name="size"/>.
    /// The spiral starts with 1 at the center and spirals outwards in a clockwise direction.
    /// The algorithm iteratively generates the numbers on the diagonals:
    /// - It starts with the center number 1.
    /// - For each subsequent "ring" or layer of the spiral, there are four diagonal numbers.
    /// - The `step` between diagonal numbers increases by 2 after each full ring (4 corners) is completed.
    /// Example for a 5x5 spiral:
    /// Ring 0 (center): 1 (currentNumber=1, total=1)
    /// Ring 1 (step=2):
    ///   - 1 + 2 = 3 (total=1+3=4)
    ///   - 3 + 2 = 5 (total=4+5=9)
    ///   - 5 + 2 = 7 (total=9+7=16)
    ///   - 7 + 2 = 9 (total=16+9=25) -> ringStep=4, step becomes 2+2=4
    /// Ring 2 (step=4):
    ///   - 9 + 4 = 13 (total=25+13=38)
    ///   - ...and so on, until currentNumber exceeds size*size.
    /// </summary>
    /// <param name="size">The dimension of the square spiral (e.g., 5 for a 5x5 spiral). Must be an odd positive integer.</param>
    /// <returns>The sum of the numbers on both diagonals of the spiral.</returns>
    /// <exception cref="ArgumentException">Thrown if size is not an odd positive integer.</exception>
    private int SpiralSum(int size) {
        if (size <= 0 || size % 2 == 0) {
            throw new ArgumentException("Size must be an odd positive integer.", nameof(size));
        }
        if (size == 1) {
            return 1; // Special case for 1x1 spiral, sum is 1.
        }

        int currentNumber = 1; // Starts with 1 at the center.
        int step = 2;          // Initial step size to get to the next diagonal number from the center.
        int sumOfDiagonals = 1; // Initialize sum with the center element '1'.
        int cornersInCurrentRing = 0; // Counts corners (diagonal elements) in the current ring/layer.

        // Loop until the currentNumber generated exceeds the largest number in the spiral (size*size).
        // The first number (1) is already added. The loop starts generating the next numbers.
        while (currentNumber < size * size) {
            currentNumber += step; // Move to the next diagonal number.
            sumOfDiagonals += currentNumber;
            cornersInCurrentRing++;

            // After adding 4 corners for a ring, the step size increases for the next larger ring.
            if (cornersInCurrentRing == 4) {
                step += 2;             // Increase step for the next ring.
                cornersInCurrentRing = 0; // Reset corner count for the new ring.
            }
        }
        return sumOfDiagonals;
    }
}