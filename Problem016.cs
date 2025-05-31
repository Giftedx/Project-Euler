using System.Numerics;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 16: Power digit sum.
/// Further details can be found at https://projecteuler.net/problem=16
/// </summary>
public class Problem016 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 16: Power digit sum.
    /// Calculates the sum of the digits of the number 2^1000.
    /// </summary>
    /// <returns>The sum of the digits of 2^1000.</returns>
    public override object Solve() {
        return PowerDigitSum(2, 1000);
    }

    /// <summary>
    /// Calculates a number raised to a specified power and then sums the digits of the result.
    /// It uses <see cref="BigInteger.Pow(BigInteger, int)"/> to compute the power, which can result in a very large number.
    /// Then, <see cref="Library.SumDigits(BigInteger)"/> is used to sum the individual digits of this large number.
    /// </summary>
    /// <param name="n">The base number.</param>
    /// <param name="exponent">The exponent to which the base number is raised.</param>
    /// <returns>The sum of the digits of <paramref name="n"/> raised to the power of <paramref name="exponent"/>.</returns>
    private int PowerDigitSum(int n, int exponent) {
        // Calculate n raised to the power of exponent.
        // BigInteger is used because the result can be very large (e.g., 2^1000).
        var number = BigInteger.Pow(n, exponent);

        // Sum the digits of the resulting large number.
        return Library.SumDigits(number);
    }
}