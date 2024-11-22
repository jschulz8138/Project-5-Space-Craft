using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAndD.Models;

namespace AllTesting.Models
{
    [TestClass]
    public class CommandReadingsTests
    {
        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var commandReadings = new CommandReadings
            {
                CommandType = "AdjustPower",
                Target = "Engine",
                Parameters = "Power: 50%",
                Status = "Success"
            };

            // Act
            var result = commandReadings.ToString();

            // Assert
            Assert.AreEqual("Command: AdjustPower, Target: Engine, Parameters: Power: 50%, Status: Success", result);
        }

        [TestMethod]
        public void Properties_ShouldStoreCorrectValues()
        {
            // Arrange
            var commandReadings = new CommandReadings();

            // Act
            commandReadings.CommandType = "ChangeDirection";
            commandReadings.Target = "Navigation";
            commandReadings.Parameters = "Direction: North";
            commandReadings.Status = "Pending";

            // Assert
            Assert.AreEqual("ChangeDirection", commandReadings.CommandType);
            Assert.AreEqual("Navigation", commandReadings.Target);
            Assert.AreEqual("Direction: North", commandReadings.Parameters);
            Assert.AreEqual("Pending", commandReadings.Status);
        }
    }
}
