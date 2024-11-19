using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Models; // Ensure this matches the namespace of your Models
using System;

namespace CnD_Test
{
    [TestClass]
    public class ModelsTests
    {
        [TestMethod]
        public void CommandReadings_ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var reading = new CommandReadings
            {
                CommandType = "AdjustPower",
                Target = "Engine",
                Parameters = "Power: 50%",
                Status = "Pending"
            };

            // Act
            var result = reading.ToString();

            // Assert
            Assert.AreEqual("Command: AdjustPower, Target: Engine, Parameters: Power: 50%, Status: Pending", result);
        }

        [TestMethod]
        public void TelemetryReadings_ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var telemetry = new TelemetryReadings
            {
                Position = "X:100, Y:200, Z:300",
                Temperature = 28.5f,
                Radiation = 1.2f,
                Velocity = 3.4f
            };

            // Act
            var result = telemetry.ToString();

            // Assert
            Assert.AreEqual("Position: X:100, Y:200, Z:300, Temperature: 28.5°C, Radiation: 1.2 mSv, Velocity: 3.4 m/s", result);
        }

        [TestMethod]
        public void CommandRequest_SetAndGetProperties_WorksCorrectly()
        {
            // Arrange
            var commandRequest = new CommandRequest
            {
                CommandType = "AdjustPower",
                Target = "Engine",
                Parameters = "Power: 75%"
            };

            // Act & Assert
            Assert.AreEqual("AdjustPower", commandRequest.CommandType);
            Assert.AreEqual("Engine", commandRequest.Target);
            Assert.AreEqual("Power: 75%", commandRequest.Parameters);
        }

        [TestMethod]
        public void TelemetryResponse_ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var response = new TelemetryResponse
            {
                Position = "X:500, Y:400, Z:600",
                Temperature = 22.5f,
                Radiation = 0.8f,
                Velocity = 5.1f
            };

            // Act
            var result = response.ToString();

            // Assert
            Assert.AreEqual("Position: X:500, Y:400, Z:600, Temperature: 22.5°C, Radiation: 0.8 mSv, Velocity: 5.1 m/s", result);
        }
    }
}
