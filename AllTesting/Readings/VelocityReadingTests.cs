//using Moq;

//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Payload_Ops;

//namespace AllTesting.Readings
//{
//    [TestClass]
//    public class VelocityReadingTests
//    {
//        [TestMethod]
//        public void GetData_ShouldReturnCorrectInitialData()
//        {
//            // Arrange
//            var velocityReading = new VelocityReading("Initial Velocity Data");

//            // Act
//            var result = velocityReading.GetData();

//            // Assert
//            Assert.AreEqual("Initial Velocity Data", result);
//        }

//        [TestMethod]
//        public void SetData_ShouldUpdateDataCorrectly()
//        {
//            // Arrange
//            var velocityReading = new VelocityReading("Initial Velocity Data");

//            // Act
//            velocityReading.SetData("Updated Velocity Data");
//            var result = velocityReading.GetData();

//            // Assert
//            Assert.AreEqual("Updated Velocity Data", result);
//        }
//    }
//}
