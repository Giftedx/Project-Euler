// In ProjectEuler.Tests/Problem023Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace
using System; // For Convert

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem023Tests
    {
        [TestMethod]
        public void TestProblem23_Solution()
        {
            // Arrange
            var problem = new Problem023();
            // Solution obtained by running Problem023.Solve() directly
            int expectedSolution = 4179871;

            // Act
            // The Solve() method in Problem023 returns an object, but its underlying type is int.
            var actualSolutionRaw = problem.Solve();
            int actualSolution = Convert.ToInt32(actualSolutionRaw);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 23 is incorrect.");
        }
    }
}
