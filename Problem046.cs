namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 46: Goldbach's other conjecture.
/// Finds the smallest odd composite that cannot be written as the sum of a prime and twice a square.
/// </summary>
public class Problem046 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 46: Goldbach's other conjecture.
    /// </summary>
    /// <returns>The smallest counter-example.</returns>
    public override object Solve() {
        return SmallestCounterExample();
    }

    /// <summary>
    /// Finds counter-example.
    /// </summary>
    private int SmallestCounterExample() {
        int n = 9;
        while (true) {
            if (Library.IsPrime(n)) {
                n += 2;
                continue;
            }

            bool found = false;
            for (int s = 1; 2 * s * s < n; s++) {
                int p = n - 2 * s * s;
                if (Library.IsPrime(p)) {
                    found = true;
                    break;
                }
            }

            if (!found) return n;
            n += 2;
        }
    }
}
