// In ProjectEuler.Tests/Problem008Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem008Tests
    {
        [TestMethod]
        public void TestProblem8_Solution()
        {
            // Arrange
            var problem = new Problem008();
            object expectedSolution = 23514624000L; // The known solution for Problem 8

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 8 is incorrect.");
        }
    }
}
