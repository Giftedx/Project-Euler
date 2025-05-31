namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 39: Integer right triangles.
/// For which value of p ≤ 1000, is the number of solutions for integer sided right angle triangles maximized?
/// The perimeter p = a + b + c.
/// Further details can be found at https://projecteuler.net/problem=39
/// </summary>
public class Problem039 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 39.
    /// It finds the perimeter p ≤ 1000 for which the number of distinct integer-sided right angle triangles is maximized.
    /// </summary>
    /// <returns>The value of p ≤ 1000 that has the maximum number of right angle triangle solutions.</returns>
    public override object Solve() {
        return FindPerimeterWithMaxRightTriangles(); // Renamed for clarity
    }

    /// <summary>
    /// Finds the perimeter `p` (where `p <= 1000`) for which the number of integer-sided
    /// right angle triangles is maximized.
    /// The method uses Euclid's formula for generating Pythagorean triples:
    /// a = k*(m² - n²), b = k*(2mn), c = k*(m² + n²)
    /// where m > n > 0, m and n are coprime, and m and n have opposite parity (one is even, one is odd).
    /// The perimeter is p = a + b + c = k*(2m(m+n)).
    ///
    /// Algorithm:
    /// 1. Initialize an array `perimeterCounts` where `perimeterCounts[p]` stores the number of triangles for perimeter `p`.
    /// 2. Iterate through possible values of `m` and `n` to generate primitive Pythagorean triples:
    ///    - `m` goes from 2 up to sqrt(limit/2) because p = 2m(m+n) > 2m^2, so 2m^2 < limit => m < sqrt(limit/2).
    ///      (Original code uses `limitSqrt` which is `sqrt(limit)`, this is a slightly looser but safe upper bound for m).
    ///    - `n` goes from 1 up to `m-1`.
    ///    - Skip pairs (m,n) if they are not coprime (`Library.Gcd(m, n) != 1`).
    ///    - Skip pairs (m,n) if they have the same parity (i.e., `(m - n) % 2 == 0`), as this leads to non-primitive triples or even sides that can be reduced.
    /// 3. For each valid (m,n) pair, calculate the sides (a,b,c) of the primitive triple and its perimeter `primitivePerimeter = a + b + c`.
    /// 4. For this `primitivePerimeter`, iterate through its multiples `k * primitivePerimeter` (where k=1,2,3,...)
    ///    as long as the multiple is within the overall `limit`. For each such perimeter, increment its count in `perimeterCounts`.
    /// 5. After checking all (m,n) pairs, iterate through `perimeterCounts` to find the perimeter `p` that has the maximum count.
    /// </summary>
    /// <returns>The perimeter `p` (≤ 1000) that corresponds to the maximum number of integer-sided right angle triangles.</returns>
    private int FindPerimeterWithMaxRightTriangles() { // Renamed for clarity
        const int limit = 1000;
        int[] perimeterCounts = new int[limit + 1]; // Stores counts for perimeters p = 0 to limit.

        // Iterate m up to sqrt(limit). A tighter bound could be sqrt(limit/2) since p = 2m(m+n) > 2m^2.
        // For limit = 1000, sqrt(1000) approx 31. sqrt(500) approx 22. Original code uses limitSqrt.
        int m_limit = (int)Math.Sqrt(limit);

        for (int m = 2; m <= m_limit; m++) { // m starts from 2
            for (int n = 1; n < m; n++) { // n < m
                // For primitive triples: m and n must be coprime and have opposite parity.
                // Opposite parity means (m-n) is odd. So if (m-n) is even, skip.
                if ((m - n) % 2 == 0) {
                    continue;
                }
                if (Library.Gcd(m, n) != 1) {
                    continue;
                }

                // Euclid's formula for primitive triples (a, b, c are sides)
                int a = m * m - n * n;
                int b = 2 * m * n;
                int c = m * m + n * n;
                int primitivePerimeter = a + b + c;

                // If primitivePerimeter itself exceeds limit, no multiple will be valid.
                if (primitivePerimeter > limit) {
                    // Optimization: if m is fixed, as n decreases, m-n increases, m+n decreases.
                    // p = 2m(m+n). If n decreases, p increases. So if p > limit for this n, it will be for smaller n too.
                    // This break is not in original, but p increases as n decreases for fixed m if m+n changes more slowly
                    // Actually, as n decreases, m+n increases, so p = 2m(m+n) increases. This means if current p > limit,
                    // then for smaller n (larger m+n), p will also be > limit. This suggests breaking inner loop.
                    // However, the outer m loop limit already somewhat constrains this.
                    // For now, let's stick to original structure of checking k*p.
                }

                // Add counts for this primitive triple and all its multiples (k*p) within the limit.
                for (int k = 1; k * primitivePerimeter <= limit; k++) {
                    perimeterCounts[k * primitivePerimeter]++;
                }
            }
        }

        int maxPerimeterValue = 0;
        int maxTriangleCount = 0;
        // Find the perimeter with the maximum count.
        for (int p = 1; p <= limit; p++) { // Start p from 1 (or smallest possible perimeter, e.g. 12 for 3,4,5)
            if (perimeterCounts[p] > maxTriangleCount) {
                maxTriangleCount = perimeterCounts[p];
                maxPerimeterValue = p;
            }
        }
        return maxPerimeterValue;
    }
}