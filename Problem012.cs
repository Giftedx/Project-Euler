namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 12: Highly divisible triangular number.
/// Further details can be found at https://projecteuler.net/problem=12
/// </summary>
public class Problem012 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 12: Highly divisible triangular number.
    /// Finds the value of the first triangle number to have over five hundred divisors.
    /// </summary>
    /// <returns>The value of the first triangle number with over 500 divisors.</returns>
    public override object Solve() {
        return HighlyDivisibleTriangle(500);
    }

    /// <summary>
    /// Finds the first triangle number that has at least a specified minimum number of divisors.
    /// A triangle number T_n is given by T_n = n * (n+1) / 2.
    /// The method leverages the property that n and (n+1) are coprime.
    /// Thus, the number of divisors tau(T_n) can be found by multiplying the number of divisors
    /// of n/2 and (n+1) if n is even, or n and (n+1)/2 if n is odd.
    /// This is because tau(a*b) = tau(a)*tau(b) if a and b are coprime.
    /// The components n/2 and (n+1) (or n and (n+1)/2) are coprime.
    /// </summary>
    /// <param name="minDivisors">The minimum number of divisors the triangle number must have.</param>
    /// <returns>The first triangle number having more than <paramref name="minDivisors"/> divisors.</returns>
    private long HighlyDivisibleTriangle(int minDivisors) {
        int n = 1;
        long triangleNumber = 1; // T_1 = 1

        while (true) {
            int divisorsCount;
            // T_n = n * (n+1) / 2.
            // Since n and n+1 are coprime, we can calculate tau(T_n) using components.
            // Let A = n/2, B = n+1 if n is even. Then T_n = A*B. tau(T_n) = tau(A)*tau(B).
            // Let A = n, B = (n+1)/2 if n is odd. Then T_n = A*B. tau(T_n) = tau(A)*tau(B).
            if (n % 2 == 0) {
                divisorsCount = Tau(n / 2) * Tau(n + 1);
            } else {
                divisorsCount = Tau(n) * Tau((n + 1) / 2);
            }

            if (divisorsCount > minDivisors) {
                triangleNumber = (long)n * (n + 1) / 2; // Calculate the triangle number T_n = n(n+1)/2
                return triangleNumber;
            }
            n++;
        }
    }

    /// <summary>
    /// Calculates the number of divisors of a given integer (tau function).
    /// It iterates from 1 up to the square root of the number.
    /// If 'i' is a divisor, then 'num/i' is also a divisor. So, 2 is added to the count.
    /// If the number is a perfect square, its square root will be counted twice in this process,
    /// so the count is decremented by 1 to correct this.
    /// </summary>
    /// <param name="num">The integer for which to count divisors. Must be positive.</param>
    /// <returns>The total number of divisors of <paramref name="num"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if num is not positive.</exception>
    private int Tau(int num) {
        if (num <= 0) {
            // Or handle as appropriate, e.g. Tau(0) is undefined or Tau(1)=1.
            // For this problem, inputs to Tau are expected to be positive from n/2 or n+1.
            throw new ArgumentOutOfRangeException(nameof(num), "Number must be positive to calculate divisors.");
        }
        if (num == 1) return 1;

        int count = 0;
        int root = (int)Math.Sqrt(num);
        for (int i = 1; i <= root; i++) {
            if (num % i == 0) {
                count += 2; // i and num/i are divisors
            }
        }
        // If num is a perfect square, its square root was counted twice (as i and as num/i when i*i=num)
        // So, we subtract one from the count.
        if (root * root == num) {
            count--;
        }
        return count;
    }
}