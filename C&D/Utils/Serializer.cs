using System.Text.Json;
using CAndD.Models;

namespace CAndD.Utils
{
    public static class Serializer
    {
        public static string SerializeTelemetry(TelemetryResponse telemetry)
        {
            return JsonSerializer.Serialize(telemetry);
        }
    }
}
