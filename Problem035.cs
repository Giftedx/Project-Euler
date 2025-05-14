using System.Collections;

namespace Project_Euler;

public class Problem035 : Problem {
    private readonly bool[] _isPrime;

    public Problem035() {
        Library.SieveOfEratosthenes(1000000, out _isPrime);
    }

    public override void Solve() {
        Print(CircularPrimeCount());
    }

    private int CircularPrimeCount() {
        int count = 0;
        for (int i = 2; i < _isPrime.Length; i++)
            if (_isPrime[i] && IsCircularPrime(i))
                count++;

        return count;
    }

    private bool IsCircularPrime(int n) {
        if (n > 10 && IsEvenOr5(n))
            return false;

        int digits = (int)Math.Log10(n) + 1;
        int powTen = Library.Pow10(digits - 1);
        int rotated = n;

        for (int i = 0; i < digits; i++) {
            if (!_isPrime[rotated]) return false;
            
            int lastDigit = rotated % 10;
            rotated = rotated / 10 + lastDigit * powTen;
        }
        return true;
    }
    
    private bool IsEvenOr5(int n) {
        while (n > 0) {
            int d = n % 10;
            if (d % 2 == 0 || d == 5) return true;
            n /= 10;
        }
        return false;
    }
}