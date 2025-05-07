using System;

namespace Project_Euler {
    public class Problem01 :  Problem {
        public override void Solve() {
            Print(SumMultiples(3, 5, 1000));
        }

        private int SumMultiples(int m1, int m2, int n) {
            int sum = 0;
            for (int i = 0; i < n; i++) {
                if (i % m1 == 0 || i % m2 == 0) {
                    sum += i;
                }
            }
            return sum;
        }
    }
}