// In ProjectEuler.Tests/Problem016Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem016Tests
    {
        [TestMethod]
        public void TestProblem16_Solution()
        {
            // Arrange
            var problem = new Problem016();
            object expectedSolution = 1366L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 16 is incorrect.");
        }
    }
}
