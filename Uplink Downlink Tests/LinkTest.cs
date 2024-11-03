using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Uplink_Downlink;

namespace UnitTest_UplinkDownlink
{
    [TestClass]
    public class LinkTest
    {
        //LinkTest_01:Testing for initializing the url and rest client
        [TestMethod]
        public void ShouldInitialize_UrlandRestClient()
        {
            String testUrl = "https://exampletest.com";

            Link link = new Link(testUrl);

            //using the reflection to access the private fields
            var urlField = typeof(Link).GetField("_url", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var clientField = typeof(Link).GetField("_client", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.AreEqual(testUrl, urlField.GetValue(link));  // Ensuring that the _url field is set correctly
            Assert.IsNotNull(clientField.GetValue(link));  // Ensure that the _client (RestClient) is initialized
        }

        //Testing whether the -url is stored correctly

        [TestMethod]
        public void URL_shouldBeStoredInField()
        {
            string testUrl = "https://exampletest.com";
            Link link = new Link(testUrl);

            var urlField = typeof(Link).GetField("_url", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Assert.AreEqual(testUrl, urlField.GetValue(link));
        }

        //Testing that RestCLient is  initiallized correctly with the provide URL
        [TestMethod]
        public void Should_initializeRestClientwithURL()
        {
            string testUrl = "https://exampletest.com/";
            Link link = new Link(testUrl);

            var clientField = typeof(Link).GetField("_client", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var restClient = clientField.GetValue(link) as RestClient;

            // Ensuring that the RestClient is initialized
            Assert.IsNotNull(restClient);

            // Checking if the RestClient's BaseUrl is the correct URL
            Assert.AreEqual(testUrl, restClient.Options.BaseUrl.ToString());
        }

        // Invalid url test

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void _ShouldThrowExceptionForInvalidUrl()
        {
            string invalidUrl = "invalid-url";
            Link link = new Link(invalidUrl);
        }
    }
}