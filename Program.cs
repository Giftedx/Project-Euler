using System.Diagnostics;

namespace Project_Euler;

internal class Program {
    public static void Main() {
        Program prog = new Program();
        ProblemSolver solver = new ProblemSolver();
        int problemCount = solver.GetProblemCount();
        do {
            Console.WriteLine("Project Euler Solver");
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
            Console.WriteLine("Enter 'a' to solve all problems.");
            Console.Write("Enter Problem to solve (1 - {0}): ", count);
            input = Console.ReadLine() ?? string.Empty;
            Console.WriteLine(" ");
            valid = ValidInput(input, count);
            if(!valid) Console.WriteLine("Invalid input");
        } while (!valid);
        return input;
    }

    private bool ValidInput(string input, int count) {
        bool numeric = int.TryParse(input, out int num);
        if(numeric)return num > 0 && num <= count;
        return input is "a" or "t";
    }

    private bool RunAgain() {
        Console.WriteLine("Type 'yes' to run program again.");
        string? input = Console.ReadLine();
        return input == "yes";
    }

    private void Test() {
        Test test = new Test();
        Stopwatch watch = Stopwatch.StartNew();
        test.Solve();
        watch.Stop();
        Console.WriteLine("{0} ms", watch.ElapsedMilliseconds);
    }
}