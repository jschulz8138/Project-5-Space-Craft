using CAndD.Controllers;
using CAndD.Services;
using C_D.Readings;  // Assuming other readings are in this namespace
using Payload_Ops;
using CAndD.Models;

namespace CAndD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var controller = new CAndDController();
            var telemetryService = new TelemetryService();
            var logService = new LogService();

            // Simulate a command input
            controller.ProcessCommand("AdjustPower 50%");

            // Collect telemetry data
            var telemetryData = telemetryService.CollectTelemetry();

            // Display telemetry data and save it to JSON
            telemetryService.SaveTelemetryDataAsJson(telemetryData);
            controller.DisplayTelemetryData();

            // Log telemetry data to Excel
            logService.LogTelemetryData(telemetryData);

            // Create and display individual readings using fully qualified names
            var positionReading = new C_D.Readings.PositionReading("Initial Position Data");
            var temperatureReading = new C_D.Readings.TemperatureReading("Initial Temperature Data");

            Console.WriteLine("Position Reading: " + positionReading.GetData());
            Console.WriteLine("Temperature Reading: " + temperatureReading.GetData());

            // Update and display modified readings
            positionReading.SetData("Updated Position Data");
            temperatureReading.SetData("Updated Temperature Data");

            Console.WriteLine("Updated Position Reading: " + positionReading.GetData());
            Console.WriteLine("Updated Temperature Reading: " + temperatureReading.GetData());
        }
    }
}
