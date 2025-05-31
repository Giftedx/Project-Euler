namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 6: Sum square difference.
/// Further details can be found at https://projecteuler.net/problem=6
/// </summary>
public class Problem006 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 6: Sum square difference.
    /// Finds the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
    /// </summary>
    /// <returns>The difference between the sum of the squares and the square of the sum for the first 100 natural numbers.</returns>
    public override object Solve() {
        return SumSquareDifference(1, 100);
    }

    /// <summary>
    /// Calculates the difference between the square of the sum and the sum of the squares
    /// for all natural numbers within the specified range (inclusive).
    /// </summary>
    /// <param name="min">The minimum number in the range.</param>
    /// <param name="max">The maximum number in the range.</param>
    /// <returns>The difference: (sum of numbers)^2 - (sum of squares of numbers).</returns>
    private long SumSquareDifference(int min, int max) {
        long sumOfSquares = 0;
        long squareOfSum = 0;
        for (int i = min; i <= max; i++) {
            sumOfSquares += (long)i * i; // Cast to long before multiplication to prevent overflow if i*i exceeds int.MaxValue
            squareOfSum += i;
        }

        squareOfSum *= squareOfSum;
        return squareOfSum - sumOfSquares;
    }
}