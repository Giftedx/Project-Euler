namespace Project_Euler;

public class Problem027 : Problem {
    public override void Solve() {
        Print(CoefficientProduct(1000));
    }

    private object CoefficientProduct(int n) {
        int numPrimes = 0;
        int bestA = 0, bestB = 0;

        for (int a = -n + 1; a <= n; a += 2)
        for (int b = -n; b <= n; b++) {
            int length = 0;
            while (Library.IsPrime(length * length + a * length + b)) length++;
            if (numPrimes >= length) continue;
            numPrimes = length;
            bestA = a;
            bestB = b;
        }

        return bestA * bestB;
    }
}