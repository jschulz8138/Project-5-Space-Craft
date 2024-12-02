using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Utils;

namespace AllTesting.Utils
{
    [TestClass]
    public class PacketWrapperTests
    {
        [TestMethod]
        public void WrapPacket_ValidData_ShouldWrapInPacketTags()
        {
            // Arrange
            var data = "TestData";

            // Act
            var result = PacketWrapper.WrapPacket(data);

            // Assert
            Assert.AreEqual("<Packet>TestData</Packet>", result);
        }

        [TestMethod]
        public void WrapPacket_EmptyData_ShouldReturnEmptyPacket()
        {
            // Arrange
            var data = "";

            // Act
            var result = PacketWrapper.WrapPacket(data);

            // Assert
            Assert.AreEqual("<Packet></Packet>", result);
        }

        [TestMethod]
        public void WrapPacket_NullData_ShouldHandleGracefully()
        {
            // Arrange
            string data = null;

            // Act
            var result = PacketWrapper.WrapPacket(data);

            // Assert
            Assert.AreEqual("<Packet></Packet>", result);
        }
    }
}
