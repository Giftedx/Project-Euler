namespace Project_Euler;

public class Problem020 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 20: Factorial digit sum.
    /// Finds the sum of the digits in the number 100!.
    /// </summary>
    /// <returns>The sum of the digits in 100!.</returns>
    public override object Solve() {
        return CalculateFactorialDigitSum();
    }

    /// <summary>
    /// Calculates 100! and returns the sum of its digits.
    /// This method specifically computes the result for 100 factorial.
    /// </summary>
    /// <returns>The sum of the digits of 100!.</returns>
    private int CalculateFactorialDigitSum() {
        var result = Library.Factorial(100);
        return Library.SumDigits(result);
    }
}