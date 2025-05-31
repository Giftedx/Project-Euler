// In ProjectEuler.Tests/Problem012Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem012Tests
    {
        [TestMethod]
        public void TestProblem12_Solution()
        {
            // Arrange
            var problem = new Problem012();
            object expectedSolution = 76576500L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 12 is incorrect.");
        }
    }
}
