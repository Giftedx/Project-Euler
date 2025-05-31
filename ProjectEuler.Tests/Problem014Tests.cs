// In ProjectEuler.Tests/Problem014Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem014Tests
    {
        [TestMethod]
        public void TestProblem14_Solution()
        {
            // Arrange
            var problem = new Problem014();
            object expectedSolution = 837799L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 14 is incorrect.");
        }
    }
}
