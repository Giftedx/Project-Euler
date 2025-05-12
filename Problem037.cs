namespace Project_Euler;

public class Problem037 : Problem {
    public override void Solve() {
        Print(SumTruncatablePrimes());
    }

    private bool[] _isPrime;
    public Problem037() {
        Library.SieveOfEratosthenes(700000, out _isPrime);
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
        for(long i = 10; i <= n; i*=10) {
            if(n < _isPrime.Length) {
                if(!_isPrime[n % (int) i])
                    return false;
            }else {
                if(!Library.IsPrime(n % (int) i))return false;
            }
        }

        while(n != 0){
            if(n < _isPrime.Length) {
                if(!_isPrime[n])
                    return false;
            }else {
                if(!Library.IsPrime(n))return false;
            }
            n /= 10;
        }
        return true;
    }
}