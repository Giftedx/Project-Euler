// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

namespace Project_Euler;

public class Problem008 : Problem {
    private const string BigString =
        "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

    /// <summary>
    /// Solves Project Euler Problem 8: Largest product in a series.
    /// Finds the thirteen adjacent digits in the 1000-digit number that have the greatest product.
    /// </summary>
    /// <returns>The value of the greatest product of thirteen adjacent digits.</returns>
    public override object Solve() {
        return LargestProductString(13, BigString);
    }

    private long LargestProductString(int len, string digitString) {
        long maxProduct = 0;
        int numLength = digitString.Length;

        if (len <= 0 || len > numLength) {
            // Or throw new ArgumentOutOfRangeException(nameof(len), "Length must be positive and not exceed string length.");
            return 0; // Consistent with problem context if no valid sequence exists
        }

        // Iterate from the start of the first possible window
        // to the start of the last possible window.
        // Last window starts at index: numLength - len
        for (int i = 0; i <= numLength - len; i++) {
            long currentProduct = 1;
            for (int k = 0; k < len; k++) {
                // Convert char digit to int value
                int digitValue = digitString[i + k] - '0'; 
                
                // Input validation for digitString content (optional, assumes valid digits '0'-'9')
                // if (digitValue < 0 || digitValue > 9) {
                //     throw new ArgumentException("digitString contains non-digit characters.", nameof(digitString));
                // }

                if (digitValue == 0) { 
                    currentProduct = 0; // If any digit is 0, product for this window is 0
                    break;              // Exit inner loop, currentProduct for this window is 0
                }
                currentProduct *= digitValue;
            }
            
            if (currentProduct > maxProduct) {
                maxProduct = currentProduct;
            }
        }
        return maxProduct;
    }
}