namespace Project_Euler;

public class Problem032 : Problem{
    public override void Solve() {
        Print(SumPandigitalProducts());
    }

    private int SumPandigitalProducts() {
        HashSet<int> products = [];
        for (int a = 1; a <= 99; ++a) {
            for (int b = 100; b <= 9999; ++b) {
                int c = a * b;
                if (IsPandigital(a, b, c)) products.Add(c);
            }
        }
        return products.Sum();
    }
    
    private bool IsPandigital(int a, int b,  int c){
        string s = "" + a + b + c;
        if(s.Length != 9) return false;
        char[] chars = s.ToCharArray();
        Array.Sort(chars);
        bool result = new string(chars).Equals("123456789");
        return result;
    }
}