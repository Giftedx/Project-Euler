namespace Project_Euler;
public class Problem010 : Problem{
    public override void Solve() {
        Print(SumPrimesBelow(2000000));
    }

    private long SumPrimesBelow(int n) {
        long sum = 0;
        Library.SieveOfEratosthenes(n, out bool[] isPrime);
        for(int i = 2; i < isPrime.Length; i++)if(isPrime[i])sum += i;
        return sum;
    }
}