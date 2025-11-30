namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 22: Names scores.
/// Calculates the total of all the name scores in the file.
/// </summary>
public class Problem022 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 22: Names scores.
    /// </summary>
    /// <returns>The total name score of the file.</returns>
    public override object Solve() {
        return CalculateNameScores();
    }

    /// <summary>
    /// Reads names, sorts them, and computes the total score.
    /// </summary>
    private long CalculateNameScores() {
        List<string> names = Library.ReadFile("names.txt");
        names.Sort();

        long totalScore = 0;
        for (int i = 0; i < names.Count; i++) {
            int nameValue = 0;
            foreach (char c in names[i]) {
                nameValue += c - 'A' + 1;
            }
            totalScore += (long)nameValue * (i + 1);
        }
        return totalScore;
    }
}
