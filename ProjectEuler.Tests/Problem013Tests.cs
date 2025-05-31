// In ProjectEuler.Tests/Problem013Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler;

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem013Tests
    {
        [TestMethod]
        public void TestProblem13_Solution()
        {
            // Arrange
            var problem = new Problem013();
            object expectedSolution = 5537376230L;

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 13 is incorrect.");
        }
    }
}
