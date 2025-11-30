namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 38: Pandigital multiples.
/// Finds the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated product of an integer with (1,2, ... , n) where n > 1.
/// </summary>
public class Problem038 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 38: Pandigital multiples.
    /// </summary>
    /// <returns>The largest 1-9 pandigital 9-digit number.</returns>
    public override object Solve() {
        return LargestPandigitalMultiple();
    }

    /// <summary>
    /// Finds largest pandigital multiple.
    /// </summary>
    private int LargestPandigitalMultiple() {
        int maxPandigital = 0;

        for (int x = 1; x < 10000; x++) {
            string s = "";
            int n = 1;
            while (s.Length < 9) {
                s += (x * n).ToString();
                n++;
            }

            if (s.Length == 9 && Library.IsPandigital(s)) {
                int val = int.Parse(s);
                if (val > maxPandigital) maxPandigital = val;
            }
        }
        return maxPandigital;
    }
}
