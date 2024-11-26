namespace Uplink_Downlink
{
    public class CommunicationHandler
    {
        private readonly ILink _link;
        private const string DataRoute = "/api/downlink/receive";
        public CommunicationHandler(string baseUrl)
        {
            _link = new Link(baseUrl);
        }

        public CommunicationHandler(ILink link)
        {
            _link = link;
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
        public async Task<bool> UpdateGroundStationAsync(string dataPacket)
        {
            if (string.IsNullOrEmpty(dataPacket))
            {
                throw new ArgumentException("Invalid data packet.");
            }

            try
            {
                return await _link.SendRequestAsync<bool>(ReqType.POST, DataRoute, dataPacket);
            }
            catch (HttpRequestException ex)
            {
                // Handle specific HTTP exceptions if needed
                // You can throw a more specific exception or log it
                throw new Exception("There was an error with the request.", ex);
            }
        }

    }
}