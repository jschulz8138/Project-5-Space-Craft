using LinkServer.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LinkServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(AuthenticateFilter))]
    public class UplinkController : ControllerBase
    {
        private static PacketWrapper _allData = new PacketWrapper("{\"sensorData\": {\"temperature\": 22.5, \"humidity\": 55.0, \"status\": \"operational\"}}");
        private static PacketWrapper _confirmationPacketSettings = new PacketWrapper("{\"settings\": {\"temperature-setting\": 21.5, \"humidity-setting\": 45.0, \"power-setting\": \"power saving\"}}");

        // POST api/uplink/send
        [HttpPost("send")]
        public IActionResult SendUplink([FromBody] PacketWrapper packet)
        {
            Console.WriteLine($"Received uplink: {packet.JsonData}");
            return Ok(new { Message = "Uplink received and processed" });
        }

        // Put apu/uplink/update-settings
        [HttpPut("update-settings")]
        public IActionResult UpdateSettings([FromBody] PacketWrapper settings)
        {
            // REPLACE WITH STUB / ACTUAL CALL FROM PAYLOAD OPS
            _confirmationPacketSettings = settings;

            if(_confirmationPacketSettings != null)
            {
                return Ok(_confirmationPacketSettings);
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
            PacketWrapper currentSettings = new PacketWrapper("{\"settings\": {\"temperature-setting\": 21.5, \"humidity-setting\": 45.0, \"power-setting\": \"power saving\"}}");

            return Ok(currentSettings);
        }

        //  GET api/uplink/request-all-data
        [HttpGet("request-all-data")]
        public IActionResult RequestAllData()
        {
            // REPLACE WITH STUB / ACTUAL CALL FROM PAYLOAD OPS
            PacketWrapper allData = new PacketWrapper("{\"sensorData\": {\"temperature\": 22.5, \"humidity\": 55.0, \"status\": \"operational\"}}");

            return Ok(allData);
        }
    }
}