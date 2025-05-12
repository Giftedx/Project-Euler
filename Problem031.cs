namespace Project_Euler;

public class Problem031 : Problem {
    private readonly int[] _coins = [1, 2, 5, 10, 20, 50, 100, 200];

    public override void Solve() {
        Print(CoinCombosForN(200));
    }

    private int CoinCombosForN(int n) {
        int[,] combos = new int[_coins.Length + 1, n + 1];
        combos[0, 0] = 1; //one way to pay 0.00
        for (int i = 0; i < _coins.Length; i++) {
            int coin = _coins[i];
            for (int j = 0; j <= n; j++)
                combos[i + 1, j] = combos[i, j] +
                                   (j >= coin ? combos[i + 1, j - coin] : 0);
        }

        return combos[_coins.Length, n];
    }
}