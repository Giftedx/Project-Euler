namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 9: Special Pythagorean triplet.
/// Finds the Pythagorean triplet for which a + b + c = 1000.
/// </summary>
public class Problem009 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 9: Special Pythagorean triplet.
    /// </summary>
    /// <returns>The product abc of the Pythagorean triplet for which a + b + c = 1000.</returns>
    public override object Solve() {
        return PythagoreanTripletProduct(1000);
    }

    /// <summary>
    /// Finds the product abc of a Pythagorean triplet (a, b, c) where a + b + c = n.
    /// </summary>
    /// <param name="n">The target sum.</param>
    /// <returns>The product a*b*c.</returns>
    private int PythagoreanTripletProduct(int n) {
        // a < b < c
        // a < n/3
        for (int a = 1; a < n / 3; a++) {
            for (int b = a + 1; b < (n - a) / 2; b++) {
                int c = n - a - b;
                if (c > b && IsTriplet(a, b, c)) {
                    return a * b * c;
                }
            }
        }
        return 0;
    }

    /// <summary>
    /// Checks if (a, b, c) is a Pythagorean triplet.
    /// </summary>
    private bool IsTriplet(int a, int b, int c) {
        return (long)a * a + (long)b * b == (long)c * c;
    }
}
