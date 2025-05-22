using System.Numerics;

namespace Project_Euler;

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
    /// Calculates base <paramref name="n"/> raised to the power <paramref name="exponent"/>, 
    /// and then sums the digits of the resulting number.
    /// </summary>
    /// <param name="n">The base.</param>
    /// <param name="exponent">The exponent.</param>
    /// <returns>The sum of the digits of n^exponent.</returns>
    private int PowerDigitSum(int n, int exponent) {
        var digits = BigInteger.Pow(n, exponent);
        return Library.SumDigits(digits);
    }
}