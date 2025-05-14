namespace Project_Euler;

public class Problem002 : Problem {
    public override void Solve() {
        Print(EvenFibSum());
    }

    private int EvenFibSum() {
        const int limit = 4000000;
        int even1 = 2;
        int even2 = 8;
        int sum = even1 + even2;
        while (true) {
            int nextEven = 4 * even2 + even1;
            if (nextEven >= limit) break;
            sum += nextEven;
            (even1 , even2) = (even2 , nextEven);
        }
        return sum;
    }
}