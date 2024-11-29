using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uplink_Downlink;
using System.Threading.Tasks;

namespace Uplink_Downlink_Tests
{
    [TestClass]
    public class ConnectionManagerTests
    {
        private Mock<ILink> _mockLink;
        private ConnectionManager _connectionManager;

        [TestInitialize]
        public void Setup()
        {
            // Step_1: Mocking the ILink interface
            _mockLink = new Mock<ILink>();

            // Step_2: Injecting the mocked ILink into ConnectionManager
            _connectionManager = new ConnectionManager(_mockLink.Object);
        }

        //CMT-001 verifying that the constructor is initializing the IsAuthenticated to false

        [TestMethod]
        public void Constructor_ShouldInitialize_IsAuthenticatedToFalse()
        {
            // Assert that IsAuthenticated is false 
            Assert.IsNotNull(_connectionManager);
            Assert.IsFalse(_connectionManager.IsAuthenticated, "Expected IsAuthenticated to be false on initialization.");
        }

        //CMT-002 Verifying that AuthenticateAsync sets IsAuthenticated to true when authentication is successful

        [TestMethod]
        public async Task AuthenticateAsync_ShouldReturnTrue_WhenAuthenticationSucceeds()
        {
            
            string authPacket = "{\"username\":\"test\",\"password\":\"password\"}";
            _mockLink.Setup(link => link.SendRequestAsync<bool>(ReqType.POST, "/api/Authentication/login", authPacket))
                     .ReturnsAsync(true); // Simulate success

            
            var result = await _connectionManager.AuthenticateAsync(authPacket);

            
            Assert.IsTrue(result, "Expected AuthenticateAsync to return true on successful authentication.");
            Assert.IsTrue(_connectionManager.IsAuthenticated, "Expected IsAuthenticated to be true after successful authentication.");
        }

        //CMT-003 Veryfying that the AuthenticateAsync sets IsAuthenticated to false when authentication fails.

        [TestMethod]
        public async Task AuthenticateAsync_ShouldReturnFalse_WhenAuthenticationFails()
        {
            
            string authPacket = "{\"username\":\"test\",\"password\":\"wrongpassword\"}";
            _mockLink.Setup(link => link.SendRequestAsync<bool>(ReqType.POST, "/api/Authentication/login", authPacket))
                     .ReturnsAsync(false); // Simulate failure

           
            var result = await _connectionManager.AuthenticateAsync(authPacket);

           
            Assert.IsFalse(result, "Expected AuthenticateAsync to return false on failed authentication.");
            Assert.IsFalse(_connectionManager.IsAuthenticated, "Expected IsAuthenticated to remain false after failed authentication.");
        }
    }
}

