using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Utils;
using CAndD.Models;
using System.Collections.Generic;

namespace CnD_Test
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void PacketWrapper_WrapPacket_ReturnsWrappedData()
        {
            // Arrange
            string data = "TestData";

            // Act
            var result = PacketWrapper.WrapPacket(data);

            // Assert
            Assert.AreEqual("<Packet>TestData</Packet>", result);
        }

        [TestMethod]
        public void Serializer_SerializeTelemetry_ReturnsSerializedJson()
        {
            // Arrange
            var telemetry = new TelemetryResponse
            {
                Position = "X:100, Y:200, Z:300",
                Temperature = 28.5f,
                Radiation = 1.2f,
                Velocity = 3.4f
            };

            // Act
            var result = Serializer.SerializeTelemetry(telemetry);

            // Assert
            StringAssert.Contains(result, "\"Position\":\"X:100, Y:200, Z:300\"");
            StringAssert.Contains(result, "\"Temperature\":28.5");
            StringAssert.Contains(result, "\"Radiation\":1.2");
            StringAssert.Contains(result, "\"Velocity\":3.4");
        }
    }
}
