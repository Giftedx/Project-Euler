// In ProjectEuler.Tests/Problem009Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem009Tests
    {
        [TestMethod]
        public void TestProblem9_Solution()
        {
            // Arrange
            var problem = new Problem009();
            object expectedSolution = 31875000L; // The known solution for Problem 9

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 9 is incorrect.");
        }
    }
}
