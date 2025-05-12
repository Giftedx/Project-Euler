namespace Project_Euler;

public class Problem044 : Problem {
    public override void Solve() {
        Print(FindMinimumD());
    }

    private long FindMinimumD() {
        for (int i = 1;; i++)
        for (int m = i, n = 1; m >= 1; m--, n++) {
            int pn = Pentagon(n);
            int pnm = Pentagon(n + m);
            if (Library.IsPentagon(pnm - pn) &&
                Library.IsPentagon(pn + pnm))
                return pnm - pn;
        }
    }

    private static int Pentagon(int n) {
        return (n * (3 * n - 1)) >> 1;
    }
}