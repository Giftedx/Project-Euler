namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 17: Number letter counts.
/// Counts the number of letters used to write out the numbers from 1 to 1000 inclusive.
/// </summary>
public class Problem017 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 17: Number letter counts.
    /// Counts the number of letters used to write out the numbers from 1 to 1000 inclusive.
    /// </summary>
    /// <returns>The total number of letters used.</returns>
    public override object Solve() {
        return NumberLetterCounts();
    }

    /// <summary>
    /// Calculates the total letters needed to write numbers from 1 to 1000.
    /// Counts are based on British usage (e.g., "one hundred and forty-two").
    /// </summary>
    /// <returns>Total letter count.</returns>
    private int NumberLetterCounts() {
        int[] digitsLength = { 0, 3, 3, 5, 4, 4, 3, 5, 5, 4 };
        int[] teensLength = { 3, 6, 6, 8, 8, 7, 7, 9, 8, 8 };
        int[] tensLength = { 0, 0, 6, 6, 5, 5, 5, 7, 6, 6 };

        int digits = digitsLength.Sum();
        int teens = teensLength.Sum();
        int tens = tensLength.Sum();

        int hundred = 7;
        int and = 3;
        int oneThousand = 11;

        int oneToNinetyNine = digits + teens + (tens * 10) + (digits * 8);
        int hundredsPart = digits * 100 + hundred * 900 + and * (9 * 99) + oneToNinetyNine * 9;

        return oneToNinetyNine + hundredsPart + oneThousand;
    }
}
