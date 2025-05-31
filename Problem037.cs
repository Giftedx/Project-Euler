namespace Project_Euler;

public class Problem037 : Problem {
    private const int Limit = 700000; // Max prime to sieve up to. Truncatable primes are not extremely large.
    private readonly bool[] _isPrime;

    public Problem037() {
        // SieveOfEratosthenesBoolArray returns an array of size Limit + 1
        _isPrime = Library.SieveOfEratosthenesBoolArray(Limit);
    }

    public override object Solve() {
        return SumTruncatablePrimes();
    }

    private long SumTruncatablePrimes() {
        var results = new List<int>();
        var queue = new Queue<int>([2, 3, 5, 7]);

        while (results.Count < 11) {
            int n = queue.Dequeue();
            if (n > 10 && IsTruncatable(n))
                results.Add(n);

            foreach (int d in new[] { 1, 3, 7, 9 }) {
                int next = n * 10 + d;
                if (IsPrime(next)) queue.Enqueue(next);
            }
        }

        return results.Sum();
    }

    private bool IsTruncatable(int n) {
        int x = n;
        while (x > 0) {
            if (!IsPrime(x)) return false;
            x /= 10;
        }

        int div = Library.Pow10(Library.DigitCount(n) - 1);
        x = n;
        while (div > 0) {
            if (!IsPrime(x)) return false;
            x %= div;
            div /= 10;
        }

        return true;
    }

    private bool IsPrime(int n) {
        // Check if n is within the bounds of the sieve array
        if (n >= 0 && n <= Limit) {
            return _isPrime[n];
        }
        // If n is outside the sieve's range (e.g., negative or too large), use the general IsPrime method.
        // (Though for this problem, numbers are positive and likely within a reasonable range from the queue generation)
        return Library.IsPrime(n);
    }
}