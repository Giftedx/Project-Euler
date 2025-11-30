using System.Numerics;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 48: Self powers.
/// Finds the last ten digits of the series, 1^1 + 2^2 + 3^3 + ... + 1000^1000.
/// </summary>
public class Problem048 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 48: Self powers.
    /// </summary>
    /// <returns>The last ten digits of the series.</returns>
    public override object Solve() {
        return LastTenDigitsOfSeries(1000);
    }

    /// <summary>
    /// Calculates last 10 digits of series.
    /// </summary>
    private long LastTenDigitsOfSeries(int limit) {
        long modulus = 10000000000;
        long sum = 0;

        for (int i = 1; i <= limit; i++) {
            sum = (sum + ModPow(i, i, modulus)) % modulus;
        }
        return sum;
    }

    /// <summary>
    /// Modular exponentiation for long.
    /// </summary>
    private long ModPow(long b, int e, long m) {
        long result = 1;
        b %= m;
        while (e > 0) {
            if (e % 2 == 1) result = (result * b) % m;
            e >>= 1;
            b = (b * b) % m;
        }
        return result;
    }
}
