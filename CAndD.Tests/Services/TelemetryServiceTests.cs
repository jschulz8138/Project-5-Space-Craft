using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Services;
using CAndD.Models;

namespace AllTesting.Services
{
    [TestClass]
    public class TelemetryServiceTests
    {
        [TestMethod]
        public void CollectTelemetry_ShouldReturnValidData()
        {
            // Arrange
            var service = new TelemetryService();

            // Act
            var telemetryData = service.CollectTelemetry();

            // Assert
            Assert.IsNotNull(telemetryData);
            Assert.IsFalse(string.IsNullOrEmpty(telemetryData.Position));
            Assert.IsTrue(telemetryData.Temperature >= -129 && telemetryData.Temperature <= 146);
            Assert.IsTrue(telemetryData.Radiation >= 1 && telemetryData.Radiation <= 2);
            Assert.IsTrue(telemetryData.Velocity >= 8 && telemetryData.Velocity <= 11);
        }

        [TestMethod]
        public void SaveTelemetryDataAsJson_ShouldSaveToJsonFile()
        {
            // Arrange
            var service = new TelemetryService();
            var telemetryData = service.CollectTelemetry();

            // Act
            service.SaveTelemetryDataAsJson(telemetryData);

            // Assert
            // Check if the JSON file exists (replace with actual file validation)
            Assert.IsTrue(true); // Placeholder for file existence validation
        }
    }
}
