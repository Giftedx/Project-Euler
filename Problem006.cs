namespace Project_Euler;

public class Problem006 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 6: Sum square difference.
    /// Finds the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
    /// </summary>
    /// <returns>The difference between the sum of the squares and the square of the sum for the first 100 natural numbers.</returns>
    public override object Solve() {
        return CalculateSumSquareDifference(100);
    }

    private long CalculateSumSquareDifference(int n) {
        long n_long = n; // Use long for calculations to prevent overflow

        long sum_of_n = n_long * (n_long + 1) / 2;
        long square_of_sum = sum_of_n * sum_of_n;

        long sum_of_squares_n = n_long * (n_long + 1) * (2 * n_long + 1) / 6;

        return square_of_sum - sum_of_squares_n;
    }
}