using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LinkServer.Controllers;

namespace LinkServer.Controllers.Tests
{
    [TestClass]
    public class UplinkControllerTests
    {
        private UplinkController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new UplinkController();

            // Reset the static fields to default values before each test
            typeof(UplinkController).GetField("_allData", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .SetValue(null, new PacketWrapper("{\"sensorData\": {\"temperature\": 22.5, \"humidity\": 55.0, \"status\": \"operational\"}}"));

            typeof(UplinkController).GetField("_currentSettings", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .SetValue(null, new PacketWrapper("{\"settings\": {\"temperature-setting\": 21.5, \"humidity-setting\": 45.0, \"power-setting\": \"power saving\"}}"));

            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TestMethod]
        public void SendUplink_ShouldReturnSuccessMessage()
        {
            // Arrange
            var packet = new PacketWrapper("{\"sensorData\": {\"temperature\": 24.5, \"humidity\": 60.0, \"status\": \"active\"}}");

            // Act
            var result = _controller.SendUplink(packet) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("{ Message = Uplink received and processed }", result?.Value?.ToString());
        }
    }
}
