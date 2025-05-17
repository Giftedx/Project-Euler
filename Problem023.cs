// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

namespace Project_Euler;

public class Problem023 : Problem {
    private const int Limit = 28123;
    private readonly int[] _properDivisorSum = new int[Limit];
    
    public Problem023() {
        for (int i = 1; i < Limit; i++) {
            for (int j = 2 * i; j < Limit; j += i) {
                _properDivisorSum[j] += i;
            }
        }
    }

    public override object Solve() {
        return SumOfNonAbundantBelow();
    }

    private int SumOfNonAbundantBelow() {
        List<int> allAbundantNumbers = [];
        for (int n = 12; n < Limit; n++)
            if (IsAbundant(n)) allAbundantNumbers.Add(n);
        
        Dictionary<int, List<int>> abundantNumbers = new();
        for (int i = 0; i < 6; i++) abundantNumbers[i] = [];

        foreach (int n in allAbundantNumbers)
            abundantNumbers[n % 6].Add(n);

        HashSet<int> abundantSums = [];
        
        for(int i = 0; i < 5; i++)
            if (abundantNumbers[i].Count > i)
                for (int j = 12 + abundantNumbers[i].Min(); j < Limit; j += 6)
                    abundantSums.Add(j);
        
        if (Limit > 40) abundantSums.Add(40);

        for (int n = 0; n < Limit; n++) {
            if (n % 6 != 1 && n % 6 != 5) continue;
            foreach (int a in abundantNumbers[3]) {
                if (!IsAbundant(n - a)) continue;
                abundantSums.Add(n);
                break;
            }
        }
        
        int total = 0;
        for (int i = 0; i < Limit; i++)
            if (!abundantSums.Contains(i)) 
                total += i;
        return total;
        bool IsAbundant(int n) => n > 0 && _properDivisorSum[n] > n;
    }
}