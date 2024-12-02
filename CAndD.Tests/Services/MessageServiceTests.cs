using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Services;

namespace AllTesting.Services
{
    [TestClass]
    public class MessageServiceTests
    {
        [TestMethod]
        public void ValidateMessage_EmptyMessage_ShouldReturnFalse()
        {
            // Arrange
            var service = new MessageService();

            // Act
            var result = service.ValidateMessage("");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateMessage_ValidMessage_ShouldReturnTrue()
        {
            // Arrange
            var service = new MessageService();

            // Act
            var result = service.ValidateMessage("AdjustPower 50%");

            // Assert
            Assert.IsTrue(result);
        }
    }
}
