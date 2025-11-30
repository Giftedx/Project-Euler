namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 25: 1000-digit Fibonacci number.
/// Finds the index of the first term in the Fibonacci sequence to contain 1000 digits.
/// </summary>
public class Problem025 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 25: 1000-digit Fibonacci number.
    /// </summary>
    /// <returns>The index of the first term in the Fibonacci sequence to contain 1000 digits.</returns>
    public override object Solve() {
        return FibonacciNDigits(1000);
    }

    /// <summary>
    /// Finds index of first Fib number with n digits using Binet's formula approximation.
    /// </summary>
    private int FibonacciNDigits(int numDigits) {
        if (numDigits <= 1) return 1;

        double phi = (1 + Math.Sqrt(5)) / 2.0;
        double log10Phi = Math.Log10(phi);
        double log10Sqrt5 = Math.Log10(Math.Sqrt(5));

        int index = 2;
        while (true) {
            double logFib = index * log10Phi - log10Sqrt5;
            int currentDigitCount = (int)Math.Floor(logFib) + 1;

            if (currentDigitCount >= numDigits) {
                break;
            }
            index++;
        }
        return index;
    }
}
