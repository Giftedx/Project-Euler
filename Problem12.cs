using System;
using System.Collections;

namespace Project_Euler {
    public class Problem12 : Problem{
        public override void Solve() {
            Print(HighlyDivisibleTriangle(500));
        }

        private long HighlyDivisibleTriangle(int n) {
            ArrayList smallest = new ArrayList { 0 };
            int index = 0;
            long triangle = 0;
            while (smallest.Count < n) {
                index++;
                triangle += index;
                if(smallest.Count > 300 && triangle % 10 != 0)
                    smallest.Add(triangle);
                int divisors = DivisorCount(triangle);
                while(smallest.Count <= divisors)smallest.Add(triangle);
            }
            return (long)smallest[smallest.Count - 1];
        }

        private int DivisorCount(long n) {
            int total = 0, end = (int)Math.Sqrt(n);
            for(int i = 1; i <= end; i++) if (n % i == 0) total+= 2;
            return total;
        }
    }
}