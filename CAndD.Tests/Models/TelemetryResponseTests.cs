using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Models;
using System;

namespace AllTesting.Models
{
    [TestClass]
    public class TelemetryResponseTests
    {
        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var telemetryResponse = new TelemetryResponse
            {
                Position = "X:100, Y:200, Z:300",
                Temperature = 25.5f,
                Radiation = 1.5f,
                Velocity = 9.5f,
                Timestamp = new DateTime(2024, 1, 1, 12, 0, 0)
            };

            // Act
            var result = telemetryResponse.ToString();

            // Assert
            Assert.AreEqual("Position: X:100, Y:200, Z:300, Temperature: 25.5°C, Radiation: 1.5 mSv, Velocity: 9.5 km/s", result);
        }

        [TestMethod]
        public void Properties_ShouldStoreCorrectValues()
        {
            // Arrange
            var telemetryResponse = new TelemetryResponse();

            // Act
            telemetryResponse.Position = "X:150, Y:250, Z:350";
            telemetryResponse.Temperature = 30.0f;
            telemetryResponse.Radiation = 2.0f;
            telemetryResponse.Velocity = 12.0f;
            telemetryResponse.Timestamp = DateTime.Now;

            // Assert
            Assert.AreEqual("X:150, Y:250, Z:350", telemetryResponse.Position);
            Assert.AreEqual(30.0f, telemetryResponse.Temperature);
            Assert.AreEqual(2.0f, telemetryResponse.Radiation);
            Assert.AreEqual(12.0f, telemetryResponse.Velocity);
            Assert.IsNotNull(telemetryResponse.Timestamp);
        }
    }
}
