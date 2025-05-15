namespace Project_Euler;

public class Problem037 : Problem {
    private readonly bool[] _isPrime;

    public Problem037() {
        Library.SieveOfEratosthenes(700000, out _isPrime);
    }

    public override void Solve() {
        Print(SumTruncatablePrimes());
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
        return n < _isPrime.Length ? _isPrime[n] : Library.IsPrime(n);
    }
}