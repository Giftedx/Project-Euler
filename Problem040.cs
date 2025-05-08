using System.Text;

namespace Project_Euler;

public class Problem040 : Problem{
    public override void Solve() {
        Print(DigitProduct(6));
    }
    
    //Fastest solution, figure it out.

    private long DigitProduct(int n) {
        int p = 1;
        for(int x = 1; x <= 1000000; x*=10)
            p *= ff(x, n);
        return p;
    }
    
    private int ff(int pos, int n) {
        int digits = 0;
        for (int x = 1; x < 1000000; x++) {
            digits += ((int)Math.Log10(x)+1);
            if (digits < pos) continue;
            string current = x.ToString();
            return (int)current[current.Length - digits + pos - 1]-'0';
        }
        return -1;
    }
}