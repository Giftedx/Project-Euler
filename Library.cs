using System.Collections;
using System.Numerics;

// ReSharper disable UnusedMember.Global

namespace Project_Euler;

public static class Library {
    //Program-wide tasks.
    public static void FunPrint(string s, bool enableDelay = false) {
        const int wait = 10;
        foreach (char c in s) {
            Console.Write(c);
            if (enableDelay) {
                Thread.Sleep(wait);
            }
        }

        Console.WriteLine();
    }

    public static void ReadFile(string fileName, out List<string> data) {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        data = File.ReadLines(filePath)
                   .SelectMany(line => line.Split(','))
                   .Select(s => s.Trim().Replace("\"", ""))
                   .ToList();
    }

    //Maths tasks.

    public static int SumDigits(BigInteger digits) {
        BigInteger sum = 0;
        while (digits != 0) {
            var last = digits % 10;
            sum += last;
            digits /= 10;
        }

        return (int)sum;
    }

    public static int Gcd(int a, int b) {
        while (b != 0) {
            int temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static BigInteger Factorial(int n) {
        BigInteger factorial = 1;
        for (int i = 2; i <= n; i++) factorial *= i;
        return factorial;
    }

    public static int IntFactorial(int n) {
        // IntFactorial is limited to n <= 12. For larger inputs, use BigInteger Factorial(int n).
        if (n > 12) {
            throw new ArgumentOutOfRangeException(nameof(n), "Input too large for int factorial, use BigInteger Factorial(int n) instead.");
        }
        int factorial = 1;
        for (int i = 2; i <= n; i++) factorial *= i;
        return factorial;
    }

    public static int Pow10(int exp) {
        int res = 1;
        while (exp-- > 0) res *= 10;
        return res;
    }

    public static int DigitCount(int n) {
        int count = 0;
        while (n > 0) {
            count++;
            n /= 10;
        }

        return count;
    }

    //Boolean Checks

    public static bool IsPalindrome(int n) {
        int reverse = 0;
        int temp = Math.Abs(n);
        while (temp != 0) {
            reverse = reverse * 10 + temp % 10;
            temp /= 10;
        }

        return reverse == Math.Abs(n);
    }

    public static bool IsPalindrome(string s) {
        for (int i = 0; i < s.Length / 2; i++)
            if (s[i] != s[s.Length - 1 - i])
                return false;

        return true;
    }

    public static bool IsPrime(int n) {
        switch (n) {
            case <= 1:
                return false;
            case 2 or 3:
                return true;
        }

        if (n % 2 == 0) return false;
        if (n % 3 == 0) return false;

        for (int i = 5; i * i <= n; i += 6)
            if (n % i == 0 || n % (i + 2) == 0)
                return false;

        return true;
    }

    public static bool IsPentagon(long pn) {
        double n = 1.0 / 6.0 * (Math.Sqrt(24 * pn + 1) + 1);
        return n - (int)n == 0;
    }

    public static bool IsPandigital(string s) {
        if (s.Length != 9) return false;
        char[] chars = s.ToCharArray();
        Array.Sort(chars);
        bool result = new string(chars).Equals("123456789");
        return result;
    }

    /// <summary>
    /// Checks if an integer n is pandigital from 1 to 9.
    /// A pandigital number contains each of the digits from 1 to 9 exactly once, irrespective of order.
    /// This implementation assumes a 1-9 pandigital check (i.e., for a 9-digit number).
    /// It does not check for 0, and it assumes the number of digits in n should ideally be 9.
    /// </summary>
    /// <param name="n">The integer to check.</param>
    /// <returns>True if n is pandigital 1-9, false otherwise.</returns>
    public static bool IsPandigital(int n) { // Renamed from IsPanDigital
        int result = 0;
        while (n > 0) {
            int digit = n % 10;
            if (digit == 0) return false;
            result |= 1 << (digit - 1);
            n /= 10;
        }

        return result == 0x1ff;
    }

    //Array Operations

    /// <summary>
    /// Generates the next lexicographical permutation of the given integer array in place.
    /// Implements Narayana Pandita's algorithm.
    /// </summary>
    /// <param name="arr">The integer array to permute. The array is modified in place.</param>
    /// <returns>
    /// True if a next lexicographical permutation was found and applied to the array.
    /// False if the array was already the last permutation (e.g., sorted in descending order), in which case the array remains unchanged.
    /// </returns>
    public static bool Permute(int[] arr) {
        int i = arr.Length - 1;
        while (i > 0 && arr[i - 1] >= arr[i]) i--;
        if (i <= 0) return false;
        int j = arr.Length - 1;
        while (arr[j] <= arr[i - 1]) j--;
        (arr[i - 1], arr[j]) = (arr[j], arr[i - 1]);
        for (int k = arr.Length - 1; i < k; i++, k--)
            (arr[i], arr[k]) = (arr[k], arr[i]);
        return true;
    }


    public static void SieveOfEratosthenes(int n, out bool[] isPrime) {
        isPrime = new bool[n];
        if (n <= 2) return;
        Array.Fill(isPrime, true, 2, n - 2);
        int limit = (int)Math.Sqrt(n) + 1;
        for (int i = 2; i < limit; i++) {
            if (!isPrime[i]) continue;
            for (int j = i * i; j < n; j += i)
                isPrime[j] = false;
        }
    }

    public static void SieveOfEratosthenes(int n, out BitArray isPrime) {
        isPrime = new BitArray(n + 1, true); // size n+1 to use index n
        if (n < 2) {
            if (n >= 0) { // ensure valid length for BitArray
                for (int k = 0; k <= n; k++) isPrime[k] = false; // mark all false
            }
            return;
        }
        isPrime[0] = isPrime[1] = false;
        for (int p = 2; p * p <= n; p++) {
            if (isPrime[p]) {
                for (int i = p * p; i <= n; i += p)
                    isPrime[i] = false;
            }
        }
    }

    public static void GetPrimeList(IEnumerable<int> numbers, out HashSet<int> primeSet) {
        var distinctNumbers = numbers.Where(n => n >= 0).Distinct().ToList(); // Allow 0 and 1 in input, they won't be primes
        if (!distinctNumbers.Any()) {
            primeSet = new HashSet<int>();
            return;
        }

        int maxNumber = 0;
        foreach (int n in distinctNumbers) {
            if (n > maxNumber) maxNumber = n;
        }

        if (maxNumber < 2) {
            primeSet = new HashSet<int>();
            return;
        }

        // Use the BitArray sieve (which will have size maxNumber + 1)
        SieveOfEratosthenes(maxNumber, out System.Collections.BitArray isPrimeSieve);
        
        primeSet = new HashSet<int>();
        foreach (int num in distinctNumbers) {
            // num must be < isPrimeSieve.Length to be a valid index
            if (num < isPrimeSieve.Length && isPrimeSieve[num]) {
                primeSet.Add(num);
            }
        }
    }
}