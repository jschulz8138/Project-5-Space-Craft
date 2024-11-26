using System.Net;
using System.Threading.Tasks;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uplink_Downlink;
using System.Linq;

namespace Uplink_Downlink_Tests
{
    [TestClass]
    public class LinkTests
    {
        private WireMockServer _server;

        [TestInitialize]
        public void SetUp()
        {
            // Start the mock server on an available port
            _server = WireMockServer.Start();
        }

        [TestCleanup]
        public void TearDown()
        {
            // Stop the server after tests
            _server.Stop();
            _server.Dispose();
        }

        [TestMethod]
        public async Task SendRequestAsync_ValidRequest_ShouldReturnTrue()
        {
            // Arrange
            var endpoint = "/test-endpoint";
            var packet = "example-packet";

            // Configure the mock server to respond to a GET request
            _server
                .Given(Request.Create().WithPath(endpoint).UsingGet()
                    .WithParam("packet", packet))
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody("{\"success\": true}"));

            var link = new Link(_server.Url);

            // Act
            var result = await link.SendRequestAsync<object>(ReqType.GET, endpoint, packet);

            // Assert
            Assert.IsTrue(result);

            // Verify that the server received the expected request
            var log = _server.LogEntries.ToList().FirstOrDefault();
            Assert.IsNotNull(log);  // Ensure there is at least one log entry
            Assert.AreEqual("GET", log.RequestMessage.Method);
            Assert.AreEqual($"{_server.Url}{endpoint}?packet={packet}", log.RequestMessage.Url);
        }

        [TestMethod]
        public async Task SendRequestAsync_UnsuccessfulResponse_ShouldReturnFalse()
        {
            // Arrange
            var endpoint = "/test-endpoint";
            var packet = "example-packet";

            // Configure the mock server to respond to a GET request with an error
            _server
                .Given(Request.Create().WithPath(endpoint).UsingGet()
                    .WithParam("packet", packet))
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithBody("{\"success\": false}"));

            var link = new Link(_server.Url);

            // Act
            var result = await link.SendRequestAsync<object>(ReqType.GET, endpoint, packet);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task SendRequestAsync_PostRequest_ShouldReturnTrue()
        {
            // Arrange
            var endpoint = "/test-post";
            var packet = "example-packet";

            // Configure the mock server to respond to a POST request
            _server
                .Given(Request.Create().WithPath(endpoint).UsingPost()
                    .WithParam("packet", packet))
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody("{\"success\": true}"));

            var link = new Link(_server.Url);

            // Act
            var result = await link.SendRequestAsync<object>(ReqType.POST, endpoint, packet);

            // Assert
            Assert.IsTrue(result);

            // Verify that the server received the expected POST request
            var log = _server.LogEntries.ToList().FirstOrDefault();
            Assert.IsNotNull(log);  // Ensure there is at least one log entry
            Assert.AreEqual("POST", log.RequestMessage.Method);
            Assert.AreEqual($"{_server.Url}{endpoint}?packet={packet}", log.RequestMessage.Url);
        }

        [TestMethod]
        public async Task SendRequestAsync_PutRequest_ShouldReturnTrue()
        {
            // Arrange
            var endpoint = "/test-put";
            var packet = "update-packet";

            // Configure the mock server to respond to a PUT request
            _server
                .Given(Request.Create().WithPath(endpoint).UsingPut()
                    .WithParam("packet", packet))
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody("{\"success\": true}"));

            var link = new Link(_server.Url);

            // Act
            var result = await link.SendRequestAsync<object>(ReqType.PUT, endpoint, packet);

            // Assert
            Assert.IsTrue(result);

            // Verify that the server received the expected PUT request
            var log = _server.LogEntries.ToList().FirstOrDefault();
            Assert.IsNotNull(log);  // Ensure there is at least one log entry
            Assert.AreEqual("PUT", log.RequestMessage.Method);
            Assert.AreEqual($"{_server.Url}{endpoint}?packet={packet}", log.RequestMessage.Url);
        }

        [TestMethod]
        public async Task SendRequestAsync_DeleteRequest_ShouldReturnTrue()
        {
            // Arrange
            var endpoint = "/test-delete";
            var packet = "delete-packet";

            // Configure the mock server to respond to a DELETE request
            _server
                .Given(Request.Create().WithPath(endpoint).UsingDelete()
                    .WithParam("packet", packet))
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody("{\"success\": true}"));

            var link = new Link(_server.Url);

            // Act
            var result = await link.SendRequestAsync<object>(ReqType.DELETE, endpoint, packet);

            // Assert
            Assert.IsTrue(result);

            // Verify that the server received the expected DELETE request
            var log = _server.LogEntries.ToList().FirstOrDefault();
            Assert.IsNotNull(log);  // Ensure there is at least one log entry
            Assert.AreEqual("DELETE", log.RequestMessage.Method);
            Assert.AreEqual($"{_server.Url}{endpoint}?packet={packet}", log.RequestMessage.Url);
        }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void SendRequestAsync_InvalidUrl_ShouldThrowException()
        {
            var invalidUrl = "invalidurl";
            var link = new Link(invalidUrl);
        }

    }
}
