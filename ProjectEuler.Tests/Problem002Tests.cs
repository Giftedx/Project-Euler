// In ProjectEuler.Tests/Problem002Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem002Tests
    {
        [TestMethod]
        public void TestProblem2_Solution()
        {
            // Arrange
            var problem = new Problem002(); // Changed from Problem001
            object expectedSolution = 4613732; // Changed to Problem 2's solution, kept as object for consistency

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 2 is incorrect."); // Updated assertion message
        }
    }
}
