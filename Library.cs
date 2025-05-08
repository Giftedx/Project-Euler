using System.Collections.Concurrent;
using System.Numerics;
namespace Project_Euler;

public static class Library { 
    public static List<string> ReadFile(string fileName) {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = basePath + fileName;
        string fileContent = File.ReadAllText(filePath);
        fileContent = fileContent.Replace("\"", "");
        List<string> data = fileContent.Split(',').ToList();
        return data;
    }
        
    public static int SumDigits(BigInteger digits) {
        BigInteger sum = 0;
        while (digits != 0) {
            BigInteger last =  digits % 10;
            sum += last;
            digits /= 10;
        }
        return (int)sum;
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
    
    public static bool IsPrime(int n) {
        switch (n) {
            case <= 1: return false;
            case 2:
            case 3: return true;
        }

        if(n % 2 == 0 || n % 3 == 0) return false;
        for (int i = 5; i * i <= n; i += 6) {
            if(n % i == 0 || n % (i + 2) == 0) return false;
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
    
    public static IList<int> GetPrimeList(IList<int> numbers) {
        var primeNumbers = new ConcurrentBag<int>();
        Parallel.ForEach(numbers, number => {
            if(IsPrime(number))primeNumbers.Add(number);
        });
        return primeNumbers.ToList();
    }
}