namespace Project_Euler;

public class Problem047 : Problem {
    public override void Solve() {
        Print(FirstInFourPrimeRun());
    }

    private int FirstInFourPrimeRun() {
        int i = 2;
        var result = new Result(false, 0);
        while (!result.Valid) {
            result = FourConsecutivePrimes(i);
            if (!result.Valid) i *= 2;
        }

        return result.Number;
    }

    private Result FourConsecutivePrimes(int limit) {
        var factors = Enumerable.Repeat(0, limit).ToList();
        int consecutive = 0;
        for (int i = 2; i < limit; i++) {
            switch (factors[i]) {
                case 0: {
                    for (int j = 2; i * j < limit; j++) factors[i * j]++;
                    consecutive = 0;
                    break;
                }
                case 4:
                    consecutive++;
                    break;
                default:
                    consecutive = 0;
                    break;
            }

            if (consecutive == 4)
                return new Result(true, i - 3);
        }

        return new Result(false, 0);
    }

    private struct Result(bool valid, int number) {
        public readonly bool Valid = valid;
        public readonly int Number = number;
    }
}