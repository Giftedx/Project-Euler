namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 17: Number letter counts.
/// Further details can be found at https://projecteuler.net/problem=17
/// </summary>
public class Problem017 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 17: Number letter counts.
    /// Calculates the total number of letters used if all the numbers from 1 to 1000 (one thousand)
    /// inclusive were written out in words. Spaces and hyphens are not counted.
    /// </summary>
    /// <returns>The total number of letters used.</returns>
    public override object Solve() {
        return NumberLetterCount();
    }

    /// <summary>
    /// Calculates the total number of letters used when writing out numbers from 1 to 1000 in words.
    /// The calculation follows British English usage (e.g., "one hundred and fifteen").
    /// It sums the letter counts for different components:
    /// - Numbers 1-9 ("one" through "nine").
    /// - Numbers 10-19 ("ten" through "nineteen").
    /// - Tens prefixes ("twenty" through "ninety").
    /// - "hundred".
    /// - "and" (used in "hundred and...").
    /// - "onethousand".
    /// The method then combines these counts based on how numbers are structured:
    /// 1. Counts letters for numbers 1-99.
    /// 2. Counts letters for the "X" in "X hundred" (e.g. "one" in "one hundred"). This occurs 100 times for each digit 1-9.
    /// 3. Counts letters for "and" which appears 99 times for each hundred-block (101-199, 201-299, etc.).
    /// 4. Counts letters for the 1-99 part that follows "X hundred and". This occurs for each of the 9 hundred-blocks.
    /// 5. Counts letters for "hundred" itself (appears 900 times for 100, 101..199, ..., 900, 901..999).
    /// 6. Adds letters for "one thousand".
    /// </summary>
    /// <returns>The total number of letters used for numbers 1 to 1000 written in words.</returns>
    private int NumberLetterCount() {
        // Letter counts for base words
        int digits = CountLetters("onetwothreefourfivesixseveneightnine"); // letters for 1,2,...,9
        int teens = CountLetters("teneleventwelvethirteenfourteenfifteen" +
                                 "sixteenseventeeneighteennineteen");    // letters for 10,11,...,19
        int tens = CountLetters("twentythirtyfortyfiftysixtyseventyeightyninety"); // letters for "twenty", "thirty",..., "ninety"
        int hundred = CountLetters("hundred"); // letters for "hundred"
        int and = CountLetters("and");       // letters for "and"
        int oneThousand = CountLetters("onethousand"); // letters for "one thousand"

        // Calculate letters for numbers 1 to 99:
        // - digits: 1-9
        // - teens: 10-19
        // - For 20-99:
        //   - The tens parts ("twenty", "thirty", ...) each appear 10 times (e.g., twenty, twentyone...twentynine). So, 10 * tens.
        //   - The units parts ("one", "two", ...) for each of these decades (21-29, 31-39,...91-99). There are 8 such decades. So, 8 * digits.
        int oneToNinetyNine = digits + teens + (10 * tens) + (8 * digits);

        // Calculate letters for numbers 100 to 999:
        // - The "X" in "X hundred": "one", "two", ..., "nine". Each of these appears 100 times.
        //   (e.g., "one" in "one hundred", "one hundred and one", ..., "one hundred and ninety-nine").
        int hundredsNamesPart = 100 * digits; // This is for the "one"..."nine" part of "X hundred..."

        // - The word "hundred": Appears for each number from 100 to 999 (900 times).
        int wordHundredPart = 900 * hundred;

        // - The word "and": Appears 99 times for each hundred-block (e.g., 101-199, 201-299, ..., 901-999). There are 9 such blocks.
        int wordAndPart = 99 * 9 * and;

        // - The numbers 1-99 that follow "X hundred and": The entire "oneToNinetyNine" sequence repeats 9 times.
        //   (e.g., for "one hundred and (1-99)", "two hundred and (1-99)", ...).
        int subHundredPart = 9 * oneToNinetyNine;

        // Total sum:
        // (Sum of 1-99) +
        // (Sum of "X" in "X hundred" for 100-999) +
        // (Sum of "hundred" for 100-999) +
        // (Sum of "and" for 100-999) +
        // (Sum of 1-99 parts for 100-999) +
        // (Sum for "one thousand")
        return oneToNinetyNine + hundredsNamesPart + wordHundredPart + wordAndPart + subHundredPart + oneThousand;
    }

    /// <summary>
    /// Counts the number of alphabetic characters in a given string.
    /// This method is used to get the letter count for number words, excluding spaces or hyphens.
    /// </summary>
    /// <param name="s">The input string, expected to be a number written in words.</param>
    /// <returns>The total count of letters in the string.</returns>
    private static int CountLetters(string s) {
        return s.Count(char.IsLetter);
    }
}