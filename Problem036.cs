namespace Project_Euler;

public class Problem036 : Problem{
    public override void Solve() {
        Print(DoubleBasePalindromeSum(1000000));
    }

    private long DoubleBasePalindromeSum(int n) {
        long sum = 0;
        for(int i = 1; i < n; i+=2)
            if (IsDoublePalindrome(i)) sum += i;
        return sum;
    }

    private bool IsDoublePalindrome(int n){
        return Library.IsPalindrome(n) && 
               Library.IsPalindrome(Convert.ToString(n, 2));
    }
}