using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Uplink_Downlink
{
    public class ConnectionManager
    {
        private readonly Link _link;
        private const string DataRoute = "/api/Authentication/login";

        public bool IsAuthenticated { get; private set; }
        public ConnectionManager(string baseUrl)
        {
            _link = new Link(baseUrl);
            IsAuthenticated = false;
        }

        //Authenticating with the ground station server


        /// <summary>
        /// Sends an authentication attempt to the ground station server using the provided credentials.
        /// </summary>
        /// <param name="AuthPacket">
        /// The authentication data, serialized as a string, which contains the username and password to be used for authentication.
        /// </param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> representing the asynchronous operation, with a value of 
        /// <c>true</c> if authentication was successful; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> AuthenticateAsync(string AuthPacket)
        {
            IsAuthenticated = await _link.SendRequestAsync<bool>(ReqType.POST, DataRoute, AuthPacket);
            return IsAuthenticated;
        }
    }
}   