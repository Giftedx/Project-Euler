namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 31: Coin sums.
/// Finds the number of different ways £2 can be made using any number of coins.
/// </summary>
public class Problem031 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 31: Coin sums.
    /// </summary>
    /// <returns>The number of ways to make £2.</returns>
    public override object Solve() {
        return CoinSums(200);
    }

    /// <summary>
    /// Calculates the number of ways to make 'target' pence using UK coins.
    /// Uses dynamic programming.
    /// </summary>
    private int CoinSums(int target) {
        int[] coins = { 1, 2, 5, 10, 20, 50, 100, 200 };
        int[] ways = new int[target + 1];
        ways[0] = 1;

        foreach (int coin in coins) {
            for (int j = coin; j <= target; j++) {
                ways[j] += ways[j - coin];
            }
        }
        return ways[target];
    }
}
