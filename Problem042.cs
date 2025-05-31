// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 42: Coded triangle numbers.
/// Determines how many words in a given text file (words.txt) are "triangle words".
/// A triangle word is a word whose word value is a triangle number.
/// The word value is found by converting each letter in a word to its alphabetical position and summing these values.
/// A triangle number t_n is given by the formula t_n = n(n+1)/2.
/// Further details can be found at https://projecteuler.net/problem=42
/// </summary>
public class Problem042 : Problem {
    /// <summary>
    /// Stores the list of words read from the "words.txt" file.
    /// Expected to be uppercase English words.
    /// </summary>
    private readonly List<string> _words;

    /// <summary>
    /// Initializes a new instance of the <see cref="Problem042"/> class.
    /// This constructor reads a list of words from the embedded resource "words.txt".
    /// It is assumed that <see cref="Library.ReadFile"/> handles the parsing of the file
    /// (e.g., comma-separated, quoted words) into a list of strings.
    /// </summary>
    public Problem042() {
        // Library.ReadFile is expected to return a list of strings (words).
        _words = Library.ReadFile("words.txt");
    }

    /// <summary>
    /// Calculates the solution for Project Euler Problem 42.
    /// It counts how many words in the provided list are triangle words.
    /// </summary>
    /// <returns>The total number of triangle words in the list.</returns>
    public override object Solve() {
        return CountTriangleWords(); // Renamed for clarity
    }

    /// <summary>
    /// Counts the number of triangle words in the <see cref="_words"/> list.
    /// A word is a triangle word if its "word value" is a triangle number.
    /// The word value is the sum of the alphabetical positions of its letters (A=1, B=2, ...).
    /// </summary>
    /// <returns>The count of triangle words.</returns>
    private int CountTriangleWords() { // Renamed for clarity
        return _words.Count(word => IsTriangleNumber(CalculateWordValue(word))); // Renamed for clarity
    }

    /// <summary>
    /// Calculates the "word value" of a given string.
    /// The word value is the sum of the alphabetical positions of its letters
    /// (e.g., A=1, B=2, ..., Z=26). Assumes the input string <paramref name="word"/>
    /// consists of uppercase English letters only.
    /// </summary>
    /// <param name="word">The word for which to calculate the value.</param>
    /// <returns>The calculated word value as an integer.</returns>
    private int CalculateWordValue(string word) { // Renamed for clarity
        int sum = 0;
        foreach (char c in word) {
            sum += c - 'A' + 1; // 'A' is 65. 'A'-'A'+1 = 1. 'B'-'A'+1 = 2.
        }
        return sum;
    }

    /// <summary>
    /// Determines if a given integer <paramref name="x"/> is a triangle number.
    /// A number is a triangle number if it is of the form t_n = n(n+1)/2 for some positive integer n.
    /// This method checks this by solving n(n+1)/2 = x for n:
    /// n^2 + n - 2x = 0.
    /// Using the quadratic formula, n = (-1 + sqrt(1 + 8x)) / 2.
    /// If n is a positive integer, then x is a triangle number.
    /// </summary>
    /// <param name="x">The integer to check.</param>
    /// <returns>True if <paramref name="x"/> is a triangle number; false otherwise.</returns>
    /// <remarks>The input x must be positive for a valid triangle number t_n where n >= 1.</remarks>
    private bool IsTriangleNumber(int x) { // Renamed for clarity
        if (x <= 0) return false; // Triangle numbers are positive.
        // Calculate n from x = n(n+1)/2. We need 1+8x to be a perfect square, and (-1 + sqrt(1+8x)) to be even.
        double n_float = (-1 + Math.Sqrt(1 + 8 * (double)x)) / 2.0;
        // Check if n_float is a positive integer.
        // Comparing floats for exact equality can be problematic, but here it's usually fine if n_float is very close to an integer.
        return n_float > 0 && n_float == (int)n_float;
    }
}