using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_D.Readings;

namespace AllTesting.Readings
{
    [TestClass]
    public class TemperatureReadingTests
    {
        [TestMethod]
        public void GetData_ShouldReturnCorrectInitialData()
        {
            // Arrange
            var temperatureReading = new TemperatureReading("Initial Temperature Data");

            // Act
            var result = temperatureReading.GetData();

            // Assert
            Assert.AreEqual("Initial Temperature Data", result);
        }

        [TestMethod]
        public void SetData_ShouldUpdateDataCorrectly()
        {
            // Arrange
            var temperatureReading = new TemperatureReading("Initial Temperature Data");

            // Act
            temperatureReading.SetData("Updated Temperature Data");
            var result = temperatureReading.GetData();

            // Assert
            Assert.AreEqual("Updated Temperature Data", result);
        }
    }
}
