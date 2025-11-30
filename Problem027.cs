namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 27: Quadratic primes.
/// Finds the product of coefficients a and b for the quadratic expression n^2 + an + b that produces the maximum number of primes.
/// </summary>
public class Problem027 : Problem {
    private const int Limit = 1000;
    private const int SieveLimit = 100000;
    private readonly bool[] _isPrime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem027"/> class.
    /// </summary>
    public Problem027() {
        _isPrime = Library.SieveOfEratosthenesBoolArray(SieveLimit);
    }

    /// <summary>
    /// Solves Project Euler Problem 27: Quadratic primes.
    /// </summary>
    /// <returns>The product of coefficients a and b.</returns>
    public override object Solve() {
        return CoefficientProduct(Limit);
    }

    /// <summary>
    /// Finds product a*b.
    /// </summary>
    private int CoefficientProduct(int coeffLimit) {
        int bestA = 0, bestB = 0, maxLength = 0;

        for (int b = 3; b < coeffLimit; b += 2) {
            if (b >= _isPrime.Length || !_isPrime[b]) {
                continue;
            }

            for (int a = -coeffLimit + 1; a < coeffLimit; a += 2) {
                int n = 0;
                while (true) {
                    int val = n * n + a * n + b;
                    if (val < 0 || val >= SieveLimit || !_isPrime[val]) {
                        break;
                    }
                    n++;
                }

                if (n > maxLength) {
                    maxLength = n;
                    bestA = a;
                    bestB = b;
                }
            }
        }
        return bestA * bestB;
    }
}
