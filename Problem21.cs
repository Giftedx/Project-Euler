namespace Project_Euler;
public class Problem21 : Problem{
    public override void Solve() {
        Print(AmicableSumBelow(10000));
    }

    private object AmicableSumBelow(int n) {
        int amicableSum = 0;
        for(int i = 0; i < n; i++)if(IsAmicable(i)) amicableSum += i;
        return amicableSum;
    }

    private bool IsAmicable(int n) {
        int m = DivisorSum(n);
        return m != n && DivisorSum(m) == n;
    }

    private int DivisorSum(int n) {
        int sum = 1;
        for (int i = 2; i < Math.Sqrt(n); i++) {
            if (n % i == 0) sum += i + n / i;
        }
        return sum;
    }
}