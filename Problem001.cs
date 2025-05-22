namespace Project_Euler;

public class Problem001 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 1: Multiples of 3 or 5.
    /// Finds the sum of all the multiples of 3 or 5 below 1000.
    /// </summary>
    /// <returns>The sum of all multiples of 3 or 5 below 1000.</returns>
    public override object Solve() {
        return SumMultiples(3, 5, 1000);
    }

    private int SumMultiples(int m1, int m2, int n) {
        return SumDivisibleBy(m1, n - 1) +
               SumDivisibleBy(m2, n - 1) -
               SumDivisibleBy(Lcm(m1, m2), n - 1);
    }

    private int SumDivisibleBy(int m, int limit) {
        int p = limit / m;
        return m * p * (p + 1) / 2;
    }

    private int Lcm(int a, int b) {
        return a / Library.Gcd(a, b) * b;
    }
}