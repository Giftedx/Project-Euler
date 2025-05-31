using System;
using System.Numerics;

public class Test {
    public static void Solve() {
        BigInteger n = new BigInteger(12345);
        try {
            // Attempt to use GetBitLength()
            // This method exists in .NET 5+ and .NET Core 2.0+
            long bitLength = n.GetBitLength();
            Console.WriteLine("BitLength:" + bitLength);
        } catch (MissingMethodException) {
            Console.WriteLine("GetBitLength_NotAvailable");
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.GetType().Name + " - " + e.Message);
        }
    }
}
