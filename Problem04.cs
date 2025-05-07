namespace Project_Euler;
public class Problem04 : Problem{
    public override void Solve() {
        Print(LargestPalindromeProduct(100, 1000));
    }

    private int LargestPalindromeProduct(int min, int max) {
        int lpp = 0;
        for (int i = min; i < max; i++) {
            for (int j = min; j < max; j++) {
                int result = i * j;
                if(result > lpp && IsPalindrome(result))
                    lpp = result;
            }
        }
        return lpp;
    }

    private bool IsPalindrome(int n) {
        int reverse = 0;
        int temp = Math.Abs(n);
        while (temp != 0) {
            reverse = (reverse * 10) + (temp % 10);
            temp /= 10;
        }
        return reverse == Math.Abs(n);
    }
}