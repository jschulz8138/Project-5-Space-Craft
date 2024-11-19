using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplink_Downlink
{
    public class CommunicationHandler
    {
        private readonly Link _link;
        private const string DataRoute = "/api/downlink/receive";
        public CommunicationHandler(string baseUrl)
        {
            _link = new Link(baseUrl);
        }

        //method to ground station

        /// <summary>
        /// Sends a data packet to the ground station and returns whether the data was successfully received.
        /// </summary>
        /// <param name="DataPacket">The data packet to be sent as a string in JSON format.</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> representing the asynchronous operation, with a value of 
        /// <c>true</c> if the data was successfully received by the ground station; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> UpdateGroundStationAsync(string DataPacket) 
        {
            return await _link.SendRequestAsync<bool>(ReqType.POST, DataRoute, DataPacket);
        }
    }
}