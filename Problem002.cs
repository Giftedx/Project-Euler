namespace Project_Euler;
public class Problem002 : Problem{
    public override void Solve() {
        Print(EvenFibSum());
    }

    private int EvenFibSum() {
        int sum = 0, limit = 4000000;
        int result = 0, fib1 = 0, fib2 = 1;
        while (result < limit) {
            result =  fib1 + fib2;
            fib1 = fib2;
            fib2 = result;
            if (result % 2 == 0) sum += result;
        }
        return sum;
    }
}