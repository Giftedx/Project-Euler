// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Project_Euler;

public class Problem044 : Problem {
    public override void Solve() {
        Print(FindMinimumD());
    }

    private long FindMinimumD() {
        List<long> pentagonNumbers = [];
    
        int n = 1;
        while (true) {
            long pn = Pentagon(n);
            pentagonNumbers.Add(pn);

            for (int i = 0; i < pentagonNumbers.Count - 1; i++) {
                long pnm = pentagonNumbers[i];
                long diff = pn - pnm;
                long sum = pn + pnm;
                
                if (IsPentagonal(diff) && IsPentagonal(sum)) {
                    return diff;
                }
            }
            
            if (pn > 10_000_000) break;
            n++;
        }

        return -1;
    }

    private static long Pentagon(int n) {
        return n * (3L * n - 1) >>1;
    }

    private static bool IsPentagonal(long x) {
        double n = (1 + Math.Sqrt(1 + 24 * x)) / 6;
        return n == (long)n;
    }
}