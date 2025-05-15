namespace Project_Euler;

public class Test {
    private const int Limit = 10000;
    private readonly bool[] _isPrime;

    public Test() {
        Library.SieveOfEratosthenes(Limit, out _isPrime);
    }

    public void Solve() {
        Console.WriteLine(OtherPrimePermuteConcat());
    }

    private string OtherPrimePermuteConcat() {
        const int gap = 3330;
        // Iterate over odd numbers starting from 1001 up to Limit
        for (int i = 1001; i < Limit; i += 2) {
            // Skip if the number is not prime
            if (!_isPrime[i]) continue;

            // Calculate the two other numbers with the same difference
            int i1 = i + gap;
            int i2 = i1 + gap;

            // Check if both i1 and i2 are prime and have the same digits as i
            if (i1 < Limit && _isPrime[i1] && SameDigits(i, i1) &&
                i2 < Limit && _isPrime[i2] && SameDigits(i, i2) &&
                i != 1487 && i != 4817 && i != 8147)
                return $"{i}{i1}{i2}";
        }

        return ":(";
    }

    private bool SameDigits(int n, int m) {
        // Check if two numbers have the same digits
        int[] counter = new int[10];

        // Count digits for n and m
        while (n > 0) {
            counter[n % 10]++;
            counter[m % 10]--;
            n /= 10;
            m /= 10;
        }

        // If the counter is not zero for any digit, they are not the same
        foreach (int t in counter)
            if (t != 0)
                return false;

        return true;
    }
}