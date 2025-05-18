using System.Collections.Concurrent;
using System.Diagnostics;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
// ReSharper disable RedundantAssignment
namespace Project_Euler;

public static class ProblemSolver {
    public static void IndividualBenchmark(string problem) {
        int n = Convert.ToInt32(problem);
        var data = Run(n, 1);
        Library.FunPrint($"Problem {n}: {data.Result}");
        Library.FunPrint($"Solved in {data.Times[0]:F3} ms");
    }

    public static void FullBenchmark() {
        StreamWriter sw = null;
        var testData = new TestData();
        var originalOut = Console.Out;
        var watch = Stopwatch.StartNew();

        try {
            var bag = new ConcurrentBag<ProblemData>();
            var range = Enumerable.Range(1, ProblemFactory.SolvedProblems());
            Parallel.ForEach(range, i => { bag.Add(Run(i, 100)); });
            var results = bag.OrderBy(p => p.Index).ToList();

            sw = new StreamWriter(new FileStream(TestData.File, FileMode.Create));
            Console.SetOut(sw);

            foreach (var result in results) {
                double best = result.Times.Min();
                double worst = result.Times.Max();

                testData.TotalTime += best;
                if (worst > testData.SlowestTime) {
                    testData.SlowestTime = worst;
                    testData.SlowestProblem = result.Index;
                }

                string probNumber = result.Index < 10 ? "0" + result.Index : result.Index.ToString();

                Console.WriteLine($"Problem {probNumber}: {result.Result}");
                Console.WriteLine($"    Best:   {best:F3} ms");
                Console.WriteLine($"    Worst:  {worst:F3} ms");
                Console.WriteLine($"    Avg:    {result.Times.Average():F3} ms");
                Console.WriteLine();
            }

            Console.WriteLine($"Total Time: {testData.TotalTime:F3} ms");
            Console.WriteLine($"Average solution time: {testData.TotalTime / results.Count:F3} ms");
            Console.WriteLine($"Slowest Problem: {testData.SlowestProblem} with {testData.SlowestTime:F3} ms");
        }
        finally {
            watch.Stop();
            Console.SetOut(originalOut);
            Library.FunPrint($"Results output to {TestData.File}, {watch.ElapsedMilliseconds} ms total");
            sw?.Dispose();
        }
    }

    private static ProblemData Run(int n, int runs) {
        var data = new ProblemData(n, runs);
        var problem = ProblemFactory.CreateProblem(n - 1);

        for (int i = 0; i < runs; i++) {
            var watch = Stopwatch.StartNew();

            object result = problem.Solve();
            if (i == 0) data.Result = result.ToString() ?? string.Empty;

            watch.Stop();
            data.Times.Add(watch.Elapsed.TotalMilliseconds);
        }

        return data;
    }

    public static void Test() {
        var test = new Test();
        var watch = Stopwatch.StartNew();
        test.Solve();
        watch.Stop();
        Console.WriteLine($"{watch.ElapsedMilliseconds} ms");
    }

    private struct ProblemData {
        public readonly int Index;
        public readonly List<double> Times = [];
        public string Result;

        public ProblemData(int index, int runs) {
            Result = "";
            Index = index;
            Times = new List<double>(runs);
        }
    }

    private struct TestData() {
        public const string File = "log.txt";
        public double TotalTime = 0;
        public double SlowestTime = double.MinValue;
        public int SlowestProblem = 0;
    }
}