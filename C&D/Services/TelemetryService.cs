using System;
using System.IO;
using System.Text.Json;
using CAndD.Models;

namespace CAndD.Services
{
    public class TelemetryService
    {
        public TelemetryResponse CollectTelemetry()
        {
            return new TelemetryResponse
            {
                Position = "X:0, Y:1, Z:3",
                Temperature = 28.5f,
                Radiation = 1.2f,
                Velocity = 3.4f
            };
        }

        public void SaveTelemetryDataAsJson(TelemetryResponse telemetryData)
        {
            // Get the solution root directory and build the path
            string solutionRootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string filePath = Path.Combine(solutionRootPath, "TelemetryData.json");

            try
            {
                // Serialize and save the data
                string jsonString = JsonSerializer.Serialize(telemetryData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);

                Console.WriteLine($"Telemetry data created successfully .");  // Testing to check file path
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save telemetry data. Error: {ex.Message}");
            }
        }
    }
}
