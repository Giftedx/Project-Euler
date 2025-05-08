namespace Project_Euler;

public class Problem049 : Problem {
    public override void Solve() {
        Print(OtherPrimePermuteConcat());
    }

    private string OtherPrimePermuteConcat() {
        const int limit = 10000;
        Library.SieveOfEratosthenes(limit, out bool[] isPrime);
        for(int i = 1000; i < limit; i++) {
            if (!isPrime[i]) continue;
            for(int gap = 1; gap < limit; gap++){
                int i1 = i + gap;
                int i2 = i1 + gap;
                if(i1 < limit && isPrime[i1] && SameDigits(i1, i) &&
                   i2 < limit && isPrime[i2] && SameDigits(i2, i) &&
                   i != 1487 && i != 4817 && i != 8147)
                    return $"{i}{i1}{i2}";
            }
        }
        return ":(";
    }

    private bool SameDigits(int n, int m){
        int[] counter = new int[10];
        while (n > 0) {
            counter[n % 10]++;
            n /= 10;
        }
        while (m > 0) {
            counter[m % 10]--;
            m/= 10;
        }
        foreach (int t in counter)
            if (t != 0) return false;
        return true;
    }
}