namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 20: Factorial digit sum.
/// Further details can be found at https://projecteuler.net/problem=20
/// </summary>
public class Problem020 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 20: Factorial digit sum.
    /// Calculates the sum of the digits in the number 100! (100 factorial).
    /// </summary>
    /// <returns>The sum of the digits of 100!.</returns>
    public override object Solve() {
        // The method name ExponentSum is a slight misnomer from a previous problem structure,
        // as this problem deals with Factorial digit sum.
        return FactorialDigitSum();
    }

    /// <summary>
    /// Calculates the sum of the digits of 100 factorial (100!).
    /// First, it computes 100! using <see cref="Library.Factorial(int)"/>, which returns a <see cref="System.Numerics.BigInteger"/>.
    /// Then, it sums the digits of this large integer result using <see cref="Library.SumDigits(System.Numerics.BigInteger)"/>.
    /// </summary>
    /// <returns>The sum of the digits of 100!.</returns>
    private int FactorialDigitSum() { // Renamed from ExponentSum for clarity
        var factorialResult = Library.Factorial(100);
        return Library.SumDigits(factorialResult);
    }
}