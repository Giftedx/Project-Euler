namespace Project_Euler;

public class Problem035 : Problem {
    public override void Solve() {
        Print(CircularPrimeCount(1000000));
    }
    
    private HashSet<int> _isPrime = null!;

    private int CircularPrimeCount(int n){
        IList<int> numbers = Enumerable.Range(0, n).ToList();
        Library.GetPrimeList(numbers, out _isPrime);
        return _isPrime.Count(IsCircularPrime);
    }

    private bool IsCircularPrime(int n){
        string s = n.ToString();
        for(int i = 0; i < s.Length; i++) {
            int index = int.Parse(s[i..] + s[..i]);
            if (!_isPrime.Contains(index)) return false;
        }
        return true;
    }
}