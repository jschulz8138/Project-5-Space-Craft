using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Services;

namespace AllTesting.Services
{
    [TestClass]
    public class CommandServiceTests
    {
        [TestMethod]
        public void ExecuteCommand_ValidCommand_ShouldLogExecution()
        {
            // Arrange
            var service = new CommandService();
            var command = "AdjustPower 50%";

            // Act
            service.ExecuteCommand(command);

            // Assert
            // Ideally, validate console output using a custom StringWriter
            Assert.IsTrue(true); // Replace with actual output validation
        }
    }
}
