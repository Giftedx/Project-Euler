namespace Project_Euler;

public class Problem999 : Problem {
    public override object Solve() {
        int sum = 0;
        for (int i = 0; i < 10; i += 2) {
            sum += i;
        }
        return sum;
    }
}
