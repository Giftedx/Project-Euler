namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 25: 1000-digit Fibonacci number.
/// Finds the index of the first term in the Fibonacci sequence to contain 1000 digits.
/// Further details can be found at https://projecteuler.net/problem=25
/// </summary>
public class Problem025 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 25.
    /// It determines the index of the first Fibonacci number that has 1000 digits.
    /// </summary>
    /// <returns>The index of the first Fibonacci number containing 1000 digits.</returns>
    public override object Solve() {
        return FibonacciNDigits(1000); // Find index for a 1000-digit Fibonacci number.
    }

    /// <summary>
    /// Finds the index of the first Fibonacci number that contains a specified number of digits.
    /// The method uses the mathematical property that the number of digits in an integer N is floor(log10(N)) + 1.
    /// For Fibonacci numbers, F_k, Binet's formula states F_k ≈ φ^k / √5, where φ is the golden ratio.
    /// Taking log10, we get log10(F_k) ≈ k * log10(φ) - log10(√5).
    /// The number of digits in F_k is therefore floor(k * log10(φ) - log10(√5)) + 1.
    /// The method iterates through Fibonacci indices 'k', calculating the approximate number of digits
    /// until it finds the first F_k with at least <paramref name="numDigits"/> digits.
    /// </summary>
    /// <param name="numDigits">The desired number of digits for the Fibonacci number.</param>
    /// <returns>The index 'k' of the first Fibonacci number F_k to contain <paramref name="numDigits"/> digits.</returns>
    /// <remarks>
    /// Alternatively, one could solve for k directly:
    /// We need floor(k * log10(φ) - log10(√5)) + 1 = numDigits.
    /// So, k * log10(φ) - log10(√5) must be >= numDigits - 1.
    /// k >= (numDigits - 1 + log10(√5)) / log10(φ).
    /// The smallest integer k is ceil((numDigits - 1 + log10(√5)) / log10(φ)).
    /// The iterative approach used here is also valid and avoids potential floating point precision issues with direct ceil.
    /// </remarks>
    private int FibonacciNDigits(int numDigits) {
        if (numDigits <= 1) return 1; // F_1 = 1 (1 digit). F_0 = 0 (1 digit) if defined.

        double phi = (1 + Math.Sqrt(5)) / 2.0; // Golden ratio φ
        double log10Phi = Math.Log10(phi);         // log10(φ)
        double log10Sqrt5 = Math.Log10(Math.Sqrt(5)); // log10(√5)

        int index = 2; // Start checking from F_2, as F_1 is handled. (F_2 = 1, F_3 = 2, ...)
                       // The loop condition will find the first index where digit count >= numDigits.
        while (true) {
            // Calculate log10(F_index) using Binet's formula approximation
            double logFib = index * log10Phi - log10Sqrt5;
            // Number of digits = floor(log10(F_index)) + 1
            int currentDigitCount = (int)Math.Floor(logFib) + 1;

            if (currentDigitCount >= numDigits) {
                break; // Found the first Fibonacci number with at least numDigits digits.
            }
            index++;
        }
        return index;
    }
}