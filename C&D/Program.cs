using CAndD.Controllers;
using CAndD.Models;
using CAndD.Services;

namespace CAndD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var controller = new CAndDController();
            var telemetryService = new TelemetryService();
            var logService = new LogService(); // Instance of LogService

            // Simulate a command input
            controller.ProcessCommand("AdjustPower 50%");

            // Collect telemetry data
            TelemetryResponse telemetryData = telemetryService.CollectTelemetry();

            // Display telemetry data and save it to JSON
            telemetryService.SaveTelemetryDataAsJson(telemetryData);

            // Display telemetry data
            controller.DisplayTelemetryData();

            // Log telemetry data to Excel using the instance of LogService
            logService.LogTelemetryData(telemetryData);
        }
    }
}
