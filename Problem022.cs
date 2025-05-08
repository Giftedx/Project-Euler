using System.Diagnostics;
namespace Project_Euler;
public class Problem022 :  Problem {
    private List<string> _names = null!;
    public override void Solve() {
        Stopwatch fileReadTimer =  Stopwatch.StartNew();
        Library.ReadFile("names.txt", out _names);
        fileReadTimer.Stop();
        Print(SumNameScores());
        Console.WriteLine("File read in {0} ms", fileReadTimer.ElapsedMilliseconds);
    }
        
    private long SumNameScores(){
        _names = _names.OrderBy(line => line).ToList();
        long sum = 0;
        for (int i = 0; i < _names.Count; i++) {
            sum += NameScore(_names[i], i);
        }
        return sum;
    }

    private int NameScore(string s, int pos){
        int sum = s.Sum(c => c - 'A' + 1);
        return (pos + 1) * sum;
    }
}