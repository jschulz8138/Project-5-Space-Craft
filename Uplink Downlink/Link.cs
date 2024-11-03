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
            _client = new RestClient(url);
        }

        // Function requires the type of request being sent (Get, Post, Put, Delete), then it requires the endpoint that its being sent to, then the last paremeter is the arguments (needs to be in dictonary object format)
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