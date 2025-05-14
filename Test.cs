using System.Collections;
using System.Numerics;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Project_Euler;

public class Test {
    //private bool[] _isPrime;
    /*public Test() {
        Library.SieveOfEratosthenes(1000000, out _isPrime);
    }*/
    
    public void Solve() {
        Console.WriteLine(FindMinimumD());
    }

    private long FindMinimumD() {
        long smallestDiff = long.MaxValue;  // Use long.MaxValue as LONG_MAX equivalent
        long j = 0, k = 1;
        long Pj = 0, Pk = 1;
        int isNot;

        // Outer loop to find the smallest difference
        do
        {
            j = 0;
            Pj = 0;
            isNot = 1;

            // Calculate the next pentagonal number Pk
            Pk += k + k + k + 1;
            k++;

            // Inner loop to check pairs of pentagonal numbers
            do
            {
                // Update Pj to the next pentagonal number
                Pj += j + j + j + 1;
                j++;

                // Check if both Pj + Pk and Pk - Pj are pentagonal numbers
                isNot = (IsPentagonal(Pj + Pk) == false || IsPentagonal(Pk - Pj) == false) ? 1 : 0;

            } while (j < k && isNot == 1);

            // If both numbers are pentagonal, check the difference
            if (isNot == 0 && Pk - Pj < smallestDiff)
            {
                smallestDiff = Pk - Pj;
            }

        } while (isNot == 1 && Pk - Pj < smallestDiff);

        // Print the smallest difference
        return smallestDiff;
    }

    // Function to check if a number is pentagonal
    static bool IsPentagonal(long num)
    {
        // Solving P = n(3n - 1) / 2 for n, we get:
        // n = (1 + sqrt(1 + 24P)) / 6
        double sqrdiscr = Math.Sqrt(1 + 24 * num);
        return sqrdiscr == (long)sqrdiscr && (long)sqrdiscr % 6 == 5;
    }
}