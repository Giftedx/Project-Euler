namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 26: Reciprocal cycles.
/// Finds the value of d &lt; 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.
/// </summary>
public class Problem026 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 26: Reciprocal cycles.
    /// </summary>
    /// <returns>The value of d &lt; 1000 with longest recurring cycle.</returns>
    public override object Solve() {
        return GetLongestCycleDenominator(1000);
    }

    /// <summary>
    /// Finds d with longest cycle.
    /// </summary>
    private int GetLongestCycleDenominator(int limit) {
        int maxCycleLength = 0;
        int denominatorWithMaxCycle = 0;

        for (int d = limit - 1; d >= 2; d--) {
            if (maxCycleLength >= d) {
                break;
            }

            if (d % 2 == 0 || d % 5 == 0 || !Library.IsPrime(d)) {
                continue;
            }

            int cycleLength = GetCycleLength(d);
            if (cycleLength > maxCycleLength) {
                maxCycleLength = cycleLength;
                denominatorWithMaxCycle = d;
            }
        }
        return denominatorWithMaxCycle;
    }

    /// <summary>
    /// Calculates cycle length of 1/d.
    /// </summary>
    private int GetCycleLength(int d) {
        if (d <= 1) {
            throw new ArgumentOutOfRangeException(nameof(d), "Denominator must be greater than 1.");
        }

        int remainder = 1;
        int position = 0;

        do {
            remainder = (remainder * 10) % d;
            position++;
        } while (remainder != 1 && remainder != 0);

        return remainder == 0 ? 0 : position;
    }
}
