using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Controllers;

namespace CnD_Test
{
    [TestClass]
    public class CAndDControllerTests
    {
        private CAndDController _controller = null!; // Initialized in TestInitialize

        // Expected messages for console output
        private const string SuccessMessage = "Command executed successfully.";
        private const string ErrorMessage = "Invalid command format.";

        [TestInitialize]
        public void Setup()
        {
            _controller = new CAndDController();
        }

        [TestMethod]
        public void ProcessCommand_ValidCommand_ExecutesSuccessfully()
        {
            // Arrange
            string validCommand = "ValidCommand";

            // Act and Assert
            AssertProcessCommandOutput(validCommand, expectedOutput: SuccessMessage);
        }

        [TestMethod]
        public void ProcessCommand_InvalidCommand_ShowsErrorMessage()
        {
            // Arrange
            string invalidCommand = "";

            // Act and Assert
            AssertProcessCommandOutput(invalidCommand, expectedOutput: ErrorMessage);
        }

        /// <summary>
        /// Helper method to execute ProcessCommand and verify console output.
        /// </summary>
        /// <param name="command">The command to be processed.</param>
        /// <param name="expectedOutput">The expected console output message.</param>
        private void AssertProcessCommandOutput(string command, string expectedOutput)
        {
            var originalConsoleOut = Console.Out;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                try
                {
                    // Act
                    _controller.ProcessCommand(command);  // No return value, just execute the method

                    // Capture and trim console output
                    string output = sw.ToString().Trim();
                    Assert.AreEqual(expectedOutput, output, $"Expected message '{expectedOutput}' was not printed.");
                }
                finally
                {
                    // Restore the original console output
                    Console.SetOut(originalConsoleOut);
                }
            }
        }
    }
}
