using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;

namespace Project_Euler;

public static class Library {
    public static void ReadFile(string fileName, out List<string> data) {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = basePath + fileName;
        string fileContent = File.ReadAllText(filePath);
        fileContent = fileContent.Replace("\"", "");
        data = fileContent.Split(',').ToList();
    }

    public static int SumDigits(BigInteger digits) {
        BigInteger sum = 0;
        while (digits != 0) {
            BigInteger last = digits % 10;
            sum += last;
            digits /= 10;
        }

        return (int)sum;
    }

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
        for(int i = 0; i < s.Length / 2; i++) {
            if(s[i] != s[s.Length - 1 - i]) {
                return false;
            }
        }
        return true;
    }

    public static BigInteger Factorial(int n) {
        BigInteger factorial = 1;
        for (int i = 2; i <= n; i++) factorial *= i;
        return factorial;
    }

    public static int IntFactorial(int n) {
        int factorial = 1;
        for (int i = 2; i <= n; i++) factorial *= i;
        return factorial;
    }

    public static bool IsPentagon(long pn) {
        double n = 1.0/6.0 * (Math.Sqrt(24 * pn + 1) + 1);
        return n - (int)n == 0;
    }

    public static bool IsPandigital(string s) {
        if (s.Length != 9) return false;
        char[] chars = s.ToCharArray();
        Array.Sort(chars);
        bool result = new string(chars).Equals("123456789");
        return result;
    }

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

    public static bool IsPrime(int n) {
        switch (n) {
            case <= 1: return false;
            case 2:
            case 3: return true;
        }

        if (n % 2 == 0 || n % 3 == 0) return false;
        for (int i = 5; i * i <= n; i += 6) {
            if (n % i == 0 || n % (i + 2) == 0) return false;
        }

        return true;
    }

    public static void SieveOfEratosthenes(int n, out bool[] isPrime) {
        isPrime = new bool[n];
        for (int i = 0; i < isPrime.Length; i++) isPrime[i] = true;
        isPrime[0] = false;
        isPrime[1] = false;
        for (int i = 2; i < Math.Sqrt(n); i++) {
            if (!isPrime[i]) continue;
            for (int j = i * i; j < n; j += i) {
                isPrime[j] = false;
            }
        }
    }

    public static void GetPrimeList(IList<int> numbers, out HashSet<int> primeSet) {
        var primeNumbers = new ConcurrentBag<int>();
        Parallel.ForEach(numbers, number => {
            if (IsPrime(number)) primeNumbers.Add(number);
        });
        primeSet = new HashSet<int>(primeNumbers);
    }
}