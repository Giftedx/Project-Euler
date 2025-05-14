// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Project_Euler;

public class Problem044 : Problem {
    public override void Solve() {
        Print(FindMinimumD());
    }

    private int FindMinimumD() {
        int diffIndex = 1;
        while (true) {
            int doubleDiff = diffIndex * (3 * diffIndex - 1);
            int divisor = 1;

            while (divisor * divisor < doubleDiff) {
                if (doubleDiff % divisor == 0) {
                    int numerator = (doubleDiff / divisor - 3 * divisor + 1);
                    if (numerator % 6 == 0) {
                        int j = numerator / 6;
                        int k = j + divisor;

                        int pk = k * (3 * k - 1) >> 1;
                        int pj = j * (3 * j - 1) >> 1;

                        if (j > 0 && IsPentagonal(pk + pj) &&
                            IsPentagonal(pk - pj)) return pk - pj;
                    }
                }
                divisor++;
            }
            diffIndex++;
        }
    }

    private bool IsPentagonal(int num) {
        double n = (1 + Math.Sqrt(1 + 24 * num)) / 6;
        return Math.Abs(n - Math.Round(n)) < 1e-10 && 2 * num ==
            (int)Math.Round(n) * (3 * (int)Math.Round(n) - 1);
    }
}