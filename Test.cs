namespace Project_Euler;

public class Test {
    //private int[] _collatzChainLength;
    private const int Limit = 1000000;
    
    public Test() {}


    public void Solve() {
        Console.WriteLine(FindMinimumD());
    }

    
    private long FindMinimumD() {
        // Precompute pentagonal numbers and store them in a list
        List<long> pentagonNumbers = new List<long>();
    
        // Start the pentagonal number generation
        int n = 1;
        while (true) {
            long pn = Pentagon(n);
            pentagonNumbers.Add(pn);

            // For each pentagonal number, we check differences and sums with all earlier pentagonal numbers
            for (int i = 0; i < pentagonNumbers.Count - 1; i++) {
                long pnm = pentagonNumbers[i];
                long diff = pn - pnm;
                long sum = pn + pnm;

                // Check if both the difference and sum are pentagonal numbers
                if (IsPentagonal(diff) && IsPentagonal(sum)) {
                    return diff;  // Return the smallest difference found
                }
            }

            // Terminate early if the current pentagonal number is sufficiently large (optimization)
            if (pn > 10_000_000) {
                break;
            }

            n++;
        }

        return -1;  // If no such difference is found, which should not happen according to the problem statement
    }

    private static long Pentagon(int n) {
        return (long)n * (3L * n - 1) / 2;
    }

    private static bool IsPentagonal(long x) {
        // Using the inverse formula for pentagonal numbers: n = (1 + sqrt(1 + 24*x)) / 6
        double n = (1 + Math.Sqrt(1 + 24 * x)) / 6;
        return n == (long)n;  // Check if n is an integer
    }
}