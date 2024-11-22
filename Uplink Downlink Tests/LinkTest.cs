using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using Uplink_Downlink;

namespace Uplink_Downlink_Tests
{
    [TestClass]
    public class LinkTests
    {
        private Mock<RestClient>?_mockClient;
        private Link?_link;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<RestClient>();
            _link = new Link("https://example.com", _mockClient.Object);
        }

        [TestMethod]
        public void Constructor_ValidUrl_ShouldInitialize()
        {
            Assert.IsNotNull(_link);
        }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void Constructor_InvalidUrl_ShouldThrowUriFormatException()
        {
            new Link("invalid-url");
        }

        [TestMethod]
        [DataRow(ReqType.GET, true)]
        [DataRow(ReqType.POST, true)]
        [DataRow(ReqType.PUT, false)]
        [DataRow(ReqType.DELETE, false)]
        public async Task SendRequestAsync_ValidRequest_ShouldReturnExpectedResult(ReqType reqType, bool expectedResult)
        {
            // Arrange
            SetupMockClientResponse(reqType, expectedResult);

            // Act
            var result = await _link!.SendRequestAsync<object>(reqType, "/endpoint", "{}");

            // Assert
            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public async Task SendRequestAsync_UnsuccessfulResponse_ShouldReturnFalse()
        {
            // Arrange
            SetupMockClientResponse(ReqType.GET, false);

            // Act
            var result = await _link!.SendRequestAsync<object>(ReqType.GET, "/endpoint", "{}");

            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SendRequestAsync_InvalidReqType_ShouldThrowArgumentException()
        {
            await _link!.SendRequestAsync<object>((ReqType)999, "/endpoint", "{}");
        }

        private void SetupMockClientResponse(ReqType reqType, bool isSuccessful)
        {
            var mockResponse = new Mock<RestResponse>();
            mockResponse.SetupGet(r => r.IsSuccessful).Returns(isSuccessful);

            switch (reqType)
            {
                case ReqType.GET:
                    _mockClient?.Setup(c => c.GetAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse.Object);
                    break;
                case ReqType.POST:
                    _mockClient?.Setup(c => c.PostAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse.Object);
                    break;
                case ReqType.PUT:
                    _mockClient?.Setup(c => c.PutAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse.Object);
                    break;
                case ReqType.DELETE:
                    _mockClient?.Setup(c => c.DeleteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse.Object);
                    break;
            }
        }
    }
}