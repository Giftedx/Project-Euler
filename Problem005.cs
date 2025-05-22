namespace Project_Euler;

public class Problem005 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 5: Smallest multiple.
    /// Finds the smallest positive number that is evenly divisible by all of the numbers from 1 to 20.
    /// </summary>
    /// <returns>The smallest positive number evenly divisible by all numbers from 1 to 20, as a string.</returns>
    public override object Solve() {
        return MinimumEvenlyDivisibleByRange(1, 20);
    }

    private string MinimumEvenlyDivisibleByRange(int min, int max) {
        ulong result = 1;
        for (int i = min; i <= max; i++) result = LeastCommonMultiple((ulong)i, result);
        return result.ToString();
    }

    private ulong LeastCommonMultiple(ulong a, ulong b) {
        // Handle cases where a or b is 0 to avoid division by zero if GCD is 0, though GCD(a,0)=a.
        // Current GCD won't return 0 if inputs are positive.
        // If a or b is 0, LCM is 0. But problem context implies positive numbers.
        // If a or b is 1, LCM is the other number.
        if (a == 0 || b == 0) return 0; // Or handle as per problem constraints (1 to 20 are positive)
        if (a == 1) return b;
        if (b == 1) return a;
        
        ulong gcd = GreatestCommonDivisor(a, b);
        // To prevent overflow, it's better to do (a / gcd) * b
        return (a / gcd) * b;
    }

    private ulong GreatestCommonDivisor(ulong a, ulong b) {
        while (b != 0) {
            ulong temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}