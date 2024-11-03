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
        private UplinkController? _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new UplinkController();

            // Reset the static fields to default values before each test
            typeof(UplinkController).GetField("_allData", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(null, new PacketWrapper("{\"sensorData\": {\"temperature\": 22.5, \"humidity\": 55.0, \"status\": \"operational\"}}"));

            typeof(UplinkController).GetField("_currentSettings", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(null, new PacketWrapper("{\"settings\": {\"temperature-setting\": 21.5, \"humidity-setting\": 45.0, \"power-setting\": \"power saving\"}}"));

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
            var result = _controller?.SendUplink(packet) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("{ Message = Uplink received and processed }", result?.Value?.ToString());
        }

        [TestMethod]
        public void UpdateSettings_ShouldReturnUpdatedSettings()
        {
            // Arrange
            var newSettings = new PacketWrapper("{\"settings\": {\"temperature-setting\": 23.5, \"humidity-setting\": 50.0, \"power-setting\": \"standard\"}}");

            // Act
            var result = _controller?.UpdateSettings(newSettings) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newSettings.JsonData, (result.Value as PacketWrapper)?.JsonData);
        }

        [TestMethod]
        public void UpdateSettings_WhenSettingsNull_ShouldReturnBadRequest()
        {
            // Arrange
            PacketWrapper? newSettings = null;

            // Act
            var result = _controller?.UpdateSettings(newSettings) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Settings not updated.", result.Value);

        }

        [TestMethod]
        public void RequestSettings_ShouldReturnUpdatedSettings()
        {
            // Arrange
            var newSettings = new PacketWrapper("{\"settings\": {\"temperature-setting\": 23.5, \"humidity-setting\": 50.0, \"power-setting\": \"standard\"}}");

            // Act
            _controller?.UpdateSettings(newSettings);

            var result = _controller?.RequestSettings() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newSettings.JsonData, (result.Value as PacketWrapper)?.JsonData);
        }

        [TestMethod]
        public void RequestSettings_ShouldReturnDefaultSettings()
        {
            // Act
            var result = _controller?.RequestSettings() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("{\"settings\": {\"temperature-setting\": 21.5, \"humidity-setting\": 45.0, \"power-setting\": \"power saving\"}}",
                            (result.Value as PacketWrapper)?.JsonData);
        }

        [TestMethod]
        public void RequestAllData_ShouldReturnAllData()
        {
            // Act
            var result = _controller?.RequestAllData() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("{\"sensorData\": {\"temperature\": 22.5, \"humidity\": 55.0, \"status\": \"operational\"}}",
                            (result.Value as PacketWrapper)?.JsonData);
        }
    }
}
