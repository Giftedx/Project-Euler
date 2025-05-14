using System.Diagnostics;

namespace Project_Euler;

internal class Program {
    public static void Main() {
        var prog = new Program();
        var solver = new ProblemSolver();
        int problemCount = solver.GetProblemCount();
        Library.FunPrint("Project Euler Solver");
        do {
            Console.WriteLine(" ");
            string input = prog.GetInput(problemCount);
            switch (input) {
                case "a":
                    solver.SolveAll();
                    break;
                case "t":
                    prog.Test();
                    break;
                default:
                    solver.SolveIndividual(input);
                    break;
            }
        } while (prog.RunAgain());
    }

    private string GetInput(int count) {
        bool valid;
        string input;
        do {
            Library.FunPrint("Enter 'a' to solve all problems.");
            Library.FunPrint("Enter Problem to solve (1 - " + count + "): ");
            input = Console.ReadLine() ?? string.Empty;
            Console.WriteLine(" ");
            valid = ValidInput(input, count);
            if (!valid) Library.FunPrint("Invalid input");
        } while (!valid);

        return input;
    }

    private bool ValidInput(string input, int count) {
        bool numeric = int.TryParse(input, out int num);
        if (numeric) return num > 0 && num <= count;
        return input is "a" or "t";
    }

    private bool RunAgain() {
        Library.FunPrint("Press any to run program again, Space to exit.");
        var input = Console.ReadKey();
        return input.Key != ConsoleKey.Spacebar;
    }

    private void Test() {
        var test = new Test();
        var watch = Stopwatch.StartNew();
        test.Solve();
        watch.Stop();
        test = null;
        //GC.Collect(2, GCCollectionMode.Forced);
        Console.WriteLine("{0} ms", watch.ElapsedMilliseconds);
    }
}