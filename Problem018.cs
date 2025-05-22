namespace Project_Euler;

public class Problem018 : Problem {
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
    /// Finds the maximum total from top to bottom of the provided triangle of numbers.
    /// </summary>
    /// <returns>The maximum path sum.</returns>
    public override object Solve() {
        return MaxPathSum();
    }

    /// <summary>
    /// Calculates the maximum path sum from the top to the bottom of the triangle
    /// stored in the _triangle field, using a bottom-up dynamic programming approach.
    /// The _triangle field is modified in place.
    /// </summary>
    /// <returns>The maximum path sum.</returns>
    private int MaxPathSum() {
        // Using dynamic programming, work from the second-to-last row upwards.
        // Each element (i,j) is updated to be itself plus the maximum of 
        // the two adjacent elements in the row below: (i+1,j) or (i+1,j+1).
        // After processing all rows, the top element _triangle[0][0] will contain the maximum path sum.
        for (int i = _triangle.Count - 2; i >= 0; i--)
        for (int j = 0; j < _triangle[i].Length; j++)
            _triangle[i][j] += Math.Max(_triangle[i + 1][j], _triangle[i + 1][j + 1]);

        return _triangle[0][0];
    }
}