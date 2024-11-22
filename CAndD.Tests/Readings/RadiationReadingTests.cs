using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payload_Ops;

namespace AllTesting.Readings
{
    [TestClass]
    public class RadiationReadingTests
    {
        [TestMethod]
        public void GetData_ShouldReturnCorrectInitialData()
        {
            // Arrange
            var radiationReading = new RadiationReading("Initial Radiation Data");

            // Act
            var result = radiationReading.GetData();

            // Assert
            Assert.AreEqual("Initial Radiation Data", result);
        }

        [TestMethod]
        public void SetData_ShouldUpdateDataCorrectly()
        {
            // Arrange
            var radiationReading = new RadiationReading("Initial Radiation Data");

            // Act
            radiationReading.SetData("Updated Radiation Data");
            var result = radiationReading.GetData();

            // Assert
            Assert.AreEqual("Updated Radiation Data", result);
        }
    }
}
