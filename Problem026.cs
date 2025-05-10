namespace Project_Euler;

public class Problem026 : Problem{
    public override void Solve() {
        Print(LongestCycleIndex(1000));
    }

    private int LongestCycleIndex(int n) {
        int longestCycleIndex = 0, longestCycle = 0;
        for(int i = 2; i < n; i++){
            int cycle = CycleLength(i);
            if (cycle <= longestCycle) continue;
            longestCycleIndex = i;
            longestCycle = cycle;
        }
        return longestCycleIndex;
    }

    private int CycleLength(int denominator) {
        Dictionary<int, int> map = new Dictionary<int, int>();
        int mod = 1, i = 1;
        while(true){
            if(map.TryGetValue(mod, out int value)) return i - value;
            map.Add(mod, i);
            mod = mod * 10 % denominator;
            i++;
        }
    }
}