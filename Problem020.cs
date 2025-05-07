using System.Numerics;
namespace Project_Euler;
public class Problem020 : Problem{
    public override void Solve() {
        Print(ExponentSum());
    }

    private int ExponentSum() {
        BigInteger result = Library.Factorial(100);
        return Library.SumDigits(result);
    }
}