namespace Project_Euler;

public class Problem002 : Problem {
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

    private int EvenFibSum() {
        // Even Fibonacci numbers follow the recurrence: E_n = 4 * E_{n-1} + E_{n-2}
        // Starting with E_1 = 2, E_2 = 8.
        int fib1 = 2;
        int fib2 = 8;
        int sum = fib1 + fib2;
        while (true) {
            int nextFib = 4 * fib2 + fib1;
            if (nextFib >= Limit) break;
            sum += nextFib;
            (fib1, fib2) = (fib2, nextFib);
        }

        return sum;
    }
}