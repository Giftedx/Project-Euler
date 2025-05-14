using System.Runtime.CompilerServices;
namespace Project_Euler;

public class Problem023 : Problem {
    private const int Limit = 20161;
    private readonly bool[] _isAbundant = new bool[Limit + 1];
    private int _smallestAbundant;
    
    public override void Solve() {
        Print(SumOfNonAbundantBelow());
    }

    private long SumOfNonAbundantBelow() {
        int[] abundantList = new int[7000];
        int abundantCount = 0;

        for (int i = 12; i <= Limit; i++) {
            if (!IsAbundant(i)) continue;
            _isAbundant[i] = true;
            abundantList[abundantCount++] = i;
        }
        _smallestAbundant = 12;
        long total = 0;

        Span<int> smallAbundantNumbers = stackalloc int[48];
        int smallAbundantCount = 0;
        for (int i = 0; i < abundantCount && abundantList[i] < 48; i++)
            smallAbundantNumbers[smallAbundantCount++] = abundantList[i];

        Span<int> oddAbundants = stackalloc int[abundantCount];
        int oddAbunCount = 0;
        for (int i = 0; i < abundantCount; i++) {
            int a = abundantList[i];
            if ((a & 1) == 1) oddAbundants[oddAbunCount++] = a;
        }
        for (int k = 1; k < _smallestAbundant; k++)
            if (IsAbundantSum(k, smallAbundantNumbers, smallAbundantCount)) 
                total += k;
        for (int k = 49; k <= 956; k += 2) total += k;
        for (int k = 957; k <= Limit; k += 2) 
            if (!IsAbundantSum(k, oddAbundants, oddAbunCount)) total += k;
        return total;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsAbundant(int n) {
        int sum = 1;
        int sqrt = (int)Math.Sqrt(n);
        for (int i = 2; i <= sqrt; i++) {
            if (n % i != 0) continue;
            sum += i;
            int div = n / i;
            if (div != i) sum += div;
        }
        return sum > n;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsAbundantSum(int k, Span<int> candidates, int count) {
        for (int i = 0; i < count; i++) {
            int x = candidates[i];
            if (x >= k) break;
            if (k - x <= Limit && _isAbundant[k - x]) return true;
            if (x - _smallestAbundant > k) return false;
        }
        return false;
    }
}