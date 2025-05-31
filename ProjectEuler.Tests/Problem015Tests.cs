// In ProjectEuler.Tests/Problem015Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem015Tests
    {
        [TestMethod]
        public void TestProblem15_Solution()
        {
            // Arrange
            var problem = new Problem015();
            object expectedSolution = 137846528820L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 15 is incorrect.");
        }
    }
}
