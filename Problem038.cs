using System.Text;

namespace Project_Euler;

public class Problem038 : Problem {
    public override void Solve() {
        Print(PandigitalMultiples());
    }

    private int PandigitalMultiples() {
        int max = 1;
        for (int n = 2; n <= 9; n++) {
            int exp = 9/n;
            for (int i = 1; i < Math.Pow(10, exp); i++) {
                StringBuilder concat = new StringBuilder();
                for (int j = 1; j <= n; j++) concat.Append(i * j);
                if (Library.IsPandigital(concat.ToString()))
                    max = Math.Max(Convert.ToInt32(concat.ToString()), max);
            }
        }
        return max;
    }
}