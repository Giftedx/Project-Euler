// In ProjectEuler.Tests/Problem005Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem005Tests
    {
        [TestMethod]
        public void TestProblem5_Solution()
        {
            // Arrange
            var problem = new Problem005();
            object expectedSolution = 232792560L; // The known solution for Problem 5

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 5 is incorrect.");
        }
    }
}
