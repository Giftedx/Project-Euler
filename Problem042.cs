using System.Diagnostics;

namespace Project_Euler;

public class Problem042 : Problem{
    private List<string> _words = null!;
    public override void Solve() { 
        Stopwatch fileReadTimer =  Stopwatch.StartNew();
        Library.ReadFile("words.txt", out _words);
        fileReadTimer.Stop();
        Print(CountWordScoreTriangleNums());
        Console.WriteLine("File read in {0} ms",
            fileReadTimer.ElapsedMilliseconds);
    }

    private int CountWordScoreTriangleNums() {
        return _words.Count(word => IsTriangle(WordValue(word)));
    }

    private int WordValue(string s) {
        return s.Sum(c => c - 'A' + 1);
    }

    private bool IsTriangle(int x) {
        for (int i = 1; ; i++) {
            int t = i * (i + 1) / 2;
            if (t == x) return true;
            if (t > x) return false;
        }
    }
}