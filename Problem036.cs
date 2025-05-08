namespace Project_Euler;

public class Problem036 : Problem{
    public override void Solve() {
        Print(DoubleBasePalindromeSum(1000000));
    }

    private long DoubleBasePalindromeSum(int n) {
        long sum = 0;
        for(int i = 1; i < n; i++)
            if (IsDoublePalindrome(i, 10, 2)) sum += i;
        return sum;
    }

    private bool IsDoublePalindrome(int n, int i, int j){
        return Library.IsPalindrome(Convert.ToString(n, i)) && 
               Library.IsPalindrome(Convert.ToString(n, j));
    }
}