using System.Collections.Concurrent;

namespace Project_Euler;

public class Test {
    public void Solve() {
        Console.WriteLine(DoubleBasePalindromeSum(1000000));
    }

    private long DoubleBasePalindromeSum(int n) {
        long sum = 0;
        // Loop only odd numbers to improve performance
        for (int i = 1; i < n; i += 2)
            if (IsDoublePalindrome(i)) sum += i;
        return sum;
    }

    private bool IsDoublePalindrome(int n) {
        return IsPalindrome(n) && IsBinaryPalindrome(n);
    }

    private bool IsBinaryPalindrome(int n) {
        int reversed = 0, temp = n;
        // Reverse the bits and compare
        while (temp > 0) {
            reversed = (reversed << 1) | (temp & 1); // Shift and add the least significant bit
            temp >>= 1; // Right shift the number
        }
        return n == reversed;
    }

    public static bool IsPalindrome(int n) {
        int original = Math.Abs(n);
        int reverse = 0;
        int temp = original;
        while (temp != 0) {
            reverse = reverse * 10 + temp % 10;
            temp /= 10;
        }
        return reverse == original;
    }

    public static bool IsPalindrome(string s) {
        int start = 0, end = s.Length - 1;
        while (start < end) {
            if (s[start] != s[end]) return false;
            start++;
            end--;
        }
        return true;
    }
}