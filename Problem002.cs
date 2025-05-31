namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 2: Even Fibonacci Numbers.
/// Finds the sum of the even-valued terms in the Fibonacci sequence
/// whose values do not exceed four million.
/// </summary>
public class Problem002 : Problem {
    /// <summary>
    /// The limit for the Fibonacci sequence values.
    /// </summary>
    private const int Limit = 4000000;

    /// <summary>
    /// Solves Project Euler Problem 2: Even Fibonacci Numbers.
    /// Finds the sum of the even-valued terms in the Fibonacci sequence 
    /// whose values do not exceed four million.
    /// </summary>
    /// <returns>The sum of even-valued Fibonacci terms below four million.</returns>
    public override object Solve() {
        return EvenFibSum();
    }

    /// <summary>
    /// Calculates the sum of even-valued Fibonacci numbers that do not exceed the defined Limit.
    /// It uses the recurrence relation E_n = 4 * E_{n-1} + E_{n-2} for even Fibonacci numbers,
    /// starting with E_1 = 2 and E_2 = 8.
    /// </summary>
    /// <returns>The sum of even Fibonacci numbers up to the Limit.</returns>
    private int EvenFibSum() {
        // Even Fibonacci numbers follow the recurrence: E_n = 4 * E_{n-1} + E_{n-2}
        // Starting with E_1 = 2, E_2 = 8.
        int fib1 = 2;
        int fib2 = 8;
        int sum = fib1; // Start sum with the first even term if it's below limit

        if (fib1 >= Limit) return 0; // No terms if the first is already too large
        if (fib2 < Limit) sum += fib2; else return sum; // Add second term if within limit

        while (true) {
            int nextFib = 4 * fib2 + fib1;
            if (nextFib >= Limit) break;
            sum += nextFib;
            (fib1, fib2) = (fib2, nextFib);
        }

        return sum;
    }
}