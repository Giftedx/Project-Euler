// In ProjectEuler.Tests/Problem014Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace
using System; // For Convert

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
            uint expectedSolution = 837799U; // The known solution for Problem 14

            // Act
            // The Solve() method in Problem014 returns an object, but its underlying type is uint.
            var actualSolutionRaw = problem.Solve();
            uint actualSolution = Convert.ToUInt32(actualSolutionRaw);

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 14 is incorrect.");
        }
    }
}
