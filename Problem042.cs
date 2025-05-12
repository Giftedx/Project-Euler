namespace Project_Euler;

public class Problem042 : Problem {
    private readonly List<string> _words;
    public Problem042() {
        Library.ReadFile("words.txt", out _words);
    }

    public override void Solve() {
        Print(CountWordScoreTriangleNums());
    }

    private int CountWordScoreTriangleNums() {
        return _words.Count(word => IsTriangle(WordValue(word)));
    }

    private int WordValue(string s) {
        return s.Sum(c => c - 'A' + 1);
    }

    private bool IsTriangle(int x) {
        for (int i = 1;; i++) {
            int t = i * (i + 1) / 2;
            if (t == x) return true;
            if (t > x) return false;
        }
    }
}