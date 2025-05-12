namespace Project_Euler;

public class Problem039 : Problem {
    public override void Solve() {
        Print(MaxTrianglePerimeters());
    }

    private int MaxTrianglePerimeters() {
        var countSortedPs = new List<int>(new int[1001]);
        for (int a = 2; a < 500; a++)
        for (int b = 2; b < 500; b++) {
            double p = Math.Sqrt(b * b + a * a) + a + b;
            if (p % 1 == 0 && p <= 1000) countSortedPs[(int)p]++;
        }

        int max = 0;
        int maxI = 0;
        for (int j = 0; j <= 1000; j++) {
            if (countSortedPs[j] <= max) continue;
            max = countSortedPs[j];
            maxI = j;
        }

        return maxI;
    }
}