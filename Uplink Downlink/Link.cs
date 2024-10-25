using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uplink_Downlink
{
    public class Link
    {
        private RestClient _client;
        private string _url;

        public Link(string url)
        {
            this._url = url;
            _client = new RestClient(url);
        }
    }
}
