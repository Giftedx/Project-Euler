using System.Numerics;

namespace Project_Euler;

public class Problem015 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 15: Lattice paths.
    /// Calculates the number of routes through a 20x20 grid from the top-left corner to the bottom-right corner, 
    /// only being able to move right and down.
    /// </summary>
    /// <returns>The total number of such routes as a BigInteger.</returns>
    public override object Solve() {
        return LatticePaths(20, 20);
    }

    private BigInteger LatticePaths(int i, int j) {
        // The number of paths in a grid of size i x j is given by the binomial coefficient (i+j choose i) or (i+j choose j).
        return Library.Combinations(i + j, i);
    }
}