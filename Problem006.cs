namespace Project_Euler;

public class Problem006 : Problem {
    public override void Solve() {
        Print(SumSquareDifference(1, 100));
    }

    private long SumSquareDifference(int min, int max) {
        long sumOfSquares = 0;
        long squareOfSum = 0;
        for (int i = min; i <= max; i++) {
            sumOfSquares += (long)i * i;
            squareOfSum += i;
        }

        squareOfSum *= squareOfSum;
        return squareOfSum - sumOfSquares;
    }
}