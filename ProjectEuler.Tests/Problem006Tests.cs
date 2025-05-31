// In ProjectEuler.Tests/Problem006Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem006Tests
    {
        [TestMethod]
        public void TestProblem6_Solution()
        {
            // Arrange
            var problem = new Problem006();
            object expectedSolution = 25164150L; // The known solution for Problem 6

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 6 is incorrect.");
        }
    }
}
