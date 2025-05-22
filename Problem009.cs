using System; // Required for Math.Sqrt

namespace Project_Euler;

public class Problem009 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 9: Special Pythagorean triplet.
    /// Finds the product abc for the Pythagorean triplet for which a + b + c = 1000.
    /// </summary>
    /// <returns>The product abc of the unique Pythagorean triplet summing to 1000.</returns>
    public override object Solve() {
        return PythagoreanTripletProduct(1000);
    }

    private long PythagoreanTripletProduct(int targetSum) {
        if (targetSum % 2 != 0) {
            // Sum of a,b,c from Euclid's formula 2dm(m+k) is always even.
            // If targetSum is odd, no such triplet exists from this formula.
            return 0; 
        }
        int sumHalf = targetSum / 2;
        // m < sqrt(sumHalf) since d >= 1 and m*(m+k) > m*m.
        // Max m to check: (int)Math.Sqrt(sumHalf -1) or similar if sumHalf is m*m
        int mLimit = (int)Math.Sqrt(sumHalf); 

        for (int m = 2; m <= mLimit; m++) {
            for (int k = 1; k < m; k++) {
                // Conditions for m, k to form a primitive triplet base:
                // 1. m and k are coprime
                // 2. One of m, k is even (i.e., m-k is odd)
                if (((m - k) % 2 == 1) && Library.Gcd(m, k) == 1) {
                    int sum_mk = m * (m + k);
                    if (sumHalf % sum_mk == 0) {
                        int d = sumHalf / sum_mk;
                        long a = d * (long)(m * m - k * k);
                        long b = d * (long)(2 * m * k);
                        long c = d * (long)(m * m + k * k);
                        // Problem implies a < b < c, but Euclid's doesn't guarantee which of a or b is smaller.
                        // However, for product abc, order doesn't matter.
                        // And for a+b+c=1000, there's only one such triplet.
                        // We can assume the problem statement's specific triplet will be found.
                        return a * b * c;
                    }
                }
            }
        }
        return 0; // Should not be reached given problem statement
    }
}