namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 46: Goldbach's other conjecture.
/// The conjecture states that every odd composite number can be written as the sum of a prime and twice a square.
/// This problem asks to find the smallest odd composite number that cannot be written in this form.
/// Further details can be found at https://projecteuler.net/problem=46
/// </summary>
public class Problem046 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 46.
    /// It finds the smallest odd composite number that is a counterexample to Goldbach's other conjecture.
    /// </summary>
    /// <returns>The smallest odd composite number that cannot be written as the sum of a prime and twice a square.</returns>
    public override object Solve() {
        return FindSmallestCounterexampleToGoldbachsOtherConjecture(); // Renamed for clarity
    }

    /// <summary>
    /// Finds the smallest odd composite number that serves as a counterexample to Goldbach's other conjecture.
    /// The conjecture is: "every odd composite number can be written as the sum of a prime and twice a square."
    /// The method starts checking odd composite numbers (beginning with 9) and increments by 2.
    /// For each number, it calls <see cref="CanBeWrittenAsPrimePlusTwiceSquare"/> to check the conjecture.
    /// The first number for which this check returns false is the answer.
    /// </summary>
    /// <returns>The smallest odd composite counterexample.</returns>
    private int FindSmallestCounterexampleToGoldbachsOtherConjecture() { // Renamed for clarity
        int numberToCheck = 9; // Smallest odd composite number.
        while (true) {
            // Check if the current odd number 'numberToCheck' satisfies the conjecture.
            // Note: The SatisfiesGoldbach method also implicitly handles primality of 'numberToCheck'.
            // If 'numberToCheck' is prime, SatisfiesGoldbach returns true, and we continue.
            // We are only interested in *composite* odd numbers failing the test.
            if (!CanBeWrittenAsPrimePlusTwiceSquare(numberToCheck)) { // Renamed for clarity
                // If it's an odd composite and does not satisfy, it's our counterexample.
                // The IsPrime check inside CanBeWrittenAsPrimePlusTwiceSquare handles filtering:
                // if numberToCheck is prime, CanBeWrittenAsPrimePlusTwiceSquare returns true.
                // So, if it returns false, numberToCheck must be an odd composite.
                if (!Library.IsPrime(numberToCheck)) { // Explicitly ensure it's composite here for clarity.
                    return numberToCheck;
                }
            }
            numberToCheck += 2; // Move to the next odd number.
        }
    }

    /// <summary>
    /// Checks if a given odd number <paramref name="n"/> can be written as the sum of a prime and twice a square (p + 2*s²).
    /// As per Goldbach's other conjecture, this should be true for all odd composite numbers.
    /// If <paramref name="n"/> is even or prime, this method returns true, as the conjecture does not apply or is trivially satisfied
    /// for the purpose of finding a counterexample among odd composites.
    /// </summary>
    /// <param name="n">The odd number to check.</param>
    /// <returns>
    /// True if <paramref name="n"/> is even, prime, or can be expressed as a prime plus twice a square.
    /// False if <paramref name="n"/> is an odd composite number that cannot be expressed in the required form.
    /// </returns>
    private bool CanBeWrittenAsPrimePlusTwiceSquare(int n) { // Renamed for clarity
        // If n is even or n is prime, it's not an odd composite, or it's a prime that trivially satisfies
        // (e.g. prime = prime + 2*0^2, though 0 is not usually considered for the square part).
        // For the search loop, these should not be counterexamples.
        if (n % 2 == 0 || Library.IsPrime(n)) {
            return true;
        }

        // Try to find a prime p such that n = p + 2*s^2, which means p = n - 2*s^2.
        // Iterate through possible values for s (s = 1, 2, 3, ...).
        // 2*s^2 must be less than n. So, s^2 < n/2, or s < sqrt(n/2).
        for (int s = 1; (2 * s * s) < n; s++) {
            int remainder = n - (2 * s * s);
            // If remainder is positive and prime, then n = prime + 2*s^2.
            if (remainder > 0 && Library.IsPrime(remainder)) {
                return true; // n satisfies the condition.
            }
        }
        // No such prime + twice_square combination found for this odd composite n.
        return false;
    }
}