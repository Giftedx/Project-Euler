using System.Numerics;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 14: Longest Collatz sequence.
/// Finds the starting number, under one million, which produces the longest Collatz chain.
/// </summary>
public class Problem014 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 14: Longest Collatz sequence.
    /// Finds the starting number, under one million, which produces the longest Collatz chain.
    /// </summary>
    /// <returns>The starting number under one million that produces the longest chain.</returns>
    public override object Solve() {
        return LongestCollatzChain(1000000);
    }

    /// <summary>
    /// Finds the number under 'limit' that generates the longest Collatz sequence.
    /// Uses caching to store the lengths of previously computed sequences.
    /// </summary>
    /// <param name="limit">The upper bound for the starting number.</param>
    /// <returns>The starting number with the longest chain.</returns>
    private int LongestCollatzChain(int limit) {
        int[] cache = new int[limit + 1];

        int maxChainLength = 0;
        int startingNumber = 0;

        for (int i = 1; i < limit; i++) {
            int length = GetChainLength(i, cache);
            if (length > maxChainLength) {
                maxChainLength = length;
                startingNumber = i;
            }
        }
        return startingNumber;
    }

    /// <summary>
    /// Calculates the Collatz chain length for a number n.
    /// Uses memoization with the 'cache' array.
    /// </summary>
    /// <param name="n">The current number in the sequence.</param>
    /// <param name="cache">The cache array for memoization.</param>
    /// <returns>The length of the chain starting at n.</returns>
    private int GetChainLength(long n, int[] cache) {
        if (n == 1) return 1;

        if (n < cache.Length && cache[n] != 0) {
            return cache[n];
        }

        int length;
        if (n % 2 == 0) {
            length = 1 + GetChainLength(n / 2, cache);
        } else {
            length = 1 + GetChainLength(3 * n + 1, cache);
        }

        if (n < cache.Length) {
            cache[n] = length;
        }

        return length;
    }
}
