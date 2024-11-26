using RestSharp;

namespace Uplink_Downlink
{
    public enum ReqType
    {
        GET, POST, PUT, DELETE
    };

    // Create an interface to allow mocking
    public interface ILink
    {
        Task<bool> SendRequestAsync<TResponse>(ReqType type, string endpoint, string serializedPacket);
    }

    public class Link : ILink
    {
        private readonly RestClient _client;
        public Link(string url, RestClient? client = null)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out _))
            {
                throw new UriFormatException("Invalid URL format.");
            }
            _client = client ?? new RestClient(url);
        }
        
        /// <summary>
        /// Sends an HTTP request of the specified type (GET, POST, PUT, DELETE) to a given endpoint with optional arguments.
        /// </summary>
        /// <typeparam name="TResponse">The type of the expected response. Must be a class with a parameterless constructor.</typeparam>
        /// <param name="type">The type of HTTP request to send (GET, POST, PUT, DELETE).</param>
        /// <param name="endpoint">The endpoint URL to which the request is sent.</param>
        /// <param name="serializedPacket">
        /// The serialized data to be sent as part of the request. This can be in JSON format and is passed as a query parameter.
        /// </param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> representing the asynchronous operation, which contains <c>true</c> if the request was successful; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if an invalid request type is specified.</exception>
        public virtual async Task<bool> SendRequestAsync<TResponse>(ReqType type, string endpoint, string serializedPacket)
        {
            var request = new RestRequest(endpoint);
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("packet", serializedPacket);

            try
            {
                // Determine request type
                RestResponse response = type switch
                {
                    ReqType.GET => await _client.GetAsync(request),
                    ReqType.POST => await _client.PostAsync(request),
                    ReqType.PUT => await _client.PutAsync(request),
                    ReqType.DELETE => await _client.DeleteAsync(request),
                    _ => throw new ArgumentException("Invalid request type")
                };

                // Return true if the response is successful
                return response.IsSuccessful;
            }
            catch (HttpRequestException ex)
            {
                // Handle specific HTTP request exceptions, such as network issues, timeouts, etc.
                Console.WriteLine($"Request failed: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Catch any other exceptions (e.g., argument exceptions, serialization issues, etc.)
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }
    }
}