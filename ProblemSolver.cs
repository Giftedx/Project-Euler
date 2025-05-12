using System.Collections;
using System.Diagnostics;
using static System.Reflection.Assembly;

namespace Project_Euler;

public class ProblemSolver {
    private readonly ArrayList _solvedProblems;

    public ProblemSolver() {
        GetSolvedProblems(out _solvedProblems);
    }

    public int GetProblemCount() {
        return _solvedProblems.Count;
    }

    private void GetSolvedProblems(out ArrayList solvedProblems) {
        solvedProblems = new ArrayList();
        var allTypes = GetAssembly(typeof(Problem))?.GetTypes() ?? Type.EmptyTypes;
        foreach (var type in allTypes)
            if (type.IsSubclassOf(typeof(Problem)))
                solvedProblems.Add(Activator.CreateInstance(type) as Problem);
    }

    public void SolveIndividual(string problem) {
        int n = Convert.ToInt32(problem);
        Library.FunPrint("Problem " + n + ": ");
        long time = Solve(n);
        Console.WriteLine("Solved in " + time + " ms");
    }

    public void SolveAll() {
        const string file = "log.txt";
        var fs = new FileStream(file, FileMode.Create);
        var temp = Console.Out;
        var sw = new StreamWriter(fs);
        Console.SetOut(sw);
        long timeSum = 0, slowestTime = 0;
        int slowest = 0;
        for (int i = 1; i <= _solvedProblems.Count; i++) {
            Console.Write("Problem " + i + ": ");
            long time = Solve(i);
            Console.WriteLine("Solved in " + time + " ms \n");
            timeSum += time;
            if (time <= slowestTime) continue;
            slowestTime = time;
            slowest = i;
        }

        long averageTime = timeSum / _solvedProblems.Count;
        Console.WriteLine("Solved all problems in {0} ms", timeSum);
        Console.WriteLine("Average solution time: {0} ms", averageTime);
        Console.WriteLine("Slowest solution was {0}, {1} ms", slowest, slowestTime);
        Console.SetOut(temp);
        Library.FunPrint("Results output to " + file + ", " + timeSum + " ms total");
        sw.Close();
    }

    private long Solve(int n) {
        var problem = _solvedProblems[n - 1] as Problem;
        var watch = Stopwatch.StartNew();
        problem?.Solve();
        watch.Stop();
        // ReSharper disable once RedundantAssignment
        problem = null;
        GC.Collect(2, GCCollectionMode.Optimized);
        return watch.ElapsedMilliseconds;
    }
}