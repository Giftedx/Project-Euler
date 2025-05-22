using System.Collections;
using System.Numerics;

// ReSharper disable UnusedMember.Global

namespace Project_Euler;

public static class Library {
    //Program-wide tasks.
    /// <summary>
    /// Prints a string to the console, character by character, with an optional delay.
    /// </summary>
    /// <param name="s">The string to print.</param>
    /// <param name="enableDelay">If true, a small delay is introduced after printing each character.</param>
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

    /// <summary>
    /// Reads all lines from a file, splits them by commas, trims whitespace, and removes quotation marks.
    /// Primarily designed for Project Euler data files like names.txt or words.txt.
    /// </summary>
    /// <param name="fileName">The name of the file to read. The file is expected to be in the application's base directory.</param>
    /// <returns>A list of strings extracted and processed from the file. Returns an empty list if the file is empty or lines produce no data after processing.</returns>
    /// <exception cref="System.IO.IOException">An I/O error occurs (e.g., file not found, permission issues).</exception>
    public static List<string> ReadFile(string fileName) {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        List<string> data = File.ReadLines(filePath)
                                .SelectMany(line => line.Split(','))
                                .Select(s => s.Trim().Replace("\"", ""))
                                .ToList();
        return data;
    }

    //Maths tasks.

    /// <summary>
    /// Calculates the sum of the digits of a BigInteger.
    /// </summary>
    /// <param name="digits">The BigInteger whose digits are to be summed.</param>
    /// <returns>The sum of the digits as an integer.</returns>
    public static int SumDigits(BigInteger digits) {
        BigInteger sum = 0;
        while (digits != 0) {
            var last = digits % 10;
            sum += last;
            digits /= 10;
        }

        return (int)sum;
    }

    /// <summary>
    /// Calculates the Greatest Common Divisor (GCD) of two integers using the Euclidean algorithm.
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    /// <returns>The GCD of a and b.</returns>
    public static int Gcd(int a, int b) {
        while (b != 0) {
            int temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    /// <summary>
    /// Calculates the factorial of a non-negative integer using BigInteger to handle large results.
    /// </summary>
    /// <param name="n">The non-negative integer.</param>
    /// <returns>The factorial of n as a BigInteger. Returns 1 for n=0 or n=1.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if n is negative.</exception>
    public static BigInteger Factorial(int n) {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "Factorial is not defined for negative numbers.");
        BigInteger factorial = 1;
        for (int i = 2; i <= n; i++) factorial *= i;
        return factorial;
    }

    /// <summary>
    /// Calculates the factorial of a non-negative integer. Result is an int.
    /// This method is limited to small inputs (n <= 12) due to int overflow.
    /// For larger inputs, use Factorial(int n) which returns BigInteger.
    /// </summary>
    /// <param name="n">The non-negative integer. Must be <= 12.</param>
    /// <returns>The factorial of n as an int. Returns 1 for n=0 or n=1.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if n is negative or > 12.</exception>
    public static int IntFactorial(int n) {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "Factorial is not defined for negative numbers.");
        if (n > 12) {
            throw new ArgumentOutOfRangeException(nameof(n), "Input too large for int factorial, use BigInteger Factorial(int n) instead.");
        }
        int factorial = 1;
        for (int i = 2; i <= n; i++) factorial *= i;
        return factorial;
    }

    /// <summary>
    /// Calculates 10 raised to the power of a non-negative exponent.
    /// </summary>
    /// <param name="exp">The non-negative exponent.</param>
    /// <returns>10 to the power of exp as an int.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if exp is negative (as it would result in a fraction).</exception>
    /// <remarks>This method may overflow for large exponents (e.g., exp > 9).</remarks>
    public static int Pow10(int exp) {
        if (exp < 0) throw new ArgumentOutOfRangeException(nameof(exp), "Exponent must be non-negative.");
        int res = 1;
        while (exp-- > 0) res *= 10;
        return res;
    }

    /// <summary>
    /// Counts the number of digits in a positive integer.
    /// </summary>
    /// <param name="n">The positive integer. Behavior for n=0 or negative n is not standard (returns 0).</param>
    /// <returns>The number of digits in n. Returns 0 if n is zero or negative.</returns>
    public static int DigitCount(int n) {
        if (n == 0) return 1; // Standard definition for digit count of 0 is 1.
        if (n < 0) n = -n; // Work with positive number for counting.
        int count = 0;
        while (n > 0) {
            count++;
            n /= 10;
        }

        return count;
    }

