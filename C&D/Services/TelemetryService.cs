using System;
using System.IO;
using System.Text.Json;
using CAndD.Models;

namespace CAndD.Services
{
    public class TelemetryService
    {
        private readonly Random _random = new Random();
        private double _lastX = 1000.0;
        private double _lastY = 2000.0;
        private double _lastZ = 3000.0;

        public TelemetryResponse CollectTelemetry()
        {
            // Generate small random variations for position to simulate movement
            double x = _lastX + (_random.NextDouble() * 2 - 1);  // Variations between -1 and 1
            double y = _lastY + (_random.NextDouble() * 2 - 1);
            double z = _lastZ + (_random.NextDouble() * 2 - 1);

            // Update last known position
            _lastX = x;
            _lastY = y;
            _lastZ = z;

            // Generate realistic data within specified ranges
            return new TelemetryResponse
            {
                Position = $"X:{x:F2}, Y:{y:F2}, Z:{z:F2}",
                Temperature = (float)(_random.NextDouble() * (146 - (-129)) + (-129)), // Range -129 to 146
                Radiation = (float)(_random.NextDouble() + 1), // Range 1 to 2 mSv
                Velocity = (float)(_random.NextDouble() * (11 - 8) + 8), // Range 8 to 11 km/h
                Timestamp = DateTime.Now
            };
        }

        public void SaveTelemetryDataAsJson(TelemetryResponse telemetryData)
        {
            string solutionRootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string filePath = Path.Combine(solutionRootPath, "TelemetryData.json");

            try
            {
                string jsonString = JsonSerializer.Serialize(telemetryData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"Telemetry data saved successfully to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save telemetry data. Error: {ex.Message}");
            }
        }
    }
}
