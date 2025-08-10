using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project_Euler
{
    [TestClass]
    public class Problem014Tests
    {
        [TestMethod]
        public void TestProblem014_Solution()
        {
            var problem = new Problem014();
            string expectedSolution = "837799";

            var actualSolution = problem.Solve();
            Assert.AreEqual(expectedSolution, actualSolution.ToString(), $"The solution for Problem 014 is incorrect.");
        }
    }
}