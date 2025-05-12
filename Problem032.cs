namespace Project_Euler;

public class Problem032 : Problem {
    public override void Solve() {
        Print(SumPandigitalProducts());
    }

    private int SumPandigitalProducts() {
        HashSet<int> products = [];
        for (int a = 1; a <= 99; ++a)
        for (int b = 100; b <= 9999; ++b) {
            int c = a * b;
            if (c > 9999) continue;
            if (Library.IsPandigital($"{a}{b}{c}"))
                products.Add(c);
        }

        return products.Sum();
    }
}