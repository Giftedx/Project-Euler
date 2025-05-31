// In ProjectEuler.Tests/Problem010Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem010Tests
    {
        [TestMethod]
        public void TestProblem10_Solution()
        {
            // Arrange
            var problem = new Problem010();
            long expectedSolution = 142913828922L; // The known solution for Problem 10

            // Act
            // The Solve() method in Problem010 returns an object, but its underlying type is long.
            var actualSolutionRaw = problem.Solve();
            long actualSolution = Convert.ToInt64(actualSolutionRaw);


            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 10 is incorrect.");
        }
    }
}
