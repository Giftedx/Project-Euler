namespace Project_Euler;

public class Problem010 : Problem {
    private const int Limit = 2000000;
    private readonly bool[] _isPrime;

    public Problem010() {
        Library.SieveOfEratosthenes(Limit, out _isPrime);
    }

    public override void Solve() {
        Print(SumPrimesBelow());
    }

    private long SumPrimesBelow() {
        long sum = 2;
        for (int i = 3; i < _isPrime.Length; i += 2)
            if (_isPrime[i])
                sum += i;
        return sum;
    }
}