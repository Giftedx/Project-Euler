using System.Diagnostics;
namespace Project_Euler;

internal class Program {
    public static void Main() {
        var prog = new Program();
        var solver = new ProblemSolver();
        int problemCount = solver.GetProblemCount();
        
        do {
            Console.Clear();
            Library.FunPrint("Project Euler Solver");
            Console.WriteLine();
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
        string input;
        do {
            Library.FunPrint("Enter 'a' to solve all problems.");
            Library.FunPrint($"Enter Problem to solve (1 - {count}): ");
            input = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();
            if(!ValidInput(input, count))
                Library.FunPrint("Invalid input. Please try again.");
        } while (!ValidInput(input, count));
        return input;
    }

    private bool ValidInput(string input, int count) {
        if (int.TryParse(input, out int num)) 
            return num > 0 && num <= count;
        return input is "a" or "t";
    }

    private bool RunAgain() {
        Library.FunPrint("Press any to run program again, Space to exit.");
        var input = Console.ReadKey(intercept:true).Key;
        return input != ConsoleKey.Spacebar;
    }

    private void Test() {
        var test = new Test();
        var watch = Stopwatch.StartNew();
        test.Solve();
        watch.Stop();
        Console.WriteLine($"{watch.ElapsedMilliseconds} ms");
    }
}