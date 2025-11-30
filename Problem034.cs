namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 34: Digit factorials.
/// Finds the sum of all numbers which are equal to the sum of the factorial of their digits.
/// </summary>
public class Problem034 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 34: Digit factorials.
    /// </summary>
    /// <returns>The sum of all numbers equal to the sum of the factorial of their digits.</returns>
    public override object Solve() {
        return SumDigitFactorials();
    }

    /// <summary>
    /// Finds sum of numbers equal to sum of digit factorials.
    /// </summary>
    private int SumDigitFactorials() {
        int[] factorials = new int[10];
        for (int i = 0; i < 10; i++) factorials[i] = Library.IntFactorial(i);

        int totalSum = 0;
        for (int i = 10; i < 2540161; i++) {
            int sumOfFacts = 0;
            int temp = i;
            while (temp > 0) {
                sumOfFacts += factorials[temp % 10];
                temp /= 10;
            }

            if (sumOfFacts == i) {
                totalSum += i;
            }
        }
        return totalSum;
    }
}
