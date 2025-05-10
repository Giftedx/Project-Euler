namespace Project_Euler;

public class Problem050 : Problem{
    public override void Solve() {
        Print(ConsecutivePrimeSumBelow(1000000));
    }

    private long ConsecutivePrimeSumBelow(int n){
        Library.SieveOfEratosthenes(n, out bool[] isPrime);
        List<int> primeList = [];
        for (int index = 0; index < isPrime.Length; index++) {
            if(isPrime[index])primeList.Add(index);
        }
        
        long maxSum = 0;
        int maxRun = 0;
        for(int i = 0; i < primeList.Count; i++){
            int sum = 0;
            for(int j = i; j < primeList.Count; j++){
                sum += primeList[j];
                if(sum > n) {
                    break;
                }

                if (j - i <= maxRun || 
                    sum <= maxSum || 
                    !isPrime[sum]) continue;
                maxSum = sum;
                maxRun = j-i;
            }
        }
        return maxSum;
    }
}