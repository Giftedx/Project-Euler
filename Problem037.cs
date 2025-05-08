namespace Project_Euler;

public class Problem037 : Problem {
    public override void Solve() {
        Print(SumTruncatablePrimes());
    }

    private long SumTruncatablePrimes() {
        long sum = 0;
        for (int count = 0, n = 10; count < 11; n++) {
            if (!IsTruncPrime(n)) continue;
            sum += n;
            count++;
        }
        return sum;
    }

    private bool IsTruncPrime(int n){
        for(long i = 10; i <= n; i*=10)
            if(!Library.IsPrime(n % (int) i))return false;
        while(n != 0){
            if(!Library.IsPrime(n))return false;
            n /= 10;
        }
        return true;
    }
}