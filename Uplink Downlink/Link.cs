using System.Threading.Tasks;
using RestSharp;

namespace Uplink_Downlink
{
    public enum ReqType
    {
        GET, POST, PUT, DELETE
    };

    public class Link
    {
        private readonly RestClient _client;
        private readonly string _url;
        public Link(string url)
        {
            // Validates the URL for throwing exception
            if (!Uri.TryCreate(url, UriKind.Absolute, out _))
            {
                throw new UriFormatException("Invalid URL format.");
            }

            _url = url;
            _client = new RestClient(_url);
        }

        /// <summary>
        /// Sends an HTTP request of the specified type (GET, POST, PUT, DELETE) to a given endpoint with optional arguments.
        /// </summary>
        /// <typeparam name="TResponse">The type of the expected response. Must be a class with a parameterless constructor.</typeparam>
        /// <param name="type">The type of HTTP request to send (GET, POST, PUT, DELETE).</param>
        /// <param name="endpoint">The endpoint URL to which the request is sent.</param>
        /// <param name="args">
        /// Optional arguments for the request, in the form of key-value pairs, 
        /// to be sent as parameters in the request.
        /// </param>
        /// <returns>
        /// A <see cref="Task{TResponse}"/> representing the asynchronous operation, which contains 
        /// the response of type <typeparamref name="TResponse"/> if the request succeeds; otherwise, <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if an invalid request type is specified.</exception>
        public async Task<TResponse?> SendRequestAsync<TResponse>(ReqType type, string endpoint, Dictionary<string, string>? args = null) where TResponse : class, new()
        {
            var request = new RestRequest(endpoint);

            // Add parameters to the request
            if (args != null)
            {
                foreach (var arg in args)
                {
                    request.AddParameter(arg.Key, arg.Value);
                }
            }

            // Determine request type
            switch (type)
            {
                case ReqType.GET:
                    return await _client.GetAsync<TResponse>(request);
                case ReqType.POST:
                    return await _client.PostAsync<TResponse>(request);
                case ReqType.PUT:
                    return await _client.PutAsync<TResponse>(request);
                case ReqType.DELETE:
                    return await _client.DeleteAsync<TResponse>(request);
                default:
                    throw new ArgumentException("Invalid request type");
            }
        }
    }
}