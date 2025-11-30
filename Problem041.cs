namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 41: Pandigital prime.
/// Finds the largest n-digit pandigital prime that exists.
/// </summary>
public class Problem041 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 41: Pandigital prime.
    /// </summary>
    /// <returns>The largest n-digit pandigital prime.</returns>
    public override object Solve() {
        return LargestPandigitalPrime();
    }

    /// <summary>
    /// Finds largest pandigital prime.
    /// </summary>
    private int LargestPandigitalPrime() {
        int maxPrime = 0;
        int[] ascDigits = { 1, 2, 3, 4, 5, 6, 7 };

        do {
            int num = DigitsToInt(ascDigits);
            if (Library.IsPrime(num)) {
                if (num > maxPrime) maxPrime = num;
            }
        } while (Library.Permute(ascDigits));

        if (maxPrime > 0) return maxPrime;

        int[] ascDigits4 = { 1, 2, 3, 4 };
        do {
            int num = DigitsToInt(ascDigits4);
            if (Library.IsPrime(num)) {
                if (num > maxPrime) maxPrime = num;
            }
        } while (Library.Permute(ascDigits4));

        return maxPrime;
    }

    private int DigitsToInt(int[] digits) {
        int n = 0;
        foreach (int d in digits) {
            n = n * 10 + d;
        }
        return n;
    }
}
