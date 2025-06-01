using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project_Euler
{
    [TestClass]
    public class Problem023Tests
    {
        [TestMethod]
        public void TestProblem023_Solution()
        {
            var problem = new Problem023();
            string expectedSolution = "4179871";

            var actualSolution = problem.Solve();
            Assert.AreEqual(expectedSolution, actualSolution.ToString(), $"The solution for Problem 023 is incorrect.");
        }
    }
}