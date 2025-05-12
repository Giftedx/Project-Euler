using System.Collections;
using System.Text;

namespace Project_Euler;

public class Problem024 : Problem {
    public override void Solve() {
        Print(NthLexicalPermutation(1000000));
    }

    private string NthLexicalPermutation(int n) {
        var numbers = new ArrayList();
        n--; //initial array is the first permutation
        for (int i = 0; i < 10; i++) numbers.Add(i);
        var result = new StringBuilder();
        for (int i = numbers.Count - 1; i >= 0; i--) {
            int fact = (int)Library.Factorial(i);
            int j = n / fact;
            result.Append(numbers[j]);
            numbers.Remove(j);
            numbers.Insert(i, j);
            n %= fact;
        }

        return result.ToString();
    }
}