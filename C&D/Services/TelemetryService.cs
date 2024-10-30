using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_D.Models;

using C_D.Models;

namespace C_D.Services
{
    public class TelemetryService
    {
        public TelemetryResponse GetTelemetryData()
        {
            // Return dummy telemetry data for now
            return new TelemetryResponse
            {
                Temperature = "72°F",
                Position = "10, 20, 30",
                PowerStatus = "On"
            };
        }
    }
}

