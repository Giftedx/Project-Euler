namespace Project_Euler;

public class Problem036 : Problem {
    public override void Solve() {
        Print(DoubleBasePalindromeSum(1000000));
    }

    private long DoubleBasePalindromeSum(int n) {
        long sum = 0;
        for (int i = 1; i < n; i += 2)
            if (IsDoublePalindrome(i))
                sum += i;
        return sum;
    }

    private bool IsDoublePalindrome(int n) {
        return Library.IsPalindrome(n) && IsBinaryPalindrome(n);
    }

    private bool IsBinaryPalindrome(int n) {
        int reversed = 0, temp = n;
        while (temp > 0) {
            reversed = (reversed << 1) | (temp & 1);
            temp >>= 1;
        }

        return n == reversed;
    }
}