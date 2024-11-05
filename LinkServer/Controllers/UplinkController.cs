using LinkServer.Filters;
using Microsoft.AspNetCore.Mvc;
using Uplink_Downlink;

namespace LinkServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(AuthenticateFilter))]
    public class UplinkController : ControllerBase
    {
        private static PacketWrapper _allData = new PacketWrapper("{\"sensorData\": {\"temperature\": 22.5, \"humidity\": 55.0, \"status\": \"operational\"}}");
        private static PacketWrapper _confirmationPacketSettings = new PacketWrapper("{\"settings\": {\"temperature-setting\": 21.5, \"humidity-setting\": 45.0, \"power-setting\": \"power saving\"}}");

        private readonly AppLogger _logger;

        // inject AppLogger through constructor
        public UplinkController(AppLogger logger)
        {
            _logger = logger;
        }


        // this is an example of a endpoint
        // POST api/uplink/send
        [HttpPost("send")]
        public IActionResult SendUplink([FromBody] PacketWrapper packet)
        {
            // log metadata about this request
            _logger.LogMetadata("POST", "api/uplink/send", 200);

            Console.WriteLine($"Received uplink: {packet.JsonData}");
            return Ok(new { Message = "Uplink received and processed" });
        }

        // Simulated packet reception method for demonstration
        [HttpPost("receive-packet")]
        public IActionResult ReceivePacket([FromBody] PacketWrapper packet)
        {
            // Log the incoming packet event
            _logger.LogPacketReceived(packet.JsonData);

            return Ok("Packet received and logged.");
        }

        //  GET api/uplink/request-all-data
        [HttpGet("request-all-data")]
        public IActionResult RequestAllData()
        {
            _logger.LogMetadata("GET", "api/uplink/request-all-data", 200);

            return Ok(_allData);
            // Call function to payload ops to get current settings and put it in a packet to send to the ground station (this is the updating of all important senesor data (new route may be added for showing current settings
        }

       

        // Put apu/uplink/update-settings
        [HttpPut("update-settings")]
        public IActionResult UpdateSettings()
        {
            _logger.LogMetadata("PUT", "api/uplink/update-settings", 200);

            return Ok(_confirmationPacketSettings);
            // Call function to PayloadOaps to pass a packet on
        }
    }
}