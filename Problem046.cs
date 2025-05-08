namespace Project_Euler;

public class Problem046 : Problem {
    public override void Solve() {
        Print(DisproveGoldbach());
    }

    private int DisproveGoldbach() {
        int i = 9;
        while (true) {
            if(!SatisfiesGoldbach(i))return i;
            i += 2;
        }
    }

    private bool SatisfiesGoldbach(int n){
        if (n % 2 == 0 || Library.IsPrime(n))return true;
        for(int i = 1; i * i * 2 <= n; i++){
            if(Library.IsPrime(n - i * i * 2))return true;
        }
        return false;
    }
}