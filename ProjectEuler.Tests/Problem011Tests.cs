// In ProjectEuler.Tests/Problem011Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem011Tests
    {
        [TestMethod]
        public void TestProblem11_Solution()
        {
            // Arrange
            var problem = new Problem011();
            object expectedSolution = 70600674L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 11 is incorrect.");
        }
    }
}
