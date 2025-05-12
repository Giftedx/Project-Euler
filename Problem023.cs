namespace Project_Euler;

public class Problem023 : Problem {
    private const int Limit = 28123;
    private readonly bool[] _isAbundant;
    public Problem023() {
        FillAbundantArray(Limit, out _isAbundant);
    }

    public override void Solve() {
        Print(SumOfNonAbundantBelow(Limit));
    }

    private int SumOfNonAbundantBelow(int n) {
        int sum = 0;
        for (int i = 0; i <= n; i++)
            if (!IsSumOfAbundant(i))
                sum += i;
        return sum;
    }

    private bool IsSumOfAbundant(int n) {
        for (int i = 0; i <= n; i++)
            if (_isAbundant[i] && _isAbundant[n - i])
                return true;
        return false;
    }

    private void FillAbundantArray(int n, out bool[] isAbundant) {
        isAbundant = new bool[n + 1];
        int bound = isAbundant.Length;
        for (int i = 1; i < bound; i++) isAbundant[i] = IsAbundant(i);
    }

    private bool IsAbundant(int n) {
        int divSum = 1;
        int end = (int)Math.Sqrt(n);
        for (int i = 2; i <= end; i++)
            if (n % i == 0)
                divSum += i + n / i;
        if (end * end == n) divSum -= end;
        return divSum > n;
    }
}