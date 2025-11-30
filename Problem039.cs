namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 39: Integer right triangles.
/// Finds the value of p &lt;= 1000 for which the number of solutions is maximised.
/// </summary>
public class Problem039 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 39: Integer right triangles.
    /// </summary>
    /// <returns>The perimeter p &lt;= 1000 with max solutions.</returns>
    public override object Solve() {
        return MaxRightTrianglesPerimeter();
    }

    /// <summary>
    /// Finds p with max integer right triangles.
    /// </summary>
    private int MaxRightTrianglesPerimeter() {
        int maxSolutions = 0;
        int bestP = 0;

        for (int p = 1; p <= 1000; p++) {
            int solutions = 0;
            for (int a = 1; a < p / 3; a++) {
                if ((p * (p - 2 * a)) % (2 * (p - a)) == 0) {
                    solutions++;
                }
            }
            if (solutions > maxSolutions) {
                maxSolutions = solutions;
                bestP = p;
            }
        }
        return bestP;
    }
}
