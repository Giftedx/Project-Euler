using System.Numerics;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 15: Lattice paths.
/// Further details can be found at https://projecteuler.net/problem=15
/// </summary>
public class Problem015 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 15: Lattice paths.
    /// Calculates the number of routes through a 20x20 grid starting from the top left corner
    /// and only being able to move right and down, to reach the bottom right corner.
    /// </summary>
    /// <returns>The total number of such routes as a <see cref="BigInteger"/>.</returns>
    public override object Solve() {
        return LatticePaths(20, 20);
    }

    /// <summary>
    /// Calculates the number of lattice paths in a grid of size m x n.
    /// This is equivalent to finding the number of ways to choose m 'down' moves (or n 'right' moves)
    /// out of a total of m+n moves. The formula used is the binomial coefficient C(m+n, m) or C(m+n, n),
    /// which is (m+n)! / (m! * n!).
    /// </summary>
    /// <param name="m">The number of rows in the grid (number of 'down' moves required).</param>
    /// <param name="n">The number of columns in the grid (number of 'right' moves required).</param>
    /// <returns>The total number of unique paths from the top-left to the bottom-right corner
    /// of an m x n grid, as a <see cref="BigInteger"/>.</returns>
    /// <remarks>
    /// For a grid of size m x n, we need to make m moves down and n moves right.
    /// The total number of moves is m + n. The number of paths is the number of ways to arrange these moves.
    /// This can be calculated as (m+n)! / (m! * n!).
    /// </remarks>
    private BigInteger LatticePaths(int m, int n) {
        // Ensure m and n are non-negative, though problem context implies positive.
        if (m < 0 || n < 0) {
            throw new ArgumentOutOfRangeException("Grid dimensions must be non-negative.");
        }
        // Using Library.Factorial which presumably handles BigInteger.
        return Library.Factorial(m + n) / (Library.Factorial(m) * Library.Factorial(n));
    }
}