namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 49: Prime permutations.
/// Finds the other 4-digit increasing sequence.
/// </summary>
public class Problem049 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 49: Prime permutations.
    /// </summary>
    /// <returns>The 12-digit number formed by concatenating the three terms.</returns>
    public override object Solve() {
        return FindOtherSequence();
    }

    /// <summary>
    /// Finds sequence.
    /// </summary>
    private string FindOtherSequence() {
        var isPrime = Library.SieveOfEratosthenesBoolArray(10000);

        for (int i = 1000; i < 10000; i++) {
            if (!isPrime[i]) continue;
            if (i == 1487) continue;

            int j = i + 3330;
            int k = j + 3330;

            if (k < 10000 && isPrime[j] && isPrime[k]) {
                if (ArePermutations(i, j) && ArePermutations(i, k)) {
                    return $"{i}{j}{k}";
                }
            }
        }
        return "";
    }

    /// <summary>
    /// Checks if a and b are permutations.
    /// </summary>
    private bool ArePermutations(int a, int b) {
        char[] ca = a.ToString().ToCharArray();
        char[] cb = b.ToString().ToCharArray();
        Array.Sort(ca);
        Array.Sort(cb);
        return new string(ca) == new string(cb);
    }
}
