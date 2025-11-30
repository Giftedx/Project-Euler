namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 33: Digit cancelling fractions.
/// Finds the denominator of the product of the four non-trivial examples.
/// </summary>
public class Problem033 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 33: Digit cancelling fractions.
    /// </summary>
    /// <returns>The denominator of the product of the fractions.</returns>
    public override object Solve() {
        return DigitCancellingFractions();
    }

    /// <summary>
    /// Finds fractions where cancelling a digit gives correct simplified value.
    /// </summary>
    private int DigitCancellingFractions() {
        int numProduct = 1;
        int denProduct = 1;

        for (int den = 11; den < 100; den++) {
            for (int num = 10; num < den; num++) {
                int n0 = num % 10;
                int n1 = num / 10;
                int d0 = den % 10;
                int d1 = den / 10;

                if (n0 == 0 && d0 == 0) continue;

                if (n0 == d1 && (double)n1 / d0 == (double)num / den) {
                    numProduct *= num;
                    denProduct *= den;
                } else if (n1 == d0 && (double)n0 / d1 == (double)num / den) {
                    numProduct *= num;
                    denProduct *= den;
                }
            }
        }

        return denProduct / Library.Gcd(numProduct, denProduct);
    }
}
