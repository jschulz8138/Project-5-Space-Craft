using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Controllers;
using Moq; // Mocking framework
using CAndD.Services;

namespace AllTesting.Controllers
{
    [TestClass]
    public class CAndDControllerTests
    {
        private Mock<CommandService> _mockCommandService;
        private Mock<TelemetryService> _mockTelemetryService;
        private Mock<MessageService> _mockMessageService;
        private CAndDController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Mock the services
            _mockCommandService = new Mock<CommandService>();
            _mockTelemetryService = new Mock<TelemetryService>();
            _mockMessageService = new Mock<MessageService>();

            // Initialize the controller
            _controller = new CAndDController();
        }

        [TestMethod]
        public void ProcessCommand_ValidCommand_ShouldExecuteSuccessfully()
        {
            // Arrange
            var command = "AdjustPower 50%";
            _mockMessageService.Setup(m => m.ValidateMessage(command)).Returns(true);

            // Act
            _controller.ProcessCommand(command);

            // Assert
            _mockCommandService.Verify(c => c.ExecuteCommand(command), Times.Once);
            // You can also assert console output or exceptions, if needed.
        }

        [TestMethod]
        public void ProcessCommand_InvalidCommand_ShouldNotExecuteCommand()
        {
            // Arrange
            var command = ""; // Invalid command
            _mockMessageService.Setup(m => m.ValidateMessage(command)).Returns(false);

            // Act
            _controller.ProcessCommand(command);

            // Assert
            _mockCommandService.Verify(c => c.ExecuteCommand(It.IsAny<string>()), Times.Never);
            // You can also validate error handling here.
        }

        [TestMethod]
        public void DisplayTelemetryData_ShouldCollectAndDisplayTelemetry()
        {
            // Arrange
            var telemetryData = "Position: X:100, Y:200, Z:300; Temp: 25°C";
            _mockTelemetryService.Setup(t => t.CollectTelemetry()).Returns(telemetryData);

            // Act
            _controller.DisplayTelemetryData();

            // Assert
            _mockTelemetryService.Verify(t => t.CollectTelemetry(), Times.Once);
            _mockTelemetryService.Verify(t => t.SaveTelemetryDataAsJson(telemetryData), Times.Once);
        }

        [TestMethod]
        public void DisplayTelemetryData_ShouldSaveTelemetryToJsonFile()
        {
            // Arrange
            var telemetryData = "Position: X:150, Y:250, Z:350; Temp: 30°C";
            _mockTelemetryService.Setup(t => t.CollectTelemetry()).Returns(telemetryData);

            // Act
            _controller.DisplayTelemetryData();

            // Assert
            _mockTelemetryService.Verify(t => t.SaveTelemetryDataAsJson(telemetryData), Times.Once);
        }
    }
}
