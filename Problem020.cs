namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 20: Factorial digit sum.
/// Finds the sum of the digits in the number 100!
/// </summary>
public class Problem020 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 20: Factorial digit sum.
    /// </summary>
    /// <returns>The sum of the digits of 100!.</returns>
    public override object Solve() {
        return FactorialDigitSum();
    }

    /// <summary>
    /// Calculates the sum of digits of 100 factorial.
    /// </summary>
    private int FactorialDigitSum() {
        var factorial = Library.Factorial(100);
        return Library.SumDigits(factorial);
    }
}
