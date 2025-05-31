// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 22: Names scores.
/// Calculates the total of all the name scores in the file names.txt.
/// The file contains over five-thousand first names.
/// Further details can be found at https://projecteuler.net/problem=22
/// </summary>
public class Problem022 : Problem {
    /// <summary>
    /// Stores the list of names read from the "names.txt" file.
    /// The names are expected to be in uppercase.
    /// </summary>
    private readonly List<string> _names;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem022"/> class.
    /// This constructor reads the list of names from the embedded resource "names.txt"
    /// (assuming Library.ReadFile handles this, e.g., by parsing a comma-separated string of quoted names).
    /// </summary>
    public Problem022() {
        // Library.ReadFile is expected to return a list of strings,
        // where each string is a name, presumably already stripped of quotes.
        _names = Library.ReadFile("names.txt");
    }

    /// <summary>
    /// Calculates the solution for Project Euler Problem 22.
    /// It sorts the names alphabetically, then calculates the score for each name,
    /// and finally sums all these scores.
    /// </summary>
    /// <returns>The total of all name scores in the file.</returns>
    public override object Solve() {
        return SumNameScores();
    }

    /// <summary>
    /// Calculates the total sum of the scores for all names in the <see cref="_names"/> list.
    /// The process involves:
    /// 1. Sorting the list of names alphabetically.
    /// 2. For each name in the sorted list:
    ///    a. Calculate its alphabetical value: Sum the alphabetical positions of its letters (A=1, B=2, ..., Z=26).
    ///    b. Multiply this alphabetical value by the name's 1-based position in the sorted list.
    /// 3. Summing up these scores for all names.
    /// </summary>
    /// <returns>A long integer representing the total of all name scores.</returns>
    private long SumNameScores() {
        _names.Sort(); // Sorts the list of names alphabetically.

        long totalNameScores = 0;
        for (int i = 0; i < _names.Count; i++) {
            long currentNameAlphabeticalValue = 0;
            // Calculate the alphabetical value of the current name.
            // Assumes names are uppercase. 'A' corresponds to ASCII 65.
            // 'A' - 'A' + 1 = 1, 'B' - 'A' + 1 = 2, etc.
            foreach (char letter in _names[i]) {
                currentNameAlphabeticalValue += letter - 'A' + 1;
            }

            // Multiply the alphabetical value by its position (1-based index) in the sorted list.
            long nameScore = currentNameAlphabeticalValue * (i + 1);
            totalNameScores += nameScore;
        }

        return totalNameScores;
    }
}