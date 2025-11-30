namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 44: Pentagon numbers.
/// Finds the pair of pentagonal numbers Pj and Pk for which their sum and difference are pentagonal and D = |Pk - Pj| is minimised.
/// </summary>
public class Problem044 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 44: Pentagon numbers.
    /// </summary>
    /// <returns>The minimised difference D.</returns>
    public override object Solve() {
        return MinimalPentagonalDifference();
    }

    /// <summary>
    /// Finds minimal difference.
    /// </summary>
    private int MinimalPentagonalDifference() {
        int i = 1;
        while (true) {
            i++;
            int pi = i * (3 * i - 1) / 2;

            for (int j = i - 1; j > 0; j--) {
                int pj = j * (3 * j - 1) / 2;

                if (Library.IsPentagonal(pi - pj) && Library.IsPentagonal(pi + pj)) {
                    return pi - pj;
                }
            }
        }
    }
}
