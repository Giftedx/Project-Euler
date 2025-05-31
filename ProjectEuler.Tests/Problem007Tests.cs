// In ProjectEuler.Tests/Problem007Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem007Tests
    {
        [TestMethod]
        public void TestProblem7_Solution()
        {
            // Arrange
            var problem = new Problem007();
            object expectedSolution = 104743L; // The known solution for Problem 7

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 7 is incorrect.");
        }
    }
}
