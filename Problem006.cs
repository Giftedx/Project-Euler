namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 6: Sum square difference.
/// Finds the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
/// </summary>
public class Problem006 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 6: Sum square difference.
    /// Finds the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
    /// </summary>
    /// <returns>The difference between the square of the sum and the sum of the squares for the first 100 natural numbers.</returns>
    public override object Solve() {
        return SumSquareDifference(100);
    }

    /// <summary>
    /// Calculates the difference between the square of the sum and the sum of the squares for numbers from 1 to n.
    /// Uses closed-form formulas:
    /// Sum(1..n) = n(n+1)/2
    /// Sum(i^2 for i=1..n) = n(n+1)(2n+1)/6
    /// </summary>
    /// <param name="n">The upper limit of the natural numbers.</param>
    /// <returns>The difference (Square of Sum - Sum of Squares).</returns>
    private long SumSquareDifference(int n) {
        long sum = n * (n + 1) / 2;
        long squareOfSum = sum * sum;

        long sumOfSquares = n * (n + 1) * (2 * n + 1) / 6;

        return squareOfSum - sumOfSquares;
    }
}
