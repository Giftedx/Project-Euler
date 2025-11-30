namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 45: Triangular, pentagonal, and hexagonal.
/// Finds the next triangle number that is also pentagonal and hexagonal.
/// </summary>
public class Problem045 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 45: Triangular, pentagonal, and hexagonal.
    /// </summary>
    /// <returns>The next triangle number after 40755.</returns>
    public override object Solve() {
        return NextTriPentHex();
    }

    /// <summary>
    /// Finds next number.
    /// </summary>
    private long NextTriPentHex() {
        long n = 144;
        while (true) {
            long h = n * (2 * n - 1);
            if (Library.IsPentagonal(h)) {
                return h;
            }
            n++;
        }
    }
}
