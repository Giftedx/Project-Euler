// In ProjectEuler.Tests/Problem020Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem020Tests
    {
        [TestMethod]
        public void TestProblem20_Solution()
        {
            // Arrange
            var problem = new Problem020();
            object expectedSolution = 648L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 20 is incorrect.");
        }
    }
}
