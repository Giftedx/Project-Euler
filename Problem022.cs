// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
namespace Project_Euler;
public class Problem022 : Problem {
    private List<string> _names;
    public Problem022() {
        Library.ReadFile("names.txt", out _names);
    }

    public override void Solve() {
        Print(SumNameScores());
    }

    private long SumNameScores() {
        _names = _names.OrderBy(line => line).ToList();
        long sum = 0;
        for (int i = 0; i < _names.Count; i++)
            sum += NameScore(_names[i], i);
        return sum;
    }

    private int NameScore(string s, int pos) {
        int sum = 0;
        foreach (char c in s) sum += (c & 15) + 1;
        return (pos + 1) * sum;
    }
}