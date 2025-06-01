namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 33: Digit cancelling fractions.
/// Identifies "curious fractions" like 49/98 = 4/8, where simplifying by cancelling a common digit incorrectly
/// (e.g., cancelling the 9s) yields a correct simplification.
/// The problem asks for the denominator of the product of these four non-trivial two-digit fractions, reduced to lowest terms.
/// Further details can be found at https://projecteuler.net/problem=33
/// </summary>
public class Problem033 : Problem {
    /// <summary>
    /// Calculates the solution for Project Euler Problem 33.
    /// It finds all non-trivial two-digit digit-cancelling fractions, multiplies them together,
    /// simplifies the result, and returns the denominator.
    /// </summary>
    /// <returns>The denominator of the simplified product of the four digit-cancelling fractions.</returns>
    public override object Solve() {
        return DigitCancellingFractionsProductDenominator();
    }

    /// <summary>
    /// Finds all two-digit "curious" fractions (e.g., 49/98 = 4/8 by "cancelling" the 9s).
    /// It then computes the product of these fractions and returns the denominator of this product
    /// when reduced to its lowest common terms.
    /// A fraction is of the form num/den where 10 ≤ num < den < 100.
    /// Trivial examples (e.g., 30/50 = 3/5 by cancelling 0s) are excluded.
    /// </summary>
    /// <returns>The denominator of the product of the identified non-trivial digit-cancelling fractions, after simplification.</returns>
    private int DigitCancellingFractionsProductDenominator() {
        int overallNumeratorProduct = 1;
        int overallDenominatorProduct = 1;

        // Iterate through all possible two-digit denominators (den for D)
        for (int den = 10; den < 100; den++) {
            // Iterate through all possible two-digit numerators (num for N), where num < den
            for (int num = 10; num < den; num++) {
                int num_units = num % 10;  // Units digit of N
                int num_tens = num / 10;   // Tens digit of N
                int den_units = den % 10;  // Units digit of D
                int den_tens = den / 10;   // Tens digit of D

                // Variable to ensure we only add a fraction once if it meets criteria by more than one path (not expected for this problem)
                bool processed = false;

                // Case 1: Common digit is num_units and den_tens (e.g., XA / BX = A / B, if common digit X = num_units = den_tens)
                // The cancelled digit (num_units) must not be zero for it to be non-trivial.
                // The new denominator (den_units) must not be zero.
                if (num_units == den_tens && num_units != 0 && den_units != 0) {
                    // Simplified fraction is num_tens / den_units.
                    // Check if num_tens / den_units == num / den  =>  num_tens * den == num * den_units
                    if (num_tens * den == num * den_units) {
                        overallNumeratorProduct *= num;
                        overallDenominatorProduct *= den;
                        processed = true;
                    }
                }

                // Case 2: Common digit is num_tens and den_units (e.g., AX / YX = A / Y, if common digit X = num_tens = den_units)
                // The cancelled digit (num_tens) must not be zero (guaranteed as it's a tens digit).
                // The new denominator (den_tens) must not be zero (guaranteed as it's a tens digit).
                if (!processed && num_tens == den_units && num_tens != 0 && den_tens != 0) {
                    // Simplified fraction is num_units / den_tens.
                    // Check if num_units / den_tens == num / den  =>  num_units * den == num * den_tens
                    if (num_units * den == num * den_tens) {
                        overallNumeratorProduct *= num;
                        overallDenominatorProduct *= den;
                        // processed = true; // Not strictly needed if cases are mutually exclusive for valid curious fractions
                    }
                }
            }
        }

        // Simplify the total product by dividing by their GCD.
        int commonDivisor = Library.Gcd(overallNumeratorProduct, overallDenominatorProduct);
        return overallDenominatorProduct / commonDivisor;
    }
}