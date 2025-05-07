using System.Numerics;
namespace Project_Euler;
public class Problem25 : Problem{
    public override void Solve() {
        Print(FibonacciNDigits(1000));
    }

    private int FibonacciNDigits(int n) {
        int index = 1, length = 0;
        BigInteger fib1 = 0;
        BigInteger fib2 = 1;

        while(length < n){
            BigInteger sum = fib1 + fib2;
            fib1 = fib2;
            fib2 = sum;
            length = GetDigitCount(sum);
            index++;
        }
        return index;
    }

    private int GetDigitCount(BigInteger number) {
        double factor = Math.Log(2) / Math.Log(10);
        int digitCount = (int)Math.Floor(BigInteger.Log10(number) + 1);
        if (BigInteger.Pow(10, digitCount - 1) == number) {
            return digitCount - 1;
        }
        return digitCount;
    }
}