using System.Collections; // Required for BitArray

namespace Project_Euler;

public class Problem010 : Problem {
    private const int Limit = 2000000;
    private readonly BitArray _isPrime; // Changed from bool[] to BitArray

    public Problem010() {
        _isPrime = Library.SieveOfEratosthenesBitArray(Limit); // Changed to use BitArray sieve
    }

    public override object Solve() {
        return SumPrimesBelow();
    }

    private long SumPrimesBelow() {
        long sum = 0;
        // _isPrime array is of size Limit + 1. Indices from 0 to Limit.
        // Primes start from 2.
        for (int i = 2; i <= Limit; i++) {
            if (_isPrime[i]) {
                sum += i;
            }
        }
        return sum;
    }
}