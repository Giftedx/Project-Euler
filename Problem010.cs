namespace Project_Euler;

public class Problem010 : Problem {
    public override void Solve() {
        Print(SumPrimesBelow(2000000));
    }

    private long SumPrimesBelow(int n) {
        long sum = 2;
        Library.SieveOfEratosthenes(n, out bool[] isPrime);
        for (int i = 3; i < isPrime.Length; i += 2)
            if (isPrime[i])
                sum += i;
        return sum;
    }
}