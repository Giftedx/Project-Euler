namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 24: Lexicographic permutations.
/// Finds the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9.
/// Further details can be found at https://projecteuler.net/problem=24
/// </summary>
public class Problem024 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 24.
    /// It determines the millionth lexicographic permutation of the digits 0 through 9.
    /// </summary>
    /// <returns>A string representing the millionth lexicographic permutation.</returns>
    public override object Solve() {
        return NthLexicalPermutation(1000000); // Find the 1,000,000th permutation.
    }

    /// <summary>
    /// Finds the Nth lexicographic permutation of the digits '0' through '9'.
    /// The method uses a factorial-based approach (Factoradic number system) to determine each digit.
    /// For a set of (initially) 10 digits:
    /// 1. The (N-1)th index (0-based) is used for calculations.
    /// 2. To find the first digit: Divide (N-1) by 9!. The quotient is the index of the first digit in the sorted list of available digits.
    /// 3. The remainder becomes the new (N-1) for the next step.
    /// 4. To find the second digit: Divide the new (N-1) by 8!. The quotient is the index of the second digit in the *remaining* sorted list of available digits.
    /// 5. This process repeats until all digits are chosen.
    /// </summary>
    /// <param name="targetPermutationIndex">The 1-based index of the desired lexicographic permutation (e.g., 1 for the first, 1000000 for the millionth).</param>
    /// <returns>A string representing the Nth lexicographic permutation.</returns>
    private string NthLexicalPermutation(ulong targetPermutationIndex) {
        var availableDigits = new List<char>("0123456789".ToCharArray());
        var resultPermutation = new char[10]; // To store the resulting permutation string

        // Adjust target to be 0-indexed for calculations
        ulong currentIndex = targetPermutationIndex - 1;

        int numDigits = availableDigits.Count; // Should be 10 initially

        for (int i = 0; i < numDigits; i++) {
            int remainingDigitsToPlace = numDigits - 1 - i; // Number of digits yet to be placed after the current one. (e.g., 9 for the first digit, then 8, ...)
            ulong factorialOfRemaining = (ulong)Library.Factorial(remainingDigitsToPlace); // (n-1)! for the first digit, then (n-2)!, etc.

            // Determine the index of the digit to pick from the current list of availableDigits
            int digitIndexInList = (int)(currentIndex / factorialOfRemaining);

            // Append the chosen digit to the result
            resultPermutation[i] = availableDigits[digitIndexInList];

            // Remove the chosen digit from the list of available digits
            availableDigits.RemoveAt(digitIndexInList);

            // Update the currentIndex for the next iteration
            currentIndex %= factorialOfRemaining;
        }

        return new string(resultPermutation);
    }
}