using System;
using System.Collections.Generic;
using System.Globalization;
using C_D.Models;

using System.Globalization;
using C_D.Models;

namespace C_D.Utils
{
    public class PacketWrapper
    {
        private readonly TelemetryReadings _readings;

        public PacketWrapper(TelemetryReadings readings)
        {
            _readings = readings;
        }

        public Dictionary<string, string> ToJson()
        {
            var data = new Dictionary<string, string>
            {
                { "datetime", DateTime.Now.ToString(new CultureInfo("en-US")) },
                { "datatype", _readings.GetType().ToString() },
                { "data", _readings.Data },
                { "crc", CrcCalculator() }
            };
            return data;
        }

        private string CrcCalculator()
        {
            return 0xFFFFFFFF.ToString("X8");
        }
    }
}

