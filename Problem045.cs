namespace Project_Euler;

public class Problem045 : Problem{
    public override void Solve() {
        Print(FindNextTphNumber(143));
    }

    private long FindNextTphNumber(int n) {
        n++; //since n is valid, and we want to find the next one.
        int i = n;
        while (true) {
            long hex = i * (2L * i - 1);
            if(Library.IsPentagon(hex)) return hex;
            i++;
        }
    }
}