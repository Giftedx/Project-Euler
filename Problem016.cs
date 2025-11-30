using System.Numerics;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 16: Power digit sum.
/// Finds the sum of the digits of the number 2^1000.
/// </summary>
public class Problem016 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 16: Power digit sum.
    /// Finds the sum of the digits of the number 2^1000.
    /// </summary>
    /// <returns>The sum of the digits of 2^1000.</returns>
    public override object Solve() {
        return PowerDigitSum(1000);
    }

    /// <summary>
    /// Calculates the sum of digits of 2^exponent.
    /// </summary>
    /// <param name="exponent">The exponent to raise 2 to.</param>
    /// <returns>The sum of the digits.</returns>
    private int PowerDigitSum(int exponent) {
        BigInteger number = BigInteger.Pow(2, exponent);
        return Library.SumDigits(number);
    }
}
