using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Utils;
using CAndD.Models;

namespace AllTesting.Utils
{
    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        public void SerializeTelemetry_ValidTelemetryData_ShouldReturnJsonString()
        {
            // Arrange
            var telemetry = new TelemetryResponse
            {
                Position = "X:100, Y:200, Z:300",
                Temperature = 25.5f,
                Radiation = 1.5f,
                Velocity = 9.5f,
                Timestamp = new System.DateTime(2024, 1, 1, 12, 0, 0)
            };

            // Act
            var json = Serializer.SerializeTelemetry(telemetry);

            // Assert
            Assert.IsTrue(json.Contains("\"Position\":\"X:100, Y:200, Z:300\""));
            Assert.IsTrue(json.Contains("\"Temperature\":25.5"));
            Assert.IsTrue(json.Contains("\"Radiation\":1.5"));
            Assert.IsTrue(json.Contains("\"Velocity\":9.5"));
        }

        [TestMethod]
        public void SerializeTelemetry_NullTelemetryData_ShouldHandleGracefully()
        {
            // Arrange
            TelemetryResponse telemetry = null;

            // Act
            var json = Serializer.SerializeTelemetry(telemetry);

            // Assert
            Assert.AreEqual("null", json); // JSON serializer returns "null" for null objects
        }
    }
}
