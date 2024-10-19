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

        // this is an example of a endpoint
        // POST api/uplink/send
        [HttpPost("send")]
        public IActionResult SendUplink([FromBody] PacketWrapper packet)
        {
            Console.WriteLine($"Received uplink: {packet.JsonData}");
            return Ok(new { Message = "Uplink received and processed" });
        }

        //  GET api/uplink/request-all-data
        [HttpGet("request-all-data")]
        public IActionResult RequestAllData()
        {
            return Ok(_allData);
            // Call function to payload ops to get current settings and put it in a packet to send to the ground station (this is the updating of all important senesor data (new route may be added for showing current settings
        }

        // Put apu/uplink/update-settings
        [HttpPut("update-settings")]
        public IActionResult UpdateSettings()
        {
            return Ok(_confirmationPacketSettings);
            // Call function to PayloadOaps to pass a packet on
        }
    }
}