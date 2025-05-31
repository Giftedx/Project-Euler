// In ProjectEuler.Tests/Problem019Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem019Tests
    {
        [TestMethod]
        public void TestProblem19_Solution()
        {
            // Arrange
            var problem = new Problem019();
            object expectedSolution = 171L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 19 is incorrect.");
        }
    }
}
