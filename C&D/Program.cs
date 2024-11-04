using CAndD.Controllers;

namespace CAndD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var controller = new CAndDController();

            // Simulate a command input
            controller.ProcessCommand("AdjustPower 50%");

            // Display telemetry data
            controller.DisplayTelemetryData();
        }
    }
}
