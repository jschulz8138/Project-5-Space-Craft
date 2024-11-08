using LinkServer.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Uplink_Downlink;

namespace LinkServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(AuthenticateFilter))]
    public class UplinkController : ControllerBase
    {
        private static PacketWrapper _allData = new PacketWrapper("{\"sensorData\": {\"temperature\": 22.5, \"humidity\": 55.0, \"status\": \"operational\"}}");
        private static PacketWrapper _currentSettings = new PacketWrapper("{\"settings\": {\"temperature-setting\": 21.5, \"humidity-setting\": 45.0, \"power-setting\": \"power saving\"}}");

        private readonly AppLogger _logger;

        // inject AppLogger through constructor
        public UplinkController(AppLogger logger)
        {
            _logger = logger;
        }

        // POST api/uplink/send
        [HttpPost("send")]
        public IActionResult SendUplink([FromBody] PacketWrapper packet)
        {
            // log metadata about this request
            _logger.LogMetadata("POST", "api/uplink/send", 200);

            Console.WriteLine($"Received uplink: {packet.JsonData}");
            return Ok(new { Message = "Uplink received and processed" });
        }

        // Put apu/uplink/update-settings
        [HttpPut("update-settings")]
        public IActionResult UpdateSettings([FromBody] PacketWrapper settings)
        {
            // REPLACE WITH STUB / ACTUAL CALL FROM PAYLOAD OPS
            _currentSettings = settings;

            if(_currentSettings != null)
            {
                _logger.LogMetadata("PUT", "api/uplink/update-settings", 200);
                return Ok(_currentSettings);
            }
            else
            {
                return BadRequest("Settings not updated."); 
            }
        }

        // GET api/uplink/request-settings
        [HttpGet("request-settings")]
        public IActionResult RequestSettings()
        {
            // REPLACE WITH STUB / ACTUAL CALL FROM PAYLOAD OPS
            PacketWrapper currentSettingsPacket = _currentSettings;
            Console.WriteLine(currentSettingsPacket.ToString());

            return Ok(currentSettingsPacket);
        }

        //  GET api/uplink/request-all-data
        [HttpGet("request-all-data")]
        public IActionResult RequestAllData()
        {
            // REPLACE WITH STUB / ACTUAL CALL FROM PAYLOAD OPS
            PacketWrapper allData = new PacketWrapper("{\"sensorData\": {\"temperature\": 22.5, \"humidity\": 55.0, \"status\": \"operational\"}}");
            _logger.LogMetadata("GET", "api/uplink/request-all-data", 200);
            return Ok(allData);
        }

        // Put apu/uplink/update-settings
        [HttpPut("update-settings")]
        public IActionResult UpdateSettings()
        {
            _logger.LogMetadata("PUT", "api/uplink/update-settings", 200);
            return Ok(_currentSettings);
        }
    }
}