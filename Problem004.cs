namespace Project_Euler;

public class Problem004 : Problem {
    public override void Solve() {
        Print(LargestPalindromeProduct(100, 1000));
    }

    private int LargestPalindromeProduct(int min, int max) {
        int lpp = 0;
        for (int i = 11; i < max; i += 11)
        for (int j = min; j < max; j++) {
            int result = i * j;
            if (result > lpp && Library.IsPalindrome(result))
                lpp = result;
        }

        return lpp;
    }
}