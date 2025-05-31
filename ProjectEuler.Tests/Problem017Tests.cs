// In ProjectEuler.Tests/Problem017Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem017Tests
    {
        [TestMethod]
        public void TestProblem17_Solution()
        {
            // Arrange
            var problem = new Problem017();
            object expectedSolution = 21124L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 17 is incorrect.");
        }
    }
}
