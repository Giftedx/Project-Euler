using System;

namespace Project_Euler {
    public class Problem07 : Problem{
        public override void Solve() {
            Print(NthPrime(10001));
        }

        private int NthPrime(int n) {
            int i = 2;
            while (n > 0) {
                if (IsPrime(i)) n--;
                i++;
            }

            return i - 1;
        }

        private bool IsPrime(int n) {
            if(n <= 1)return false;
            if(n == 2 || n == 3) return true;
            if(n % 2 == 0 || n % 3 == 0) return false;
            for (int i = 5; i * i <= n; i += 6) {
                if(n % i == 0 || n % (i + 2) == 0) return false;
            }
            return true;
        }
    }
}