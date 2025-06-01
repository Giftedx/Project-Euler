using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project_Euler
{
    [TestClass]
    public class Problem010Tests
    {
        [TestMethod]
        public void TestProblem010_Solution()
        {
            var problem = new Problem010();
            string expectedSolution = "142913828922";

            var actualSolution = problem.Solve();
            Assert.AreEqual(expectedSolution, actualSolution.ToString(), $"The solution for Problem 010 is incorrect.");
        }
    }
}