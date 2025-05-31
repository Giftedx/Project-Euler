// In ProjectEuler.Tests/Problem050Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem050Tests
    {
        [TestMethod]
        public void TestProblem50_Solution()
        {
            // Arrange
            var problem = new Problem050();
            object expectedSolution = 997651L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 50 is incorrect.");
        }
    }
}
