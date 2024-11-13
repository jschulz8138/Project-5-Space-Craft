using CAndD.Controllers;
using CAndD.Models;
using CAndD.Services;
using C_D.Readings;
using Payload_Ops;

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

            // Demonstrate usage of ReadingStub and other readings
            var positionReading = new PositionReading("Initial Position Data");
            var temperatureReading = new TemperatureReading("Initial Temperature Data");
            var readingStub = new ReadingStub("Initial Stub Data");

            // Simulate collecting data from various readings
            Console.WriteLine("Position Reading: " + positionReading.GetData());
            Console.WriteLine("Temperature Reading: " + temperatureReading.GetData());
            Console.WriteLine("Reading Stub: " + readingStub.GetData());

            // Modify the data in each reading
            positionReading.SetData("Updated Position Data");
            temperatureReading.SetData("Updated Temperature Data");
            readingStub.SetData("Updated Stub Data");

            // Display updated data
            Console.WriteLine("Updated Position Reading: " + positionReading.GetData());
            Console.WriteLine("Updated Temperature Reading: " + temperatureReading.GetData());
            Console.WriteLine("Updated Reading Stub: " + readingStub.GetData());
        }
    }
}
