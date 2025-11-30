namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 42: Coded triangle numbers.
/// Counts how many words in a file are triangle numbers.
/// </summary>
public class Problem042 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 42: Coded triangle numbers.
    /// </summary>
    /// <returns>The count of triangle words.</returns>
    public override object Solve() {
        return CountTriangleWords();
    }

    /// <summary>
    /// Counts words whose value is a triangular number.
    /// </summary>
    private int CountTriangleWords() {
        List<string> words = Library.ReadFile("words.txt");
        int count = 0;
        foreach (string word in words) {
            if (IsTriangleWord(word)) {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Checks if a word is a triangle word.
    /// </summary>
    private bool IsTriangleWord(string word) {
        int value = 0;
        foreach (char c in word) {
            value += c - 'A' + 1;
        }
        return Library.IsTriangular(value);
    }
}
