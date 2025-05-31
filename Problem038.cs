using System.Text;

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 38: Pandigital multiples.
/// Finds the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated
/// product of an integer with (1, 2, ..., n) where n > 1.
/// For example, 192 × (1, 2, 3) = 192384576.
/// Further details can be found at https://projecteuler.net/problem=38
/// </summary>
public class Problem038 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 38.
    /// It searches for the largest 1 to 9 pandigital 9-digit number formed by concatenating
    /// the products of an integer with the sequence (1, 2, ..., n) where n > 1.
    /// </summary>
    /// <returns>The largest such pandigital number found.</returns>
    public override object Solve() {
        return FindLargestPandigitalMultiple(); // Renamed for clarity
    }

    /// <summary>
    /// Finds the largest 1 to 9 pandigital 9-digit number that can be formed as the
    /// concatenated product of an integer `i` with (1, 2, ..., `n`), where `n` > 1.
    ///
    /// Algorithm:
    /// - The method iterates through possible values of `n` (from 2 to 9), which is the
    ///   length of the multiplier sequence (1, 2, ..., `n`).
    /// - For each `n`, it determines an approximate upper `limit` for the integer `i`.
    ///   If `i` has `d_i` digits, the total number of digits in the concatenated product
    ///   will be roughly `n * d_i`. Since the target is a 9-digit pandigital number, `d_i`
    ///   is about `9 / n`. Thus, `i` should be less than `10^(9/n)`.
    /// - It then iterates through integers `i` from 1 up to this `limit`.
    /// - For each `(i, n)` pair, it constructs the concatenated product string:
    ///   `(i*1)(i*2)...(i*n)`.
    /// - If the length of this concatenated string is not 9, it's skipped (implicitly or explicitly).
    ///   (The current code checks pandigitality, which usually implies length 9 for 1-9 pandigital).
    /// - <see cref="Library.IsPandigital(string)"/> is used to check if the string is 1-9 pandigital.
    /// - If it is, the string is converted to an integer, and the `maxPandigital` value is updated if this new number is larger.
    /// </summary>
    /// <returns>The largest 1 to 9 pandigital number formed by such a concatenated product.</returns>
    private int FindLargestPandigitalMultiple() { // Renamed for clarity
        int maxPandigital = 0; // Initialize with 0, as any pandigital found will be larger.
                               // The problem example 192384576 is a good baseline.
                               // Smallest 1-9 pandigital is 123456789.

        // Iterate n from 2 to 9. n is the number of terms in (1, 2, ..., n).
        for (int nMultiplierSequenceLength = 2; nMultiplierSequenceLength <= 9; nMultiplierSequenceLength++) {
            // Determine an upper limit for the integer 'i'.
            // If i has D digits, concatenated product of n terms has roughly n*D digits.
            // We need n*D to be around 9. So D is approx 9/n.
            // Thus, i should be less than 10^(9/n).
            // Example: n=2, 9/n=4.5. limit=10^4=10000. i iterates up to 9999.
            // Max i for n=2: i=9876. 9876*1=9876 (4 digits), 9876*2=19752 (5 digits). Total 9.
            // Example: n=9, 9/n=1. limit=10^1=10. i iterates up to 9.
            // Max i for n=9: i=1. 1*(1..9) = 123456789 (9 digits).
            // Smallest i is 1.
            int i_limit = Library.Pow10(9 / nMultiplierSequenceLength);
            if (nMultiplierSequenceLength == 1) i_limit = Library.Pow10(9); // Should not happen as n starts at 2.

            for (int i = 1; i < i_limit; i++) {
                var concatenatedProductString = new StringBuilder();
                for (int j = 1; j <= nMultiplierSequenceLength; j++) {
                    concatenatedProductString.Append(i * j);
                }

                string currentPandigitalCandidateStr = concatenatedProductString.ToString();

                // Only proceed if the length is 9, as we are looking for a 9-digit pandigital number.
                if (currentPandigitalCandidateStr.Length == 9) {
                    if (Library.IsPandigital(currentPandigitalCandidateStr)) { // Assumes IsPandigital checks for 1-9, length 9.
                        int pandigitalNumber = Convert.ToInt32(currentPandigitalCandidateStr);
                        if (pandigitalNumber > maxPandigital) {
                            maxPandigital = pandigitalNumber;
                        }
                    }
                } else if (currentPandigitalCandidateStr.Length > 9) {
                    // If length exceeds 9, further increments of 'j' for this 'i' or 'i' itself will also exceed.
                    // So, we can break from the inner 'j' loop (or from 'i' loop if 'j' was 1).
                    // This specific 'i' with this 'nMultiplierSequenceLength' is too large.
                    if (nMultiplierSequenceLength > 1) break; // Break from 'i' loop if length too long early.
                }
            }
        }
        return maxPandigital;
    }
}