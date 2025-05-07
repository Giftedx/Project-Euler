using System.Numerics;
namespace Project_Euler;
public class Problem015 : Problem {
    public override void Solve() {
        Print(LatticePaths(20, 20));
    }

    private BigInteger LatticePaths(int i, int j) {
        return Library.Factorial(i + j) / (Library.Factorial(i) * Library.Factorial(j));
    }
}