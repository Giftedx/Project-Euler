namespace Project_Euler;
public class Problem009 : Problem {
    public override void Solve() {
        Print(PythagoreanTripletProduct(1000));
    }

    private int PythagoreanTripletProduct(int n) {
        for (int a = 1; a <= n; a++) {
            for (int b = a + 1; b <= n - a; b++) {
                int c = n - a - b;
                if (IsTriplet(a, b, c)) return a * b * c;
            }
        }
        return 0;
    }
        
    private bool IsTriplet(int a, int b, int c){return a*a + b*b == c*c;}
}