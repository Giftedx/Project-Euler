namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 12: Highly divisible triangular number.
/// Finds the value of the first triangle number to have over five hundred divisors.
/// </summary>
public class Problem012 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 12: Highly divisible triangular number.
    /// Finds the value of the first triangle number to have over five hundred divisors.
    /// </summary>
    /// <returns>The first triangle number with over 500 divisors.</returns>
    public override object Solve() {
        return FirstTriangleWithDivisors(500);
    }

    /// <summary>
    /// Finds the first triangular number that has more than 'limit' divisors.
    /// </summary>
    /// <param name="limit">The number of divisors to exceed.</param>
    /// <returns>The first triangular number with more than 'limit' divisors.</returns>
    private int FirstTriangleWithDivisors(int limit) {
        int n = 1;
        int d = 1;

        while (d <= limit) {
            n++;
            int triangleNumber = n * (n + 1) / 2;
            d = CountDivisors(triangleNumber);
        }
        return n * (n + 1) / 2;
    }

    /// <summary>
    /// Counts the number of divisors of a number.
    /// </summary>
    private int CountDivisors(int number) {
        int count = 0;
        int sqrt = (int)Math.Sqrt(number);
        for (int i = 1; i <= sqrt; i++) {
            if (number % i == 0) {
                count += 2;
            }
        }
        if (sqrt * sqrt == number) count--; // Perfect square correction
        return count;
    }
}
