namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 24: Lexicographic permutations.
/// Finds the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9.
/// </summary>
public class Problem024 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 24: Lexicographic permutations.
    /// </summary>
    /// <returns>The millionth lexicographic permutation.</returns>
    public override object Solve() {
        return NthLexicalPermutation(1000000);
    }

    /// <summary>
    /// Finds the Nth lexicographic permutation using factoradic system.
    /// </summary>
    private string NthLexicalPermutation(ulong targetPermutationIndex) {
        var availableDigits = new List<char>("0123456789".ToCharArray());
        var resultPermutation = new char[10];

        ulong currentIndex = targetPermutationIndex - 1;
        int numDigits = availableDigits.Count;

        for (int i = 0; i < numDigits; i++) {
            int remainingDigitsToPlace = numDigits - 1 - i;
            ulong factorialOfRemaining = (ulong)Library.Factorial(remainingDigitsToPlace);

            int digitIndexInList = (int)(currentIndex / factorialOfRemaining);
            resultPermutation[i] = availableDigits[digitIndexInList];
            availableDigits.RemoveAt(digitIndexInList);
            currentIndex %= factorialOfRemaining;
        }

        return new string(resultPermutation);
    }
}
