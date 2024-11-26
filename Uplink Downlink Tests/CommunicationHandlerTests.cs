using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uplink_Downlink;

namespace Uplink_Downlink_Tests
{
    [TestClass]
    public class CommunicationHandlerTests
    {
        private Mock<ILink> _mockLink;
        private CommunicationHandler _communicationHandler;

        [TestInitialize]
        public void Setup()
        {
            // Setup mock for ILink
            _mockLink = new Mock<ILink>();
            // Mock SendRequestAsync method to return true
            _mockLink.Setup(link => link.SendRequestAsync<bool>(It.IsAny<ReqType>(), It.IsAny<string>(), It.IsAny<string>()))
                     .ReturnsAsync(true);

            // Initialize CommunicationHandler with the mock Link
            _communicationHandler = new CommunicationHandler(_mockLink.Object);
        }

        [TestMethod]
        public async Task UpdateGroundStationAsync_SuccessfulRequest_ShouldReturnTrue()
        {
            // Arrange
            var dataPacket = "{\"data\": \"test\"}";

            // Mock the SendRequestAsync to return true (success)
            _mockLink.Setup(link => link.SendRequestAsync<bool>(ReqType.POST, "/api/downlink/receive", dataPacket))
                     .ReturnsAsync(true);

            // Act
            var result = await _communicationHandler.UpdateGroundStationAsync(dataPacket);

            // Assert
            Assert.IsTrue(result, "Expected true when the data was successfully sent.");
        }

        [TestMethod]
        public async Task UpdateGroundStationAsync_UnsuccessfulRequest_ShouldReturnFalse()
        {
            // Arrange
            var dataPacket = "{\"data\": \"test\"}";

            // Mock the SendRequestAsync to return false (failure)
            _mockLink.Setup(link => link.SendRequestAsync<bool>(ReqType.POST, "/api/downlink/receive", dataPacket))
                     .ReturnsAsync(false);

            // Act
            var result = await _communicationHandler.UpdateGroundStationAsync(dataPacket);

            // Assert
            Assert.IsFalse(result, "Expected false when the data was not successfully sent.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task UpdateGroundStationAsync_InvalidDataPacket_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidDataPacket = string.Empty; // or null, depending on your validation logic

            // Act
            await _communicationHandler.UpdateGroundStationAsync(invalidDataPacket);

            // Assert: Exception should be thrown, so no need for an Assert statement here.
        }
    }
}