    /// <summary>
    /// Calculates the list of all positive divisors of a given number.
    /// </summary>
    /// <param name="n">The number for which to find divisors. Must be positive.</param>
    /// <returns>A list of long integers representing all positive divisors of n, sorted in ascending order.
    /// Returns {1} if n is 1. Returns an empty list if n is zero or negative (or consider throwing ArgumentOutOfRangeException for n <= 0 based on desired strictness).</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if n is less than or equal to 0.</exception>
    public static List<long> GetDivisors(long n) {
        if (n <= 0) {
            throw new ArgumentOutOfRangeException(nameof(n), "Input number must be positive.");
        }
        if (n == 1) {
            return new List<long> { 1 };
        }

        var divisors = new List<long>();
        for (long i = 1; i * i <= n; i++) {
            if (n % i == 0) {
                divisors.Add(i);
                if (i * i != n) {
                    divisors.Add(n / i);
                }
            }
        }
        divisors.Sort();
        return divisors;
    }

    /// <summary>
    /// Calculates the total number of positive divisors for a given number.
    /// </summary>
    /// <param name="number">The number for which to count divisors. Must be positive.</param>
    /// <returns>The total number of divisors of the number.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if number is zero or negative.</exception>
    public static int CountDivisors(long number) {
        if (number <= 0) {
            throw new ArgumentOutOfRangeException(nameof(number), "Number must be positive.");
        }
        if (number == 1) {
            return 1;
        }

        int count = 0;
        long root = LongIntegerSquareRoot(number); // Use existing helper for long

        for (long i = 1; i <= root; i++) {
            if (number % i == 0) {
                count += 2; // For i and number/i
            }
        }

        // If the number is a perfect square, the square root was counted twice.
        if (root * root == number) {
            count--;
        }
        return count;
    }

    /// <summary>
    /// Calculates Euler's Totient function (phi function) for a given positive integer n.
    /// φ(n) counts the number of positive integers up to n that are relatively prime to n.
    /// </summary>
    /// <param name="n">A positive integer.</param>
    /// <returns>The value of Euler's Totient function φ(n).</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if n is less than or equal to 0.</exception>
    public static long EulerPhi(long n) {
        if (n <= 0) {
            throw new ArgumentOutOfRangeException(nameof(n), "Input number must be positive.");
        }

        long result = n;
        long p = 2;
        long tempN = n;

        while (p * p <= tempN) {
            if (tempN % p == 0) {
                while (tempN % p == 0) {
                    tempN /= p;
                }
                result -= result / p;
            }
            p++;
        }

        if (tempN > 1) { // Remaining tempN is a prime factor
            result -= result / tempN;
        }

        return result;
    }

    /// <summary>
    /// Performs modular exponentiation (baseVal^exponent) % modulus.
    /// </summary>
    /// <param name="baseVal">The base value.</param>
    /// <param name="exponent">The non-negative exponent.</param>
    /// <param name="modulus">The positive modulus.</param>
    /// <returns>The result of (baseVal^exponent) % modulus.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown if exponent is negative or if modulus is less than or equal to 0.
    /// </exception>
    public static BigInteger ModPow(BigInteger baseVal, BigInteger exponent, BigInteger modulus) {
        if (modulus <= 0) {
            throw new ArgumentOutOfRangeException(nameof(modulus), "Modulus must be positive.");
        }
        if (exponent < 0) {
            throw new ArgumentOutOfRangeException(nameof(exponent), "Exponent must be non-negative.");
        }

        BigInteger res = 1;
        baseVal %= modulus; // Ensure baseVal is within modulus range

        while (exponent > 0) {
            if (exponent % 2 == 1) { // If exponent is odd
                res = (res * baseVal) % modulus;
            }
            exponent >>= 1; // exponent /= 2
            baseVal = (baseVal * baseVal) % modulus; // Square the base
        }
        return res;
    }

