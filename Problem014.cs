namespace Project_Euler;

public class Problem014 : Problem {
    private int[] _collatzChainLength = [];

    public override void Solve() {
        Print(LongestChain());
    }

    private int LongestChain() {
        const int limit = 1000000;
        _collatzChainLength = new int[limit];
        int maxSeed = -1;
        int maxChain = 0;
        for (int i = 1; i < limit; i += 2) {
            int chainLen = CollatzChainLength(i);
            if (chainLen <= maxChain) continue;
            maxSeed = i;
            maxChain = chainLen;
        }

        return maxSeed;
    }

    private int CollatzChainLength(long n) {
        if (n >= _collatzChainLength.Length)
            return CollatzChainLengthDirect(n);
        if (_collatzChainLength[n] == 0)
            _collatzChainLength[n] = CollatzChainLengthDirect(n);
        return _collatzChainLength[n];
    }

    private int CollatzChainLengthDirect(long n) {
        if (n == 1) return 1;
        if (n % 2 == 0) return CollatzChainLength(n >> 1) + 1;
        return CollatzChainLength(3 * n + 1) + 1;
    }
}