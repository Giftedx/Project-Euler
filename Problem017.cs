namespace Project_Euler;

public class Problem017 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 17: Number letter counts.
    /// Calculates the total number of letters used if all numbers from 1 to 1000 (one thousand) inclusive 
    /// were written out in words, following British usage for "and".
    /// </summary>
    /// <returns>The total number of letters used.</returns>
    public override object Solve() {
        return NumberLetterCount();
    }

    /// <summary>
    /// Calculates the total number of letters used to write numbers from 1 to 1000 in words.
    /// It uses a pattern-based calculation rather than converting each number individually.
    /// </summary>
    /// <returns>The total count of letters.</returns>
    private int NumberLetterCount() {
        int digits = CountLetters("onetwothreefourfivesixseveneightnine");
        int teens = CountLetters("teneleventwelvethirteenfourteenfifteen" +
                                 "sixteenseventeeneighteennineteen");
        int tens = CountLetters("twentythirtyfortyfiftysixtyseventyeightyninety");
        int hundred = CountLetters("hundred");
        int and = CountLetters("and");
        int oneThousand = CountLetters("onethousand");

        int oneToNinetyNine = digits + teens + 8 * digits + 10 * tens;
        int hundredsPart = 100 * digits;
        int andPart = 99 * 9 * and;
        int subHundredPart = 9 * oneToNinetyNine;
        int hundredText = 9 * 100 * hundred;

        return oneToNinetyNine + hundredsPart + andPart +
               subHundredPart + hundredText + oneThousand;
    }

    /// <summary>
    /// Counts the number of alphabetic characters in a given string.
    /// </summary>
    /// <param name="s">The string to count letters from.</param>
    /// <returns>The number of letters in the string.</returns>
    private static int CountLetters(string s) {
        return s.Count(char.IsLetter);
    }
}