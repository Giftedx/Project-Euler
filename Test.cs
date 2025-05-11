namespace Project_Euler;

public class Test {
    public void Solve() {
        Console.WriteLine(AmicableSumBelow(10000));
    }
    
    private int AmicableSumBelow(int n) {
        int[] divis = new int[n + 1]; 
        int sum = 0; 
        for(int i = 1; i < n + 1; ++i){
            for(int j  = 2 * i; j <=n ; j+=i){  
                divis[j] += i; 
            }
        }
        for(int i = 1; i < n + 1; ++i){
            int j = divis[i]; 
            if(j>i && j<=n && divis[j] == i){
                sum += i + j ; 
            }
        }
        
        return sum;
    }
 }