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
        private const string GroundStationUri = "/api";
        private const string DataRoute = "/Downlink";
        public CommunicationHandler(string baseUrl)
        {
            _link = new Link(baseUrl);
        }

        //method to ground station

        /// <summary>
        /// Sends data to the ground station and returns whether the data was received properly.
        /// </summary>
        /// <param name="generalData">The data to be sent as key-value pairs.</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> representing the asynchronous operation, with a value of 
        /// <c>true</c> if the data was received successfully; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool>UpdateGroundStationAsync(Dictionary<string, string> generalData) 
        {
            var response = await _link.SendRequestAsync<RestResponse>(ReqType.POST, GroundStationUri + DataRoute, generalData);
            return response != null && response.IsSuccessful;
        }
    }
}
