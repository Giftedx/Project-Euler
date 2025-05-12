namespace Project_Euler;

public class Test {
    private readonly bool[] _isPrimeCache;

    public Test() {
        Library.SieveOfEratosthenes(1000000, out _isPrimeCache);
    }

    public void Solve() {
        Console.WriteLine(ConsecutivePrimeSumBelow(1000000));
    }

    private long ConsecutivePrimeSumBelow(int n) {
        int highestPrime = n - Enumerable
            .Range(1, n)
            .First(i => Library.IsPrime(n - i));

        int maxSequence = GetMaxSequence(highestPrime);

        for (int sequence = maxSequence;; sequence--) {
            Queue<int> primes = new(sequence);
            primes.Enqueue(2);

            for (int i = 3; i < n; i += 2) {
                if (!_isPrimeCache[i]) continue;
                if (primes.Count < sequence) {
                    primes.Enqueue(i);
                    if (primes.Count < sequence) continue;
                }
                else {
                    primes.Dequeue();
                    primes.Enqueue(i);
                }

                int sum = primes.Sum();
                if (sum > highestPrime) break;
                if (Library.IsPrime(sum)) return sum;
            }
        }
    }

    private int GetMaxSequence(int highestPrime) {
        int maxSequence = 1;
        for (int i = 3, sum = 2;; i += 2) {
            if (sum + i > highestPrime) break;
            if (!_isPrimeCache[i]) continue;
            maxSequence++;
            sum += i;
        }

        return maxSequence;
    }
}