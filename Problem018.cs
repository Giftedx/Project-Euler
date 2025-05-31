namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 18: Maximum path sum I.
/// Further details can be found at https://projecteuler.net/problem=18
/// </summary>
public class Problem018 : Problem {
    /// <summary>
    /// Represents the triangle of numbers for the problem.
    /// Each element of the list is an array of integers representing a row in the triangle.
    /// </summary>
    private readonly List<int[]> _triangle = [
        new[] { 75 },
        new[] { 95, 64 },
        new[] { 17, 47, 82 },
        new[] { 18, 35, 87, 10 },
        new[] { 20, 04, 82, 47, 65 },
        new[] { 19, 01, 23, 75, 03, 34 },
        new[] { 88, 02, 77, 73, 07, 63, 67 },
        new[] { 99, 65, 04, 28, 06, 16, 70, 92 },
        new[] { 41, 41, 26, 56, 83, 40, 80, 70, 33 },
        new[] { 41, 48, 72, 33, 47, 32, 37, 16, 94, 29 },
        new[] { 53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14 },
        new[] { 70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57 },
        new[] { 91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48 },
        new[] { 63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31 },
        new[] { 04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23 }
    ];

    /// <summary>
    /// Solves Project Euler Problem 18: Maximum path sum I.
    /// Finds the maximum total from top to bottom of the triangle provided in the problem.
    /// </summary>
    /// <returns>The maximum path sum from the top to the bottom of the triangle.</returns>
    public override object Solve() {
        // Note: This problem is a smaller version of Problem 67.
        // The MaxPathSum method modifies the _triangle in place.
        // If _triangle were to be used elsewhere or needed to remain unchanged, a copy should be made first.
        return MaxPathSum();
    }

    /// <summary>
    /// Calculates the maximum path sum from the top to the bottom of the triangle stored in <see cref="_triangle"/>.
    /// The method uses a dynamic programming approach, working upwards from the second-to-last row.
    /// For each element at `triangle[i][j]`, it updates its value by adding the maximum of its two
    /// "children" in the row below (i.e., `triangle[i+1][j]` and `triangle[i+1][j+1]`).
    /// After processing all relevant rows, the element at the top of the triangle (`triangle[0][0]`)
    /// will contain the maximum total sum from top to bottom.
    /// </summary>
    /// <returns>The maximum path sum.</returns>
    private int MaxPathSum() {
        // Iterate from the second-to-last row (index _triangle.Count - 2) up to the top row (index 0).
        for (int i = _triangle.Count - 2; i >= 0; i--) {
            for (int j = 0; j < _triangle[i].Length; j++) {
                // Update the current element with the sum of itself and the larger of its two children below it.
                _triangle[i][j] += Math.Max(_triangle[i + 1][j], _triangle[i + 1][j + 1]);
            }
        }
        // The top element of the triangle now holds the maximum path sum.
        return _triangle[0][0];
    }
}