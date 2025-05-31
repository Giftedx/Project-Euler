// In ProjectEuler.Tests/Problem003Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem003Tests
    {
        [TestMethod]
        public void TestProblem3_Solution()
        {
            // Arrange
            var problem = new Problem003();
            object expectedSolution = 6857L; // The known solution for Problem 3

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 3 is incorrect.");
        }
    }
}
