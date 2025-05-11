using System.Collections;
using System.Diagnostics;
using static System.Reflection.Assembly;

namespace Project_Euler;

public class ProblemSolver {
    private readonly ArrayList _solvedProblems;

    public ProblemSolver() {
        GetSolvedProblems(out _solvedProblems);
    }
    
    public int GetProblemCount() {return _solvedProblems.Count;}
    
    private void GetSolvedProblems(out ArrayList solvedProblems) {
        solvedProblems = new ArrayList();
        Type[] allTypes = GetAssembly(typeof(Problem))?.GetTypes() ?? Type.EmptyTypes;
        foreach (Type type in allTypes)
            if (type.IsSubclassOf(typeof(Problem)))
                solvedProblems.Add(Activator.CreateInstance(type) as Problem);
    }

    public void SolveIndividual(string problem) {
        int n = Convert.ToInt32(problem);
        Console.Write("Problem {0}: ", n);
        long time = Solve(n);
        Console.WriteLine("Solved in {0} ms \n", time);
    }

    public void SolveAll() {
        const string file = "log.txt";
        FileStream fs = new FileStream(file, FileMode.Create);
        TextWriter temp = Console.Out;
        StreamWriter sw = new StreamWriter(fs);
        Console.SetOut(sw);
        long timeSum = 0, slowestTime = 0;
        int slowest = 0;
        for(int i = 1; i <= _solvedProblems.Count; i++) {
            Console.Write("Problem {0}: ", i);
            long time = Solve(i);
            Console.WriteLine("Solved in {0} ms \n", time);
            timeSum +=  time;
            if (time <= slowestTime) continue;
            slowestTime = time;
            slowest = i;
        }
        long averageTime = timeSum / _solvedProblems.Count;
        Console.WriteLine("Solved all problems in {0} ms", timeSum);
        Console.WriteLine("Average solution time: {0} ms", averageTime);
        Console.WriteLine("Slowest solution was {0}, {1} ms", slowest, slowestTime);
        Console.SetOut(temp);
        Console.WriteLine("Results output to {0}", file);
        sw.Close();
    }

    private long Solve(int n) {
        Problem? problem = _solvedProblems[n - 1] as Problem;
        Stopwatch watch = Stopwatch.StartNew();
        problem?.Solve();
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
}