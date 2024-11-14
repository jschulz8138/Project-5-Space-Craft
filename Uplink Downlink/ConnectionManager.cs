using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Uplink_Downlink
{
    internal class ConnectionManager
    {
        private readonly Link _link;
        private const string GroundStationUri = "/api/Authentication";
        private const string DataRoute = "/login";

        public bool IsAuthenticated { get; private set; }
        public ConnectionManager(string baseUrl)
        {
            _link = new Link(baseUrl);
            IsAuthenticated = false;
        }

        //Authenticating with the ground station server


        /// <summary>
        /// Sends an authentication attempt to the ground station.
        /// </summary>
        /// <param name="username">The username used for authentication</param>
        /// <param name="password">The password used for authentication</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> representing the asynchronous operation, with a value of 
        /// <c>true</c> if authentication was successful; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var credentials = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };

            var response = await _link.SendRequestAsync<RestResponse>(ReqType.POST, GroundStationUri + DataRoute, credentials);
            IsAuthenticated = response != null && response.IsSuccessful;
            return IsAuthenticated;
        }
    }
}