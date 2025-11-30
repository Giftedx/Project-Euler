using System.Numerics;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 15: Lattice paths.
/// Finds the number of routes through a 20x20 grid starting in the top left and ending in the bottom right, moving only right and down.
/// </summary>
public class Problem015 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 15: Lattice paths.
    /// </summary>
    /// <returns>The number of routes through a 20x20 grid.</returns>
    public override object Solve() {
        return LatticePaths(20);
    }

    /// <summary>
    /// Calculates the number of lattice paths in an n x n grid.
    /// The number of paths is given by the central binomial coefficient: (2n choose n).
    /// </summary>
    /// <param name="n">The grid size (n x n).</param>
    /// <returns>The number of paths.</returns>
    private BigInteger LatticePaths(int n) {
        return Library.Combinations(2 * n, n);
    }
}
