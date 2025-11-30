namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 8: Largest product in a series.
/// Finds the thirteen adjacent digits in the 1000-digit number that have the greatest product.
/// </summary>
public class Problem008 : Problem {
    /// <summary>
    /// The 1000-digit number as a string constant.
    /// </summary>
    private const string BigString =
        "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843" +
        "8586156078911294949545950173795833195285320880551112540698747158523863050715693290963295227443043557" +
        "6689664895044524452316173185640309871112172238311362229893423380308135336276614282806444486645238749" +
        "3035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776" +
        "6572733300105336788122023542180975125454059475224352584907711670556013604839586446706324415722155397" +
        "5369781797784617406495514929086256932197846862248283972241375657056057490261407972968652414535100474" +
        "8216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586" +
        "1786645835912456652947654568284891288314260769004224219022671055626321111109370544217506941658960408" +
        "0719840385096245544436298123098787992724428490918884580156166097919133875499200524063689912560717606" +
        "0588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

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
    /// </summary>
    /// <param name="len">The number of adjacent digits to consider for the product.</param>
    /// <param name="digitString">The string of digits to search within.</param>
    /// <returns>The largest product found.</returns>
    private long LargestProductString(int len, string digitString) {
        if (digitString == null) throw new ArgumentNullException(nameof(digitString));
        if (len <= 0 || len > digitString.Length) throw new ArgumentOutOfRangeException(nameof(len));

        long highestProduct = 0;
        for (int i = 0; i <= digitString.Length - len; i++) {
            long currentProduct = 1;
            bool containsZero = false;
            for (int k = 0; k < len; k++) {
                char c = digitString[i + k];
                if (c == '0') {
                    containsZero = true;
                    break;
                }
                currentProduct *= (c & 15);
            }

            if (!containsZero && currentProduct > highestProduct) {
                highestProduct = currentProduct;
            }
        }

        return highestProduct;
    }
}
