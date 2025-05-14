namespace Project_Euler;

public class Problem027 : Problem {
    private readonly bool[] _isPrime;
    private const int Limit = 1000;
    public Problem027() {
        Library.SieveOfEratosthenes(Limit * Limit, out _isPrime);
    }
    
    public override void Solve() {
        Print(CoefficientProduct(Limit));
    }

    private int CoefficientProduct(int limit) {
        int bestA = 0, bestB = 0, maxLength = 0;
        for (int b = 2; b < limit; b++) {
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