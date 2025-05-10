namespace Project_Euler;
public class Problem014:Problem {
    public override void Solve() {
        Print(LongestChain());
    }
    private int[] _collatzChainLength = null!;

    private int LongestChain() {
        const int limit = 1000000;
        _collatzChainLength = new int[limit];
        int maxArg = -1;
        int maxChain = 0;
        for (int i = 1; i < limit; i++) {
            int chainLen = CollatzChainLength(i);
            if (chainLen <= maxChain) continue;
            maxArg = i;
            maxChain = chainLen;
        }
        return maxArg;
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
        if (n % 2 == 0) return CollatzChainLength(n/2) + 1;
        return CollatzChainLength(3 * n + 1) + 1;
    }
}