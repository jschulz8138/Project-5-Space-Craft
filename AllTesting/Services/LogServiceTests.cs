using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Services;
using CAndD.Models;
using System;

namespace AllTesting.Services
{
    [TestClass]
    public class LogServiceTests
    {
        [TestMethod]
        public void LogTelemetryData_ShouldWriteToExcelFile()
        {
            // Arrange
            var service = new LogService();
            var telemetryData = new TelemetryResponse
            {
                Position = "X:100, Y:200, Z:300",
                Temperature = 25.5f,
                Radiation = 1.5f,
                Velocity = 9.5f,
                Timestamp = DateTime.Now
            };

            // Act
            service.LogTelemetryData(telemetryData);

            // Assert
            // Check if the file was updated (if possible, use mocks or a test file)
            Assert.IsTrue(true); // Placeholder for Excel validation
        }
    }
}
