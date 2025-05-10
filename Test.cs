namespace Project_Euler;

public class Test {
    public void Solve() {
        Console.WriteLine(SubStringDivisiblePandigitalSum());
    }

    private readonly int[] _tests = [2, 3, 5, 7, 11, 13, 17];

    private long SubStringDivisiblePandigitalSum() {
        int[] arr = Enumerable.Range(0, 10).ToArray();
        long sum = 0;
        do {
            if (TestPerm(arr)) sum += ArrayToLong(arr);
        } while (Library.Permute(arr));

        return sum;
    }

    private bool TestPerm(int[] arr) {
        if (arr[0] == 0) return false;
        for (int i = 0; i < _tests.Length; i++) {
            if (i > 6 && arr[5] != 5) return false;
            int d = AbcToInt(arr[i + 1], arr[i + 2], arr[i + 3]);
            if (d % _tests[i] != 0) return false;
        }

        return true;
    }

    private long ArrayToLong(int[] array) {
        long result = 0;
        foreach (int i in array) result = result * 10 + i;
        return result;
    }

    private int AbcToInt(int a, int b, int c) {
        return a * 100 + b * 10 + c;
    }
}