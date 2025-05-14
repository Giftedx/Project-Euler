using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Project_Euler;

public class Test {
    //private bool[] _isPrime;
    /*public Test() {
        Library.SieveOfEratosthenes(1000000, out _isPrime);
    }*/

    public void Solve() {
        
        Console.WriteLine(FindMinimumDifference());
    }

    
    private int FindMinimumDifference() {
        int m = 1;
        while (true) {
            int diff2 = m * (3 * m - 1);
            int d = 1;

            while (d * d < diff2) {
                if (diff2 % d == 0) {
                    int numerator = (diff2 / d - 3 * d + 1);
                    if (numerator % 6 == 0) {
                        int j = numerator / 6;
                        int k = j + d;

                        int pk = k * (3 * k - 1) / 2;
                        int pj = j * (3 * j - 1) / 2;

                        if (j > 0 && IsPentagonal(pk + pj) &&
                            IsPentagonal(pk - pj)) return pk - pj;
                    }
                }
                d++;
            }
            m++;
        }
    }

    private bool IsPentagonal(int num) {
        double n = (1 + Math.Sqrt(1 + 24 * num)) / 6;
        return Math.Abs(n - Math.Round(n)) < 1e-10 && 2 * num ==
            (int)Math.Round(n) * (3 * (int)Math.Round(n) - 1);
    }
}