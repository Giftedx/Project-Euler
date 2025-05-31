namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 9: Special Pythagorean triplet.
/// Further details can be found at https://projecteuler.net/problem=9
/// </summary>
public class Problem009 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 9: Special Pythagorean triplet.
    /// Finds the product abc for the Pythagorean triplet for which a + b + c = 1000.
    /// </summary>
    /// <returns>The product abc of the unique Pythagorean triplet (a < b < c) whose sum is 1000.</returns>
    public override object Solve() {
        return PythagoreanTripletProduct(1000);
    }

    /// <summary>
    /// Finds the product (a*b*c) of a Pythagorean triplet (a, b, c) such that a + b + c = n.
    /// The method iterates through possible values for 'a' and 'b' under the constraints
    /// a < b < c and a + b + c = n. These constraints imply a < n/3 and b < n/2.
    /// For each pair (a, b), 'c' is calculated as n - a - b.
    /// It then checks if (a,b,c) forms a Pythagorean triplet (a^2 + b^2 = c^2).
    /// </summary>
    /// <param name="n">The sum for which the Pythagorean triplet (a+b+c=n) should be found.</param>
    /// <returns>The product a*b*c if such a triplet is found; otherwise, returns 0.</returns>
    private int PythagoreanTripletProduct(int n) {
        // a < b < c
        // a + b + c = n
        // a + a+1 + a+2 <= n  => 3a+3 <= n => a <= (n-3)/3
        // More simply, a < n/3
        // Also, a+b > c. Since a+b+c=n, then a+b > n-(a+b) => 2(a+b) > n => a+b > n/2
        for (int a = 1; a < n / 3; a++) {
            // b > a
            // b < c => b < n-a-b => 2b < n-a => b < (n-a)/2
            // Also, from problem statement, we are looking for a unique triplet.
            // The loop for b goes up to n/2 as b < c means b cannot be more than half of remaining sum (n-a)
            for (int b = a + 1; b < (n - a) / 2; b++) { // Corrected upper bound for b for tighter loop
                int c = n - a - b;
                // Now, we must ensure a < b < c.
                // a < b is ensured by loop start b = a + 1.
                // b < c means b < n - a - b  => 2b < n - a => b < (n-a)/2. This is used in loop condition.
                // If a,b,c are positive and sum to n, and a<b and b<c, then c will be the largest.
                if (c > b && IsTriplet(a, b, c)) { // Explicitly check c > b, though loop for b should ensure it.
                    return a * b * c;
                }
            }
        }
        return 0; // Should not be reached if n=1000 as a triplet exists.
    }

    /// <summary>
    /// Checks if three integers a, b, and c form a Pythagorean triplet.
    /// A triplet (a,b,c) is Pythagorean if a^2 + b^2 = c^2, assuming a, b, c are positive.
    /// By convention, 'c' is usually the hypotenuse (largest side).
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    /// <param name="c">The third integer (potential hypotenuse).</param>
    /// <returns>True if a^2 + b^2 = c^2, false otherwise.</returns>
    private bool IsTriplet(int a, int b, int c) {
        // Ensure a, b, c are positive, although loops in PythagoreanTripletProduct should handle this.
        if (a <= 0 || b <= 0 || c <= 0) return false;
        return (long)a * a + (long)b * b == (long)c * c; // Use long for intermediate products to prevent overflow
    }
}