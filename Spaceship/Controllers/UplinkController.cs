using LinkServer.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Uplink_Downlink;
using Payload_Ops;
using Proj5Spaceship.Filters;

namespace LinkServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(AuthenticateFilter))]
    public class UplinkController : ControllerBase
    {
        private readonly AppLogger _logger;
        private readonly Spaceship _spaceship;

        public UplinkController(AppLogger logger, Spaceship spaceship)
        {
            _logger = logger;
            _spaceship = spaceship;
        }

        [HttpPost("send")]
        public IActionResult SendUplink([FromBody] string packet)
        {
            bool success = _spaceship.Receive(packet.ToUpper());

            string returnMessage = success ? "Uplink processed successfully." : "Uplink failed to process.";

            if (success)
            {
                _logger.LogMetadata("POST", "api/uplink/send", 200);
                return Ok(returnMessage);
            }
            else
            {
                _logger.LogMetadata("POST", "api/uplink/send", 400);
                return BadRequest(returnMessage);
            }
        }
    }
}