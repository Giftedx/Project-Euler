using System;

namespace Project_Euler {
    public class Problem10 : Problem{
        public override void Solve() {
            Print(SumPrimesBelow(2000000));
        }

        private long SumPrimesBelow(int n) {
            long sum = 0;
            SieveOfEratosthenes(n, out bool[] isPrime);
            for(int i = 2; i < isPrime.Length; i++)if(isPrime[i])sum += i;
            return sum;
        }

        private void SieveOfEratosthenes(int n, out bool[] isPrime) {
            isPrime = new bool[n];
            for (int i = 0; i < isPrime.Length; i++) isPrime[i] = true;
            isPrime[0] = false;
            isPrime[1] = false;
            for (int i = 2; i < Math.Sqrt(n); i++) {
                if (!isPrime[i]) continue;
                for (int j = i * i; j < n; j += i) {
                    isPrime[j] = false;
                }
            }
        }
    }
}