    /// <summary>
    /// Calculates the integer square root of a non-negative BigInteger (i.e., floor(sqrt(n))).
    /// </summary>
    /// <param name="n">The non-negative BigInteger for which to find the integer square root.</param>
    /// <returns>The integer square root of n.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if n is negative.</exception>
    public static BigInteger IntegerSquareRoot(BigInteger n) {
        if (n < 0) {
            throw new ArgumentOutOfRangeException(nameof(n), "Cannot calculate square root of a negative number.");
        }
        if (n == 0) {
            return 0;
        }

        BigInteger x;
        // Initial guess
        // For very large numbers, a guess based on bit length is much better.
        // n.GetBitLength() is not public. Approximate with n.ToByteArray().Length.
        // Each byte is 8 bits. So, bit length is roughly n.ToByteArray().Length * 8.
        // sqrt(2^k) = 2^(k/2). So, shift right by (bitLength / 2).
        // Add 1 to ensure guess is not too small, especially for small n.
        try {
            int bitLength = 0;
            // BigInteger.ToByteArray() returns an empty array for 0, but we handled n=0.
            // For small positive numbers, it might return a single byte.
            byte[] bytes = n.ToByteArray();
            if (bytes.Length == 0 && n > 0) { // Should not happen for n > 0 with standard BigInteger
                bitLength = 64; // Default for safety if byte array is unexpectedly empty
            } else {
                bitLength = bytes.Length * 8;
                // Correct for leading zero bits in the last byte if possible, though this is an approximation.
                // For instance, if the last byte is 0b00000100, bitLength is too high.
                // However, for Newton's method, a rough guess is usually fine.
                // A common heuristic: if last byte has leading zeros, subtract them.
                // This is complex to do perfectly without a GetBitLength().
                // Let's keep it simpler: use n itself or n/2 for smaller numbers if bit length is tricky.
            }

            if (bitLength > 120) { // Heuristic: for "large enough" numbers, use bit shift guess
                 x = BigInteger.One << (bitLength / 2); // Initial guess: 2^(bitLength/2)
            } else if (n > 1000000) { // Heuristic for moderately large numbers
                x = n / (BigInteger.One << 10); // n / 1024, a bit arbitrary
                 if (x == 0) x = BigInteger.One;
            }
            else {
                 x = n; // For smaller numbers, n itself or n/2 is fine.
            }
             if (x == 0) x = BigInteger.One; // Ensure guess is at least 1 for n > 0
        }
        catch (Exception) { // Fallback in case of any issue with guess estimation
            x = n;
            if (x == 0 && n > 0) x = BigInteger.One;
        }


        BigInteger y = (x + n / x) / 2;
        // Loop until y >= x. This is a standard termination for integer Newton's method.
        // When y >= x, x is either floor(sqrt(n)) or floor(sqrt(n))+1.
        while (y < x) {
            x = y;
            y = (x + n / x) / 2;
        }

        // After the loop, x is the smallest integer such that x >= sqrt(n).
        // So, if x*x > n, then (x-1) is the true floor(sqrt(n)).
        // Otherwise, x is floor(sqrt(n)).
        if (x * x > n) {
            return x - 1;
        }
        return x;
    }

    #region Combinatorics

    /// <summary>
    /// Calculates the number of combinations (nCk, "n choose k"), which is the number of ways 
    /// to choose k items from a set of n items without regard to the order of selection.
    /// Uses the formula nCk = n! / (k! * (n-k)!).
    /// </summary>
    /// <param name="n">The total number of items in the set. Must be non-negative.</param>
    /// <param name="k">The number of items to choose. Must be non-negative and not greater than n.</param>
    /// <returns>The number of combinations (nCk) as a BigInteger. 
    /// Returns 0 if k < 0, k > n.
    /// Returns 1 if k == 0 or k == n.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if n is negative.</exception>
    public static BigInteger Combinations(int n, int k) {
        if (n < 0) {
            throw new ArgumentOutOfRangeException(nameof(n), "Total number of items (n) must be non-negative.");
        }
        if (k < 0 || k > n) {
            return 0; // Or throw ArgumentOutOfRangeException if strictness is required for k.
        }
        if (k == 0 || k == n) {
            return 1;
        }
        // Optimization: C(n, k) = C(n, n-k). Choose smaller k.
        if (k > n / 2) {
            k = n - k;
        }

        BigInteger result = 1;
        for (int i = 1; i <= k; i++) {
            // result = result * (n - k + i) / i; // Alternative form
            result = result * (n - i + 1) / i; // Iterative calculation: C(n,k) = (n/1) * ((n-1)/2) * ... * ((n-k+1)/k)
        }
        return result;
    }

