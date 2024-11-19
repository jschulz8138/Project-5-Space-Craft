using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Services;
using CAndD.Models;

namespace CnD_Test
{
    [TestClass]
    public class ServicesTests
    {
        [TestMethod]
        public void CommandService_ExecuteCommand_ShouldPrintExecutionMessage()
        {
            // Arrange
            var service = new CommandService();
            string command = "AdjustPower";

            // Act
            service.ExecuteCommand(command);

            // Assert
            // Check console output to verify "Executing command: AdjustPower"
        }

        [TestMethod]
        public void MessageService_ValidateMessage_NonEmptyMessage_ShouldReturnTrue()
        {
            // Arrange
            var service = new MessageService();
            string message = "ValidMessage";

            // Act
            bool result = service.ValidateMessage(message);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MessageService_ValidateMessage_EmptyMessage_ShouldReturnFalse()
        {
            // Arrange
            var service = new MessageService();
            string message = "";

            // Act
            bool result = service.ValidateMessage(message);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TelemetryService_CollectTelemetry_ShouldReturnCorrectTelemetryResponse()
        {
            // Arrange
            var service = new TelemetryService();

            // Act
            var telemetry = service.CollectTelemetry();

            // Assert
            Assert.AreEqual("X:0, Y:1, Z:3", telemetry.Position);
            Assert.AreEqual(28.5f, telemetry.Temperature);
            Assert.AreEqual(1.2f, telemetry.Radiation);
            Assert.AreEqual(3.4f, telemetry.Velocity);
        }
    }
}
