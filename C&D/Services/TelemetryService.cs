using System.Text.Json;
using System.IO;
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
            string jsonString = JsonSerializer.Serialize(telemetryData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("TelemetryData.json", jsonString);
        }
    }
}
