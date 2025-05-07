namespace Project_Euler;
public class Problem17 : Problem{
    public override void Solve() {
        Console.WriteLine(NumberLetterCount());
    }

    private int NumberLetterCount() {
        int digits = CountLetters("one two three four five six seven eight nine");
        int teens = CountLetters("ten eleven twelve thirteen fourteen fifteen" +
                                 "sixteen seventeen eighteen nineteen");
        int tens =  CountLetters("twenty thirty forty fifty sixty seventy" +
                                 "eighty ninety");
        int hundreds = CountLetters("hundred");
        int and =  CountLetters("and");
        int oneToNinetyNine = digits + teens + tens + 9*tens + 8*digits;
        int oneHundredToOneThousand = 100 * digits + 9 * hundreds;
        oneHundredToOneThousand+= 9*99*(hundreds+and) + 9*oneToNinetyNine;
        oneHundredToOneThousand += CountLetters("one thousand");
        return oneToNinetyNine + oneHundredToOneThousand;
    }

    private static int CountLetters(string s) {
        return s.Count(char.IsLetter);
    }
}