    /// <summary>
    /// Calculates the number of partial permutations (nPk), which is the number of ways 
    /// to choose and arrange k items from a set of n items where the order of selection matters.
    /// Uses the formula nPk = n! / (n-k)!.
    /// </summary>
    /// <param name="n">The total number of items in the set. Must be non-negative.</param>
    /// <param name="k">The number of items to choose and arrange. Must be non-negative and not greater than n.</param>
    /// <returns>The number of partial permutations (nPk) as a BigInteger.
    /// Returns 0 if k < 0 or k > n.
    /// Returns 1 if k == 0.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if n is negative.</exception>
    public static BigInteger Permutations(int n, int k) {
        if (n < 0) {
            throw new ArgumentOutOfRangeException(nameof(n), "Total number of items (n) must be non-negative.");
        }
        if (k < 0 || k > n) {
            return 0; // Or throw ArgumentOutOfRangeException if strictness is required for k.
        }
        if (k == 0) {
            return 1;
        }

        BigInteger result = 1;
        for (int i = 0; i < k; i++) {
            result *= (n - i);
        }
        return result;
    }

    #endregion Combinatorics

    #region Figurate Number Checks

    /// <summary>
    /// Calculates the integer square root of a non-negative long integer (i.e., floor(sqrt(n))).
    /// Uses Newton's method for refinement if needed.
    /// </summary>
    /// <param name="n">The non-negative long integer.</param>
    /// <returns>The integer square root of n.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if n is negative.</exception>
    private static long LongIntegerSquareRoot(long n) {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "Cannot calculate square root of a negative number.");
        if (n == 0) return 0;
        
        long root;
        // Initial guess using Math.Sqrt, then refine with Newton's method
        // This is generally good for `long` as Math.Sqrt provides a double precision float.
        // For perfect square checks, the subsequent multiplication handles most precision issues.
        // Newton's method step is added for robustness, especially if n is very large.
        if (n > (1L << 50)) { // Heuristic: for very large n, Math.Sqrt might be less precise for integer root
            root = (long)Library.IntegerSquareRoot(new BigInteger(n)); // Fallback to BigInteger version for very large longs
        } else {
            root = (long)Math.Sqrt(n);
        }

        // One or two steps of Newton's method can refine the integer root if Math.Sqrt was slightly off for large longs.
        // (x + n/x)/2
        // Ensure no division by zero if root is 0 from Math.Sqrt (e.g. n=0, already handled)
        // or if n/root truncates significantly.
        if (root == 0 && n > 0) root = 1; // Should not happen if n > 0 for Math.Sqrt

        long nextRoot = (root + n / root) / 2;
        if (nextRoot < root) { // Check if refinement is moving towards a smaller root
             root = nextRoot;
             // Optionally, a second step: root = (root + n / root) / 2;
        }
       
