namespace Project_Euler;

public class Problem026 : Problem {
    public override void Solve() {
        Print(GetLongestCycleDenominator(1000));
    }

    private int GetLongestCycleDenominator(int limit) {
        int maxLength = 0;
        int result = 0;
        int[] seen = new int[limit];
        for (int d = 2; d < limit; d++) {
            if (maxLength >= d) continue;
            
            int value = 1;
            int position = 0;
            while (value != 0 && seen[value] == 0) {
                seen[value] = position++;
                value = value * 10 % d;
            }

            int length = position - seen[value];
            if (length <= maxLength) continue;
            maxLength = length;
            result = d;
        }

        return result;
    }
}