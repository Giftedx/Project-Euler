// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

using System.Runtime.CompilerServices;

namespace Project_Euler;

public class Problem022 : Problem {
    private readonly List<string> _names;

    public Problem022() {
        Library.ReadFile("names.txt", out _names);
    }

    public override void Solve() {
        Print(SumNameScores());
    }

    private long SumNameScores() {
        _names.Sort();
        long sum = 0;
        for (int i = 0; i < _names.Count; i++)
            sum += (long)(i + 1) * GetNameScore(_names[i]);
        return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetNameScore(string name) {
        int score = 0;
        foreach (char t in name)
            score += (t - 'A') & 15;
        return score;
    }
}