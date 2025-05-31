// In ProjectEuler.Tests/Problem018Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem018Tests
    {
        [TestMethod]
        public void TestProblem18_Solution()
        {
            // Arrange
            var problem = new Problem018();
            object expectedSolution = 1074L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 18 is incorrect.");
        }
    }
}
