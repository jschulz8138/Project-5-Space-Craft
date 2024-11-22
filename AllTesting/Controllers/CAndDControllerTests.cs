using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Controllers;
using CAndD.Services;

namespace AllTesting.Controllers
{
    [TestClass]
    public class CAndDControllerTests
    {
        private CAndDController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Use the default constructor
            _controller = new CAndDController();
        }

        [TestMethod]
        public void ProcessCommand_ValidCommand_ShouldExecuteSuccessfully()
        {
            // Act
            _controller.ProcessCommand("AdjustPower 50%");

            // Assert
            // Validate console output or functionality if needed.
        }

        [TestMethod]
        public void DisplayTelemetryData_ShouldDisplayCorrectOutput()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _controller.DisplayTelemetryData();

                // Assert
                var result = sw.ToString();
                Assert.IsTrue(result.Contains("Telemetry Data:"));
            }
        }
    }
}
