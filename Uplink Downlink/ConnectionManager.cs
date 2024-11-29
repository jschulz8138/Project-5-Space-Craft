namespace Uplink_Downlink
{
    public class ConnectionManager
    {

        private readonly ILink _link;//changed to ILink to support mocking
        private const string DataRoute = "/api/Authentication/login";

        public bool IsAuthenticated { get; private set; }
        public ConnectionManager(string baseUrl)
        {
            _link = new Link(baseUrl);
            IsAuthenticated = false;
        }

        //for testing (mocking)
        public ConnectionManager(ILink link)
        {
            _link = link ?? throw new ArgumentNullException(nameof(link));
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