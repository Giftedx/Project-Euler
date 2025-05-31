// In ProjectEuler.Tests/Problem004Tests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Euler; // Assuming the main project's namespace

namespace ProjectEuler.Tests
{
    [TestClass]
    public class Problem004Tests
    {
        [TestMethod]
        public void TestProblem4_Solution()
        {
            // Arrange
            var problem = new Problem004();
            object expectedSolution = 906609L; // The known solution for Problem 4

            // Act
            var actualSolution = problem.Solve();

            // Assert
            Assert.AreEqual(expectedSolution, actualSolution, "The solution for Problem 4 is incorrect.");
        }
    }
}
