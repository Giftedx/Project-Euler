namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 31: Coin sums.
/// Calculates the number of different ways £2 (200 pence) can be made using any number of British coins.
/// The available coins are 1p, 2p, 5p, 10p, 20p, 50p, £1 (100p), and £2 (200p).
/// Further details can be found at https://projecteuler.net/problem=31
/// </summary>
public class Problem031 : Problem {
    /// <summary>
    /// Array of available British coin denominations in pence.
    /// </summary>
    private readonly int[] _coins = [1, 2, 5, 10, 20, 50, 100, 200];

    /// <summary>
    /// Calculates the solution for Project Euler Problem 31.
    /// It determines the number of different ways 200 pence can be made using the available coin denominations.
    /// </summary>
    /// <returns>The total number of ways to make 200 pence.</returns>
    public override object Solve() {
        return CoinCombosForN(200); // Target amount is 200 pence.
    }

    /// <summary>
    /// Calculates the number of different ways a target amount <paramref name="targetAmount"/> can be made
    /// using the coin denominations specified in the <see cref="_coins"/> array.
    /// This method uses a dynamic programming approach (often known as the "coin change problem" or "unbounded knapsack type problem").
    ///
    /// Algorithm:
    /// 1. Create an array `combinations` of size `targetAmount + 1`. `combinations[i]` will store the number of ways to make amount `i`.
    /// 2. Initialize `combinations[0] = 1`. This signifies one way to make amount 0 (by choosing no coins).
    /// 3. For each `coin` in the `_coins` array:
    ///    Iterate from `amount = coin` up to `targetAmount`.
    ///    For each `amount`, update `combinations[amount]` by adding `combinations[amount - coin]`.
    ///    This means: the number of ways to make `amount` using the current `coin` (and previously processed coins)
    ///    is increased by the number of ways to make `amount - coin` (because we can form `amount` by adding the
    ///    current `coin` to all combinations that sum up to `amount - coin`).
    /// 4. After iterating through all coins, `combinations[targetAmount]` will hold the total number of ways.
    /// </summary>
    /// <param name="targetAmount">The target amount (in pence) for which to find the number of coin combinations.</param>
    /// <returns>The total number of different ways the <paramref name="targetAmount"/> can be made.</returns>
    private int CoinCombosForN(int targetAmount) {
        // Array to store the number of combinations for each amount from 0 to targetAmount.
        // combinations[i] will store the number of ways to make amount 'i'.
        int[] combinations = new int[targetAmount + 1];

        // Base case: There is one way to make amount 0 (by using no coins).
        combinations[0] = 1;

        // Iterate through each available coin denomination.
        foreach (int coin in _coins) {
            // For each coin, update the combinations array for amounts from 'coin' up to 'targetAmount'.
            for (int amount = coin; amount <= targetAmount; amount++) {
                // The number of ways to make 'amount' is increased by the number of ways to make 'amount - coin'.
                // This is because we can form 'amount' by taking a combination for 'amount - coin' and adding the current 'coin'.
                combinations[amount] += combinations[amount - coin];
            }
        }
        // The final answer is the number of ways to make the targetAmount.
        return combinations[targetAmount];
    }
}