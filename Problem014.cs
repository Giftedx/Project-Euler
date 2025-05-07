namespace Project_Euler;
public class Problem014:Problem {
    public override void Solve() {
        Print(LongestChain(1000000));
    }

    private int[] _cache = null!;
    private int LongestChain(int n) {
        int longestChain = 0;
        int longestChainSeed = 0;
        _cache = new int[n];
        for (int i = 0; i < n; i++) {
            int collatzLength = CollatzLength(i);
            if (collatzLength > longestChain) {
                longestChain = collatzLength;
                longestChainSeed = i;
            }
        }
        return longestChainSeed;
    }

    private int CollatzLength(long n) {
        int length = 1;
        int start = (int)n;
        while (n > 1) {
            if (n < start) {
                length += _cache[(int)n];
                break;
            }
            if (n % 2 == 0) n /= 2;
            else n = 3 * n + 1;
            length++;
        }
        _cache[start] = length;
        return length;
    }
}