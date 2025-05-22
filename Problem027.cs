namespace Project_Euler;

public class Problem027 : Problem {
    private const int Limit = 1000;
    // Max possible value for n*n + a*n + b is roughly Limit*Limit + Limit*Limit + Limit = 1000*1000 + 1000*1000 + 1000 = 2,001,000
    // However, primes are checked up to b (max 999 for a,b < 1000) and val (n*n + an + b).
    // The problem constraints imply b must be prime, so b < 1000.
    // Max n is around 70-80 for known problem solutions.
    // (80*80) + 1000*80 + 1000 = 6400 + 80000 + 1000 = 87400.
    // Let's use a sieve limit that comfortably covers values of n*n + a*n + b and b itself.
    // A limit of 100,000 should be safe for val. Sieve for b up to 1000.
    private const int SieveLimit = 100000; // Increased for safety for `val`
    private readonly bool[] _isPrime;

    public Problem027() {
        _isPrime = Library.SieveOfEratosthenesBoolArray(SieveLimit);
    }

    public override object Solve() {
        return CoefficientProduct(Limit);
    }

    private int CoefficientProduct(int limit) {
        int bestA = 0, bestB = 0, maxLength = 0;
        for (int b = 3; b < limit; b += 2) {
            if (!_isPrime[b]) continue;
            for (int a = -limit + 1; a < limit; a += 2) {
                int n = 0;
                while (true) {
                    int val = n * n + a * n + b;
                    if (val < 0 || val >= _isPrime.Length || !_isPrime[val]) break;
                    n++;
                }

                if (n <= maxLength) continue;
                maxLength = n;
                bestA = a;
                bestB = b;
            }
        }

        return bestA * bestB;
    }
}