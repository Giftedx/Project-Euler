namespace Project_Euler;

public class Problem043 : Problem {
    private readonly int[] _tests = [2, 3, 5, 7, 11, 13, 17];
    private readonly bool[] _used = new bool[10];

    public override void Solve() {
        Console.WriteLine(SubStringDivisiblePandigitalSum());
    }

    private long SubStringDivisiblePandigitalSum() {
        long total = 0;
        BuildPandigitalSum("", ref total);
        return total;
    }

    private void BuildPandigitalSum(string s, ref long total) {
        switch (s.Length) {
            case > 3 when int.Parse(s.Substring(s.Length - 3, 3))
                % _tests[s.Length - 4] != 0:
                return;
            case 10:
                total += long.Parse(s);
                break;
        }

        for (char i = s.Length == 0 ? '1' : '0'; i <= '9'; i++) {
            int index = i & 15;
            if (_used[index]) continue;
            _used[index] = true;
            BuildPandigitalSum(s + i, ref total);
            _used[index] = false;
        }
    }
}