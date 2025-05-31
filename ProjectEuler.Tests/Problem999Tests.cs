// In ProjectEuler.Tests/Problem999Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem999Tests
    {
        [TestMethod]
        public void TestProblem999_Solution()
        {
            // Arrange
            var problem = new Problem999();
            object expectedSolution = 20L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 999 is incorrect.");
        }
    }
}