        // Final check: the result 'root' must satisfy root*root <= n.
        // If (root+1)*(root+1) is also <= n, then root is too small.
        // If root*root > n, root is too large.
        // Iteratively adjust if needed (usually one step is enough after Math.Sqrt)
        while (root * root > n) {
            root--;
        }
        while ((root + 1) * (root + 1) <= n) {
            root++;
        }
        return root;
    }

    /// <summary>
    /// Checks if a number is a perfect square.
    /// </summary>
    /// <param name="number">The number to check. Must be non-negative.</param>
    /// <returns>True if the number is a perfect square, false otherwise.</returns>
    /// <remarks>Returns false for negative numbers.</remarks>
    public static bool IsSquare(long number) {
        if (number < 0) return false;
        if (number == 0) return true; // 0 is considered a square of 0.
        long root = LongIntegerSquareRoot(number);
        return root * root == number;
    }

    /// <summary>
    /// Checks if a number is a triangular number.
    /// Triangular numbers are generated by the formula T_k = k(k+1)/2.
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>True if the number is triangular, false otherwise.</returns>
    /// <remarks>Returns false for non-positive numbers as triangular numbers are positive (except T0=0, handled if needed by context).</remarks>
    public static bool IsTriangular(long number) {
        if (number < 0) return false;
        if (number == 0) return true; // T_0 = 0. Depending on context, 0 might be considered triangular.
        // We need to solve k(k+1)/2 = number  => k^2 + k - 2*number = 0
        // For k to be a positive integer, 1 + 8*number must be a perfect square.
        // Let 1 + 8*number = m^2. Then k = (-1 + m) / 2.
        // So, m must be odd and m > 1 for k to be a positive integer.
        long discriminant = 1 + 8 * number;
        if (discriminant < 0 || !IsSquare(discriminant)) {
            return false;
        }
        long m = LongIntegerSquareRoot(discriminant);
        // k = (-1 + m) / 2. For k to be an integer, (-1+m) must be even, so m must be odd.
        // For k to be positive, m > 1.
        return (m > 1) && ((m - 1) % 2 == 0);
    }
    
    /// <summary>
    /// Checks if a number is a pentagonal number.
    /// Pentagonal numbers are generated by the formula Pk = k(3k-1)/2.
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>True if number is a pentagonal number, false otherwise.</returns>
    /// <remarks>Returns false for non-positive numbers as pentagonal numbers are positive (P0=0, P1=1).</remarks>
    public static bool IsPentagonal(long number) {
        if (number < 0) return false;
        if (number == 0) return true; // P_0 = 0. 
        // We need to solve k(3k-1)/2 = number => 3k^2 - k - 2*number = 0
        // For k to be a positive integer, 1 + 24*number must be a perfect square.
        // Let 1 + 24*number = m^2. Then k = (1 + m) / 6.
        // So, m must exist, and (1+m) must be divisible by 6.
        // Also m = sqrt(1+24*number) must be of the form 6k-1.
        long discriminant = 1 + 24 * number;
        if (discriminant < 0 || !IsSquare(discriminant)) {
            return false;
        }
        long m = LongIntegerSquareRoot(discriminant);
        // k = (1 + m) / 6. For k to be a positive integer, (1+m) must be divisible by 6.
        // And 1+m must be > 0. Since m >= 0, this is true.
        return (1 + m) % 6 == 0;
    }

    /// <summary>
    /// Checks if a number is a hexagonal number.
    /// Hexagonal numbers are generated by the formula Hk = k(2k-1).
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>True if the number is hexagonal, false otherwise.</returns>
    /// <remarks>Returns false for non-positive numbers (H0=0, H1=1).</remarks>
    public static bool IsHexagonal(long number) {
        if (number < 0) return false;
        if (number == 0) return true; // H_0 = 0.
        // We need to solve k(2k-1) = number => 2k^2 - k - number = 0
        // For k to be a positive integer, 1 + 8*number must be a perfect square.
        // Let 1 + 8*number = m^2. Then k = (1 + m) / 4.
        // So, m must exist, and (1+m) must be divisible by 4.
        long discriminant = 1 + 8 * number;
        if (discriminant < 0 || !IsSquare(discriminant)) {
            return false;
        }
        long m = LongIntegerSquareRoot(discriminant);
        // k = (1 + m) / 4. For k to be a positive integer, (1+m) must be divisible by 4.
        return (1 + m) % 4 == 0;
    }

    /// <summary>
    /// Checks if a number is a heptagonal number.
    /// Heptagonal numbers are generated by the formula Pk,7 = k(5k-3)/2.
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>True if the number is heptagonal, false otherwise.</returns>
    /// <remarks>Returns false for non-positive numbers (P0,7=0, P1,7=1).</remarks>
    public static bool IsHeptagonal(long number) {
        if (number < 0) return false;
        if (number == 0) return true; // P_0,7 = 0.
        // We need to solve k(5k-3)/2 = number => 5k^2 - 3k - 2*number = 0
        // For k to be a positive integer, 9 + 40*number must be a perfect square.
        // Let 9 + 40*number = m^2. Then k = (3 + m) / 10.
        // So, m must exist, and (3+m) must be divisible by 10.
        long discriminant = 9 + 40 * number;
        if (discriminant < 0 || !IsSquare(discriminant)) {
            return false;
        }
        long m = LongIntegerSquareRoot(discriminant);
        // k = (3 + m) / 10. For k to be a positive integer, (3+m) must be divisible by 10.
        return (3 + m) % 10 == 0;
    }

    /// <summary>
    /// Checks if a number is an octagonal number.
    /// Octagonal numbers are generated by the formula Pk,8 = k(3k-2).
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>True if the number is octagonal, false otherwise.</returns>
    /// <remarks>Returns false for non-positive numbers (P0,8=0, P1,8=1).</remarks>
    public static bool IsOctagonal(long number) {
        if (number < 0) return false;
        if (number == 0) return true; // P_0,8 = 0.
        // We need to solve k(3k-2) = number => 3k^2 - 2k - number = 0
        // For k to be a positive integer, 4 + 12*number must be a perfect square.
        // (This is equivalent to 1 + 3*number being a perfect square, if you divide by 4: (2+sqrt(4(1+3N)))/6 = (2+2sqrt(1+3N))/6 = (1+sqrt(1+3N))/3)
        // Let 4 + 12*number = m^2. Then k = (2 + m) / 6.
        // So, m must exist, and (2+m) must be divisible by 6.
        long discriminant = 4 + 12 * number;
        if (discriminant < 0 || !IsSquare(discriminant)) {
            return false;
        }
        long m = LongIntegerSquareRoot(discriminant);
        // k = (2 + m) / 6. For k to be a positive integer, (2+m) must be divisible by 6.
        return (2 + m) % 6 == 0;
    }

    #endregion Figurate Number Checks

    //Boolean Checks

    /// <summary>
    /// Checks if an integer is a palindrome.
    /// A number is a palindrome if it reads the same forwards and backward (e.g., 121, 55, 9009).
    /// Negative signs are ignored; the check is performed on the absolute value.
    /// </summary>
    /// <param name="n">The integer to check.</param>
    /// <returns>True if the absolute value of n is a palindrome, false otherwise.</returns>
    public static bool IsPalindrome(int n) {
        if (n == int.MinValue) return false; // Math.Abs(int.MinValue) overflows
        int absN = Math.Abs(n);
        int reverse = 0;
        int temp = absN;
        while (temp != 0) {
            reverse = reverse * 10 + temp % 10;
            temp /= 10;
        }
        return reverse == absN;
    }

    /// <summary>
    /// Checks if a string is a palindrome.
    /// A string is a palindrome if it reads the same forwards and backward (e.g., "madam", "racecar").
    /// The check is case-sensitive.
    /// </summary>
    /// <param name="s">The string to check. Can be null or empty.</param>
    /// <returns>True if the string is a palindrome, false otherwise. Returns true for null or empty strings or single-character strings.</returns>
    public static bool IsPalindrome(string s) {
        if (string.IsNullOrEmpty(s) || s.Length == 1) return true;
        for (int i = 0; i < s.Length / 2; i++)
            if (s[i] != s[s.Length - 1 - i])
                return false;

        return true;
    }

    /// <summary>
    /// Checks if an integer is a prime number.
    /// A prime number is a natural number greater than 1 that has no positive divisors other than 1 and itself.
    /// </summary>
    /// <param name="n">The integer to check.</param>
    /// <returns>True if n is prime, false otherwise.</returns>
    public static bool IsPrime(int n) {
        switch (n) {
            case <= 1:
                return false;
            case 2 or 3:
                return true;
        }

        if (n % 2 == 0) return false;
        if (n % 3 == 0) return false;

        // Check from 5 onwards, stepping by 6 (i.e., 5, 11, 17, ...)
        // Numbers not divisible by 2 or 3 can be written as 6k ± 1.
        for (int i = 5; i * i <= n; i += 6)
            if (n % i == 0 || n % (i + 2) == 0) // Check i and i+2 (e.g., 5 and 7)
                return false;

        return true;
    }

    /// <summary>
    // IsPentagon was moved into the Figurate Number Checks region.
    // Its old location is removed by this diff.
    /// Checks if a string is pandigital from 1 to 9.
    /// The string must be exactly 9 characters long and contain each digit from '1' to '9' exactly once.
    /// </summary>
    /// <param name="s">The string to check.</param>
    /// <returns>True if the string is 1-9 pandigital, false otherwise.</returns>
    public static bool IsPandigital(string s) {
        if (s.Length != 9) return false;
        int mask = 0;
        foreach (char c in s) {
            if (c < '1' || c > '9') return false; // Not a digit 1-9
            int digit = c - '0';
            int bit = 1 << (digit - 1);
            if ((mask & bit) != 0) return false; // Digit already seen
            mask |= bit;
        }
        return mask == 0x1FF; // 0x1FF = 111111111 in binary, represents digits 1-9
    }

    /// <summary>
    /// Checks if an integer is pandigital from 1 up to a certain number of digits (implicitly, the number of digits in n).
    /// For example, if n is 123, it checks if it's 1-3 pandigital. If n is 2143, it checks if it's 1-4 pandigital.
    /// Digits must be non-zero.
    /// </summary>
    /// <param name="n">The integer to check. Must be positive.</param>
    /// <returns>True if n is pandigital for its number of digits (e.g., a 4-digit number is 1-4 pandigital), false otherwise.
    /// Returns false if n contains a zero digit, or if n is negative or zero.</returns>
    public static bool IsPandigital(int n) {
        if (n <= 0) return false;

        int numDigits = 0;
        int tempN = n;
        int bitmask = 0;

        while (tempN > 0) {
            int digit = tempN % 10;
            if (digit == 0) return false; // Pandigital 1-k does not include 0

            int bit = 1 << (digit - 1);
            if ((bitmask & bit) != 0) return false; // Repeated digit
            
            bitmask |= bit;
            numDigits++;
            tempN /= 10;
        }

        // Check if all digits from 1 to numDigits are present
        // e.g., if numDigits is 3, expectedMask is (1 << 3) - 1 = 0b111
        // e.g., if numDigits is 9, expectedMask is (1 << 9) - 1 = 0b111111111 (0x1FF)
        int expectedMask = (1 << numDigits) - 1;
        return bitmask == expectedMask;
    }

    //Array Operations

    /// <summary>
    /// Generates the next lexicographical permutation of the given integer array in place.
    /// Implements Narayana Pandita's algorithm.
    /// </summary>
    /// <param name="arr">The integer array to permute. The array is modified in place. Cannot be null.
    /// The elements are expected to be unique for typical lexicographical permutation behavior,
    /// though the algorithm works with duplicates (but might not produce unique permutations then).</param>
    /// <returns>
    /// True if a next lexicographical permutation was found and applied to the array.
    /// False if the array was already the last permutation (e.g., sorted in descending order), 
    /// in which case the array is reversed to its first permutation (e.g., sorted in ascending order).
    /// </returns>
    /// <exception cref="System.ArgumentNullException">Thrown if arr is null.</exception>
    public static bool Permute(int[] arr) {
        if (arr == null) throw new ArgumentNullException(nameof(arr));
        int i = arr.Length - 1;
        while (i > 0 && arr[i - 1] >= arr[i]) i--;
        if (i <= 0) {
            // Array is the last permutation (sorted in descending order).
            // Reverse it to the first permutation (sorted in ascending order).
            Array.Reverse(arr);
            return false;
        }

        // Find the smallest element to the right of arr[i-1] that is greater than arr[i-1]
        int j = arr.Length - 1;
        while (arr[j] <= arr[i - 1]) j--;
        
        // Swap arr[i-1] and arr[j]
        (arr[i - 1], arr[j]) = (arr[j], arr[i - 1]);

        // Reverse the suffix starting at arr[i]
        Array.Reverse(arr, i, arr.Length - i);
        return true;
    }

    /// <summary>
    /// Generates a boolean array indicating primality up to a specified limit using the Sieve of Eratosthenes.
    /// </summary>
    /// <param name="limit">The inclusive upper bound for the sieve. Must be non-negative.</param>
    /// <returns>A boolean array `isPrime` of size `limit + 1` where `isPrime[i]` is true if `i` is prime, and false otherwise.
    /// Returns an array representing no primes if limit is less than 2.</returns>
    public static bool[] SieveOfEratosthenesBoolArray(int limit) {
        if (limit < 0) throw new ArgumentOutOfRangeException(nameof(limit), "Limit must be non-negative.");
        bool[] isPrime = new bool[limit + 1];
        if (limit < 2) return isPrime; // All false by default, which is correct for 0, 1

        for (int i = 2; i <= limit; i++) isPrime[i] = true;

        for (int p = 2; p * p <= limit; p++) {
            if (isPrime[p]) {
                for (int i = p * p; i <= limit; i += p)
                    isPrime[i] = false;
            }
        }
        return isPrime;
    }

    /// <summary>
    /// Generates a BitArray indicating primality up to a specified limit using the Sieve of Eratosthenes.
    /// This version is more memory-efficient for large limits than SieveOfEratosthenesBoolArray.
    /// </summary>
    /// <param name="limit">The inclusive upper bound for the sieve. Must be non-negative.</param>
    /// <returns>A BitArray `isPrime` of size `limit + 1` where `isPrime[i]` is true if `i` is prime, and false otherwise.
    /// Returns a BitArray representing no primes if limit is less than 2.</returns>
    public static BitArray SieveOfEratosthenesBitArray(int limit) {
        if (limit < 0) throw new ArgumentOutOfRangeException(nameof(limit), "Limit must be non-negative.");
        BitArray isPrime = new BitArray(limit + 1, true); // size limit + 1 to use index limit

        if (limit < 2) {
            // Mark all as false if limit is less than 2
            // For limit = 0, size is 1. For limit = 1, size is 2.
            for (int k = 0; k <= limit; k++) isPrime[k] = false;
            return isPrime;
        }

        isPrime[0] = false;
        isPrime[1] = false;

        for (int p = 2; p * p <= limit; p++) {
            if (isPrime[p]) {
                for (int i = p * p; i <= limit; i += p)
                    isPrime[i] = false;
            }
        }
        return isPrime;
    }

    /// <summary>
    /// Filters a collection of numbers and returns a set containing only those that are prime.
    /// Uses SieveOfEratosthenesBitArray internally to determine primality efficiently.
    /// </summary>
    /// <param name="numbers">An enumerable collection of integers to check for primality.</param>
    /// <returns>A HashSet containing all unique prime numbers from the input collection.
    /// Numbers less than 2 are ignored. Duplicate numbers in the input are processed once.</returns>
    public static HashSet<int> GetPrimesFromNumbers(IEnumerable<int> numbers) {
        var distinctNumbers = numbers.Where(n => n >= 2).Distinct().ToList();
        if (!distinctNumbers.Any()) {
            return new HashSet<int>();
        }

        int maxNumber = 0;
        // Find the maximum number in the list to set the sieve limit.
        // This loop is necessary because LINQ Max() on an empty collection throws.
        foreach (int n in distinctNumbers) {
            if (n > maxNumber) maxNumber = n;
        }
        // If maxNumber is still less than 2 (e.g., input was {2}), Sieve will handle it.
        // However, if distinctNumbers was not empty, maxNumber must be >= 2.

        BitArray isPrimeSieve = SieveOfEratosthenesBitArray(maxNumber);
        
        var primeSet = new HashSet<int>();
        foreach (int num in distinctNumbers) {
            // num is already >= 2 here.
            // isPrimeSieve is of size maxNumber + 1. num is <= maxNumber.
            if (isPrimeSieve[num]) {
                primeSet.Add(num);
            }
        }
        return primeSet;
    }

    /// <summary>
    /// Generates a list of all prime numbers up to a specified limit using the Sieve of Eratosthenes.
    /// </summary>
    /// <param name="limit">The inclusive upper bound for generating primes. Must be non-negative.</param>
    /// <returns>A list of integers containing all prime numbers from 2 up to the limit.
    /// Returns an empty list if the limit is less than 2.</returns>
    public static List<int> SievePrimesList(int limit) {
        if (limit < 2) {
            return new List<int>();
        }
        BitArray isPrimeSieve = SieveOfEratosthenesBitArray(limit);
        var primes = new List<int>();
        for (int i = 2; i <= limit; i++) {
            if (isPrimeSieve[i]) {
                primes.Add(i);
            }
        }
        return primes;
    }
}
