using System.Numerics;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 48: Self powers.
/// Finds the last ten digits of the series, 1^1 + 2^2 + 3^3 + ... + 1000^1000.
/// Further details can be found at https://projecteuler.net/problem=48
/// </summary>
public class Problem048 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 48.
    /// It computes the sum of n^n for n from 1 to 1000, and returns the last ten digits of this sum.
    /// </summary>
    /// <returns>A <see cref="BigInteger"/> representing the last ten digits of the sum.</returns>
    public override object Solve() {
        return CalculateSelfPowersSumLastTenDigits(); // Renamed for clarity
    }

    /// <summary>
    /// Calculates the last ten digits of the sum of the series n^n for n = 1 to 1000.
    /// To handle the large numbers involved, all calculations are performed modulo 10^10.
    ///
    /// Algorithm:
    /// 1. Initialize `totalSum = 0`.
    /// 2. Define `modulus = 10^10` (which is 10,000,000,000). This is used to get the last ten digits.
    /// 3. For each integer `i` from 1 to 1000:
    ///    a. Calculate `term = i^i mod modulus` using `BigInteger.ModPow(i, i, modulus)`.
    ///       This computes `i` raised to the power of `i` efficiently, keeping only the last ten digits at each step
    ///       of the exponentiation to prevent overflow and manage number size.
    ///    b. Add this `term` to `totalSum`. Since `term` is already `mod modulus`, `totalSum` might exceed `modulus`
    ///       but intermediate terms are kept manageable.
    /// 4. After the loop, the final `totalSum` is taken modulo `modulus` (`totalSum % modulus`) to ensure
    ///    that the returned result represents only the last ten digits of the actual grand sum.
    /// </summary>
    /// <returns>A <see cref="BigInteger"/> representing the last ten digits of the sum 1^1 + 2^2 + ... + 1000^1000.</returns>
    private BigInteger CalculateSelfPowersSumLastTenDigits() { // Renamed for clarity
        BigInteger totalSum = 0;
        // Modulus to get the last ten digits: 10^10.
        var modulus = BigInteger.Pow(10, 10);

        for (int i = 1; i <= 1000; i++) {
            // Calculate i^i mod (10^10)
            BigInteger term = BigInteger.ModPow(i, i, modulus);
            totalSum += term;
            // It's good practice to keep the running sum also under the modulus if it can grow very large,
            // though with 1000 terms, each max 10^10-1, sum is max 1000 * 10^10 = 10^13.
            // BigInteger handles this, but taking modulo at each step or at the end is fine.
            // Current code takes modulo at the end.
        }
        // Ensure the final sum is also modulo 10^10.
        return totalSum % modulus;
    }
}