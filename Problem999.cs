namespace Project_Euler;

/// <summary>
/// Represents a template or example problem for the Project Euler solution framework.
/// This problem class is typically used for testing the framework or as a starting point
/// for new problem implementations. It does not correspond to an official Project Euler problem.
/// </summary>
public class Problem999 : Problem {
    /// <summary>
    /// Provides an example solution. In this specific template, it calculates the sum of even numbers
    /// less than 10 (0 + 2 + 4 + 6 + 8).
    /// </summary>
    /// <returns>An object representing the result of the example calculation. In this case, an integer sum.</returns>
    public override object Solve() {
        int sum = 0;
        // Example logic: Sum even numbers from 0 up to (but not including) 10.
        // i.e., 0 + 2 + 4 + 6 + 8 = 20.
        for (int i = 0; i < 10; i += 2) {
            sum += i;
        }
        return sum;
    }
}
