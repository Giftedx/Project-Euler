// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 8: Largest product in a series.
/// Further details can be found at https://projecteuler.net/problem=8
/// </summary>
public class Problem008 : Problem {
    /// <summary>
    /// The 1000-digit number provided for Project Euler Problem 8.
    /// </summary>
    private const string BigString =
        "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

    /// <summary>
    /// Solves Project Euler Problem 8: Largest product in a series.
    /// Finds the thirteen adjacent digits in the 1000-digit number that have the greatest product.
    /// </summary>
    /// <returns>The value of this product.</returns>
    public override object Solve() {
        return LargestProductString(13, BigString);
    }

    /// <summary>
    /// Finds the largest product of 'len' adjacent digits in the given 'digitString'.
    /// It iterates through the 'digitString', taking substrings of length 'len'.
    /// For each substring, it calculates the product of its digits. Digits are converted from char
    /// to int using (char & 15), which works for ASCII digits '0'-'9'.
    /// The method keeps track of the highest product found.
    /// </summary>
    /// <param name="len">The number of adjacent digits to consider for the product.</param>
    /// <param name="digitString">The string of digits to search within.</param>
    /// <returns>The largest product of 'len' adjacent digits found in the 'digitString'. Returns 0 if 'len' is invalid or no product can be formed.</returns>
    /// <exception cref="ArgumentNullException">Thrown if digitString is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if len is non-positive or greater than the length of digitString.</exception>
    private long LargestProductString(int len, string digitString) {
        if (digitString == null) throw new ArgumentNullException(nameof(digitString));
        if (len <= 0 || len > digitString.Length) throw new ArgumentOutOfRangeException(nameof(len), "Length must be positive and not exceed the string length.");

        long highestProduct = 0;
        // Iterate up to the point where the last possible substring of length 'len' can start.
        // This is digitString.Length - len.
        for (int i = 0; i <= digitString.Length - len; i++) {
            long currentProduct = 1;
            bool containsZero = false;
            for (int k = 0; k < len; k++) {
                char c = digitString[i + k];
                if (c == '0') {
                    containsZero = true;
                    break;
                }
                // Convert char digit to int. (c & 15) is a bitwise operation that works for '0'-'9'.
                // Alternatively, (c - '0') could be used.
                currentProduct *= (c & 15);
            }

            if (containsZero) {
                // If the current window contains a '0', the product is 0.
                // We can advance 'i' to skip past this '0'.
                // The next possible window starts after this '0'.
                // Example: "1230567", len=4. i=0, sub="1230", product=0.
                // Next useful i is 3 (0-indexed), to start with "0567".
                // The loop for i will naturally handle this, but we could optimize by setting i = i + k.
                // For now, simple highestProduct tracking is fine. If product is 0, it won't be highest unless highestProduct is also 0.
                if (highestProduct == 0 && currentProduct == 0) {
                    // This ensures that if all products are 0, 0 is returned.
                    // No specific handling needed here due to initialization of highestProduct = 0.
                }
            }

            if (currentProduct > highestProduct) {
                highestProduct = currentProduct;
            }
        }

        return highestProduct;
    }
}