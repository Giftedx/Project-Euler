using System.Numerics;

namespace Project_Euler;

public class Problem029 : Problem{
    public override void Solve() {
        Print(DistinctPowers(100));
    }

    private int DistinctPowers(int n) {
        HashSet<BigInteger> results = [];
        for(int a = 2; a <= n; a++)
            for(int b = 2; b <= n; b++)
                results.Add(BigInteger.Pow(a, b));
        return results.Count;
    }
}