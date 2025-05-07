using System;
using System.Numerics;

namespace Project_Euler {
    public class Problem16 : Problem {
        public override void Solve() {
            Print(PowerDigitSum(2, 1000));
        }

        private int PowerDigitSum(int n, int exponent) {
            BigInteger digits = BigInteger.Pow(n,  exponent);
            return Library.SumDigits(digits);
        }
    }
}