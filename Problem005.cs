namespace Project_Euler;

public class Problem005 : Problem{
    public override void Solve() {
        Print(MinimumEvenlyDivisibleByRange(1, 20));
    }

    private string MinimumEvenlyDivisibleByRange(int min, int max) {
        ulong result = 1;
        for (int i = min; i <= max; i++) {
            result = LeastCommonMultiple((ulong)i, result);
        }
        return result.ToString();
    }

    private ulong LeastCommonMultiple(ulong a, ulong b) {
        return a / GreatestCommonDivisor(a, b) * b;
    }

    private ulong GreatestCommonDivisor(ulong a, ulong b) {
        if(a == 0)return b;
        return GreatestCommonDivisor(b % a, a);
    }
}