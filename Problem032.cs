namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 32: Pandigital products.
/// Finds the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.
/// </summary>
public class Problem032 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 32: Pandigital products.
    /// </summary>
    /// <returns>The sum of all pandigital products.</returns>
    public override object Solve() {
        return SumPandigitalProducts();
    }

    /// <summary>
    /// Finds sum of unique products.
    /// </summary>
    private int SumPandigitalProducts() {
        HashSet<int> products = new HashSet<int>();

        for (int a = 1; a < 10; a++) {
            for (int b = 1234; b < 9876; b++) {
                int p = a * b;
                if (p > 9876) break;
                if (IsPandigitalProduct(a, b, p)) products.Add(p);
            }
        }

        for (int a = 12; a < 99; a++) {
            for (int b = 123; b < 987; b++) {
                int p = a * b;
                if (p > 9876) break;
                if (IsPandigitalProduct(a, b, p)) products.Add(p);
            }
        }

        return products.Sum();
    }

    /// <summary>
    /// Checks if concatenation of a, b, p is 1-9 pandigital.
    /// </summary>
    private bool IsPandigitalProduct(int a, int b, int p) {
        string s = $"{a}{b}{p}";
        return Library.IsPandigital(s);
    }
}
