// In ProjectEuler.Tests/Problem001Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem001Tests
    {
        [TestMethod]
        public void TestProblem1_Solution()
        {
            // Arrange
            var problem = new Problem001();
            var expectedSolution = 233168; // The known solution for Problem 1

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 1 is incorrect.");
        }
    }
}
