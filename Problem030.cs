namespace Project_Euler;

public class Problem030 : Problem{
    public override void Solve() {
        Print(SumAllFifthPowerSums());
    }

    private int SumAllFifthPowerSums() {
        int sum = 0;
        for(int i = 2; i < 200000; i++){
            if(i == FifthPowerDigitSum(i)) sum += i;
        }
        return sum;
    }

    private int FifthPowerDigitSum(int n) {
        int sum = 0;
        while(n != 0){
            int digit = n % 10;
            sum += (int) Math.Pow(digit, 5);
            n /= 10;
        }
        return sum;
    }
}