namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 21: Amicable numbers.
/// Finds the sum of all the amicable numbers under 10000.
/// </summary>
public class Problem021 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 21: Amicable numbers.
    /// </summary>
    /// <returns>The sum of all amicable numbers under 10000.</returns>
    public override object Solve() {
        return SumAmicableNumbers(10000);
    }

    /// <summary>
    /// Calculates the sum of all amicable numbers under the given limit.
    /// Uses a cache for divisor sums.
    /// </summary>
    private int SumAmicableNumbers(int limit) {
        int[] d = new int[limit];
        // Calculate d(n) for all n < limit
        for (int i = 1; i < limit; i++) {
            d[i] = SumProperDivisors(i);
        }

        int sum = 0;
        for (int a = 2; a < limit; a++) {
            int b = d[a];
            if (b > a && b < limit && d[b] == a) {
                sum += a + b;
            }
        }
        return sum;
    }

    /// <summary>
    /// Calculates sum of proper divisors of n.
    /// </summary>
    private int SumProperDivisors(int n) {
        int sum = 1;
        int sqrt = (int)Math.Sqrt(n);
        for (int i = 2; i <= sqrt; i++) {
            if (n % i == 0) {
                sum += i;
                if (i * i != n) sum += n / i;
            }
        }
        return sum;
    }
}
