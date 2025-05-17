using static Project_Euler.InputHandler;

namespace Project_Euler;

internal static class Program {
    public static void Main() {
        var solver = new ProblemSolver();
        int problemCount = solver.GetProblemCount();

        do {
            Console.Clear();
            Library.FunPrint("Project Euler Solver");
            Console.WriteLine();
            string input = GetInput(problemCount);
            switch (input) {
                case "a":
                    solver.SolveAll();
                    break;
                case "t":
                    ProblemSolver.Test();
                    break;
                default:
                    solver.SolveIndividual(input);
                    break;
            }
        } while (RunAgain());
    }
}