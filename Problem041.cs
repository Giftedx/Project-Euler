namespace Project_Euler;

public class Problem041 : Problem{
    public override void Solve() {
        Print(FindLargestPandigitalPrime());
    }

    private object FindLargestPandigitalPrime() {
        for (int n = 9; n >= 1; n--) {
            int[] digits = new int[n];
            for (int i = 1; i <= digits.Length; i++)digits[i-1] = i;
            int result = 0;
            do {
                int num = ArrayToInt(digits);
                if (Library.IsPrime(num))result = num;
            } while (Library.Permute(digits));
            if (result != 0)return result;
        }
        return 0;
    }

    private int ArrayToInt(int[] array){
        int result = 0;
        foreach(int element in array)
            result = result * 10 + element;
        return result;
    }
}