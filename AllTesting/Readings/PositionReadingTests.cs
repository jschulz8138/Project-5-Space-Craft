using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_D.Readings;

namespace AllTesting.Readings
{
    [TestClass]
    public class PositionReadingTests
    {
        [TestMethod]
        public void GetData_ShouldReturnCorrectInitialData()
        {
            // Arrange
            var positionReading = new PositionReading("Initial Position Data");

            // Act
            var result = positionReading.GetData();

            // Assert
            Assert.AreEqual("Initial Position Data", result);
        }

        [TestMethod]
        public void SetData_ShouldUpdateDataCorrectly()
        {
            // Arrange
            var positionReading = new PositionReading("Initial Position Data");

            // Act
            positionReading.SetData("Updated Position Data");
            var result = positionReading.GetData();

            // Assert
            Assert.AreEqual("Updated Position Data", result);
        }
    }
}
