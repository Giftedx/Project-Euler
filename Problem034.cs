namespace Project_Euler;

public class Problem034 : Problem {
    private readonly int[] _factorials = new int[10];

    public override void Solve() {
        Print(SumCurious());
    }

    private int SumCurious() {
        FillFactorials();
        int sum = 0;
        for (int i = 3; i < 7 * _factorials[9]; i += 2)
            if (i == FactorialDigitsSum(i))
                sum += i;
        return sum;
    }

    private int FactorialDigitsSum(int n) {
        int sum = 0;
        while (n != 0) {
            sum += _factorials[n % 10];
            n /= 10;
        }

        return sum;
    }

    private void FillFactorials() {
        for (int i = 0; i < _factorials.Length; i++)
            _factorials[i] = Library.IntFactorial(i);
    }
}