namespace Project_Euler;
public class Problem23 : Problem{
    public override void Solve() {
        Print(SumOfNonAbundantBelow(28123));
    }

    private bool[] _isAbundant = null!;

    private int SumOfNonAbundantBelow(int n) {
        FillAbundantArray(n);
        int sum = 0;
        for (int i = 0; i <= n; i++) {
            if (!IsSumOfAbundants(i)) sum += i;
        }
        return sum;
    }

    private bool IsSumOfAbundants(int n){
        for(int i = 0; i <= n; i++)
            if (_isAbundant[i] && _isAbundant[n - i]) return true;
        return false;
    }

    private void FillAbundantArray(int n){
        _isAbundant = new bool[n + 1];
        int bound = _isAbundant.Length;
        for (int i = 1; i < bound; i++) {
            _isAbundant[i] = IsAbundant(i);
        }
    }

    private bool IsAbundant(int n){
        int divSum = 1;
        int end = (int)Math.Sqrt(n);
        for (int i = 2; i <= end; i++) {
            if(n % i == 0) divSum += i + n/i;
        }
        if(end*end == n)divSum -= end;
        return divSum > n;
    }
}