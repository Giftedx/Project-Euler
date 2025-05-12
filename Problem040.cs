namespace Project_Euler;

public class Problem040 : Problem {
    public override void Solve() {
        Print(DigitProduct());
    }

    //Fastest solution, figure it out.

    private long DigitProduct() {
        int p = 1;
        for (int x = 1; x <= 1000000; x *= 10)
            p *= Ff(x);
        return p;
    }

    private int Ff(int pos) {
        int digits = 0;
        for (int x = 1; x < 1000000; x++) {
            digits += (int)Math.Log10(x) + 1;
            if (digits < pos) continue;
            string current = x.ToString();
            return current[current.Length - digits + pos - 1] - '0';
        }

        return -1;
    }
}