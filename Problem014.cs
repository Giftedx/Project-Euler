namespace Project_Euler;

public class Problem014 : Problem {
    private int[] _collatzChainLength = [];
    private const int Limit = 1000000;

    public override void Solve() {
        Print(LongestChain());
    }

    private int LongestChain() {
        _collatzChainLength = new int[Limit];
        _collatzChainLength[1] = 1;

        int maxSeed = 1;
        int maxChain = 1;

        for (int i = 1; i < Limit; i += 2) {
            int len = CollatzChainLength(i);
            if (len <= maxChain) continue;
            maxChain = len;
            maxSeed = i;
        }
        return maxSeed;
    }

    private int CollatzChainLength(long n) {
        if (n < _collatzChainLength.Length) {
            if (_collatzChainLength[n] != 0)
                return _collatzChainLength[n];

            int result;
            if ((n & 1) == 0) result = CollatzChainLength(n >> 1) + 1;
            else result = CollatzChainLength(3 * n + 1) + 1;

            _collatzChainLength[n] = result;
            return result;
        }
        if ((n & 1) == 0) return CollatzChainLength(n >> 1) + 1;
        return CollatzChainLength(3 * n + 1) + 1;
    }
}