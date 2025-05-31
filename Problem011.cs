namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 11: Largest product in a grid.
/// Further details can be found at https://projecteuler.net/problem=11
/// </summary>
public class Problem011 : Problem {
    /// <summary>
    /// The 20x20 grid of numbers for Project Euler Problem 11.
    /// </summary>
    private readonly int[,] _grid = {
        { 8, 2, 22, 97, 38, 15, 0, 40, 0, 75, 4, 5, 7, 78, 52, 12, 50, 77, 91, 8 },
        { 49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 4, 56, 62, 0 },
        { 81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 3, 49, 13, 36, 65 },
        { 52, 70, 95, 23, 4, 60, 11, 42, 69, 24, 68, 56, 1, 32, 56, 71, 37, 2, 36, 91 },
        { 22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80 },
        { 24, 47, 32, 60, 99, 3, 45, 2, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50 },
        { 32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70 },
        { 67, 26, 20, 68, 2, 62, 12, 20, 95, 63, 94, 39, 63, 8, 40, 91, 66, 49, 94, 21 },
        { 24, 55, 58, 5, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72 },
        { 21, 36, 23, 9, 75, 0, 76, 44, 20, 45, 35, 14, 0, 61, 33, 97, 34, 31, 33, 95 },
        { 78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 3, 80, 4, 62, 16, 14, 9, 53, 56, 92 },
        { 16, 39, 5, 42, 96, 35, 31, 47, 55, 58, 88, 24, 0, 17, 54, 24, 36, 29, 85, 57 },
        { 86, 56, 0, 48, 35, 71, 89, 7, 5, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58 },
        { 19, 80, 81, 68, 5, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 4, 89, 55, 40 },
        { 4, 52, 8, 83, 97, 35, 99, 16, 7, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66 },
        { 88, 36, 68, 87, 57, 62, 20, 72, 3, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69 },
        { 4, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 8, 46, 29, 32, 40, 62, 76, 36 },
        { 20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 4, 36, 16 },
        { 20, 73, 35, 29, 78, 31, 90, 1, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 5, 54 },
        { 1, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 1, 89, 19, 67, 48 }
    };

    /// <summary>
    /// Solves Project Euler Problem 11: Largest product in a grid.
    /// Finds the greatest product of four adjacent numbers in the same direction
    /// (up, down, left, right, or diagonally) in the 20x20 grid.
    /// </summary>
    /// <returns>The greatest product of four adjacent numbers in the grid.</returns>
    public override object Solve() {
        return MaximumGridProduct();
    }

    /// <summary>
    /// Calculates the maximum product of four adjacent numbers in any valid direction within the grid.
    /// The method iterates through each cell of the grid. For each cell, it considers it as a potential
    /// starting point for a sequence of four numbers. It checks four directions:
    /// 1. Horizontal (right)
    /// 2. Vertical (down)
    /// 3. Diagonal (down-right)
    /// 4. Diagonal (up-right) - equivalent to down-left if starting from the other end.
    /// It calculates the product for each valid sequence of four numbers and returns the maximum found.
    /// If a zero is encountered in a sequence, that sequence's product becomes zero and is processed accordingly.
    /// </summary>
    /// <returns>The maximum product of four adjacent numbers found in the grid.</returns>
    private int MaximumGridProduct() {
        int maxProduct = 0;
        int rows = _grid.GetLength(0);
        int cols = _grid.GetLength(1);

        // Directions: [dx, dy]
        // dx: change in row, dy: change in col
        // [0, 1]  : Horizontal (right)
        // [1, 0]  : Vertical (down)
        // [1, 1]  : Diagonal (down-right)
        // [-1, 1] : Anti-diagonal (up-right) / (down-left from other end)
        // Other diagonal directions like [1, -1] (down-left) are covered by iterating all start cells
        // and using [ -dx, -dy] implicitly from another starting cell or by symmetry.
        // For example, a down-left sequence from (r,c) is an up-right sequence from (r+3, c-3).
        // The current set of directions is sufficient when checking from every cell as a potential start.
        int[][] directions = new int[][] {
            new int[] {0, 1},  // Right
            new int[] {1, 0},  // Down
            new int[] {1, 1},  // Down-Right
            new int[] {-1, 1}  // Up-Right (or Down-Left when iterating from the other end of the sequence)
        };

        foreach (int[] dir in directions) {
            int dx = dir[0];
            int dy = dir[1];

            for (int r = 0; r < rows; r++) { // Changed variable names for clarity: r for row, c for col
                for (int c = 0; c < cols; c++) {
                    // Check if a sequence of 4 numbers starting at (r,c) in direction (dx,dy) is within bounds
                    if (!IsInBounds(r, c, dx, dy, rows, cols)) {
                        continue;
                    }

                    int currentProduct = 1;
                    bool zeroEncountered = false;
                    for (int i = 0; i < 4; i++) {
                        int val = _grid[r + i * dx, c + i * dy];
                        if (val == 0) {
                            currentProduct = 0; // Product becomes 0 if any element is 0
                            zeroEncountered = true;
                            break; // No need to multiply further for this sequence
                        }
                        currentProduct *= val;
                    }

                    if (currentProduct > maxProduct) {
                        maxProduct = currentProduct;
                    }
                }
            }
        }
        return maxProduct;
    }

    /// <summary>
    /// Checks if a sequence of 4 numbers, starting from a given cell (row, col) and extending in a specific
    /// direction (dx, dy), remains within the grid boundaries.
    /// The check is performed for the end-point of the sequence (i.e., the 4th number).
    /// The start point (row,col) is assumed to be valid by the calling loops.
    /// </summary>
    /// <param name="row">The starting row index of the sequence.</param>
    /// <param name="col">The starting column index of the sequence.</param>
    /// <param name="dx">The change in row index for each step in the sequence (direction).</param>
    /// <param name="dy">The change in column index for each step in the sequence (direction).</param>
    /// <param name="rows">The total number of rows in the grid.</param>
    /// <param name="cols">The total number of columns in the grid.</param>
    /// <returns>True if the entire sequence of 4 numbers is within grid boundaries; false otherwise.</returns>
    private bool IsInBounds(int row, int col, int dx, int dy, int rows, int cols) {
        // Calculate the coordinates of the 4th number in the sequence (index 3)
        int endRow = row + 3 * dx;
        int endCol = col + 3 * dy;

        // Check if the starting cell itself is out of bounds for this direction
        // (e.g., for up-right, if row is < 3, it can't start an up-right sequence of 4).
        // This is implicitly handled by checking the endRow and endCol.
        // For dx = -1, row + 0*dx must be valid, row + 1*dx, row + 2*dx, row + 3*dx.
        // If dx = -1, row + 3*dx = row - 3. This must be >= 0. So row must be >= 3.
        // If dy = -1 (not used here but for completeness), col + 3*dy = col - 3. This must be >= 0. So col must be >=3.

        return endRow >= 0 && endRow < rows && endCol >= 0 && endCol < cols;
    }
}