namespace Project_Euler;

public class Problem035 : Problem {
    public override void Solve() {
        Print(CircularPrimeCount());
    }
    
    private readonly bool[] _isPrime;
    public Problem035() {
        Library.SieveOfEratosthenes(1000000, out _isPrime);
    }

    private int CircularPrimeCount(){
        int count = 0;
        for (int i = 2; i < _isPrime.Length; i++) {
            if (_isPrime[i] && IsCircularPrime(i)) count++;
        }

        return count;
    }

    private bool IsCircularPrime(int n){
        int count = 0, temp = n;
        while (temp > 0) {
            count++;
            temp /= 10;
        }
 
        int num = n;
        while (_isPrime[num]) {
            int rem = num % 10;
            int div = num / 10;
            int tenthPower = (int)Math.Pow(10, count - 1);
            num = tenthPower * rem + div;

            if (num == n) return true;
        }
        return false;
    }
}