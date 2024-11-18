using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LinkServer.Controllers;
using Uplink_Downlink;
using System.ComponentModel;
using LinkServer;
using Payload_Ops;

namespace UD_ControllerTests
{
    [TestClass]
    public class UplinkControllerTests
    {
        private UplinkController _uplinkController;
        private Spaceship _spaceship = new Spaceship();
        private AppLogger _logger = new ServerLogger("test.txt");

        [TestInitialize]
        public void Setup()
        {
            _uplinkController = new UplinkController(_logger, _spaceship);

            var httpContext = new DefaultHttpContext();
            _uplinkController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TestMethod]
        public void SendUplink_ShouldReturnSuccessMessage()
        {
            // Arrange
            string packet = "{\"Date\":\"2024-11-07T19:26:34.0177707-05:00\",\"FunctionType\":\"FunctionStub\",\"Command\":\"TestingData\",\"PacketCRC\":\"some_crc_value\"}";


            // Act
            var result = _uplinkController.SendUplink(packet) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Uplink processed successfully.", result?.Value?.ToString());
        }

        [TestMethod]
        public void SendUplink_ShouldReturnFailureMessage()
        {
            // Arrange
            string packet = "{\"Date\":,\"FunctionType\":\"FunctionStub\",\"Command\":\"TestingData\",\"PacketCRC\":\"some_crc_value\"}";

            // Act
            var result = _uplinkController.SendUplink(packet) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Uplink failed to process.", result?.Value?.ToString());
        }
    }

    [TestClass]
    public class AuthenticationControllerTests
    {
        private AuthenticatorController _controller = new AuthenticatorController(new ServerLogger("test.txt"));

        [TestInitialize]
        public void TestInitialize()
        {
            // Set up an in-memory session for testing
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new TestSession();  // Implement a simple TestSession class inheriting ISession
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TestMethod]
        public void TestLogin_SuccessfulAuthentication()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };

            // Act
            var result = _controller.Login(credentials) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Authenticated", result.Value);
            Assert.IsTrue(AuthenticatorController.IsAuthenticated("user1"));
        }

        [TestMethod]
        public void TestLogin_InvalidPassword()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "wrongpassword" };

            // Act
            var result = _controller.Login(credentials);

            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
            var unauthorizedResult = result as UnauthorizedObjectResult;

            Assert.IsNotNull(unauthorizedResult);
            Assert.AreEqual("Invalid credentials.", unauthorizedResult.Value);
        }

        [TestMethod]
        public void TestLogin_TooManyAttempts()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user2", Password = "wrongpassword" };
            for (int i = 0; i < 3; i++) _controller.Login(credentials);

            // Act
            var result = _controller.Login(credentials) as UnauthorizedObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Too many login attempts.", result.Value);
        }

        [TestMethod]
        public void TestLogout_SuccessfulLogout()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };
            _controller.Login(credentials);

            // Act
            var result = _controller.Logout(credentials) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Logged out", result.Value);
            Assert.IsFalse(AuthenticatorController.IsAuthenticated("user1"));
        }

        [TestMethod]
        public void TestLogout_UserNotLoggedIn()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user2", Password = "password2" };

            // Act
            var result = _controller.Logout(credentials) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("User is not logged in.", result.Value);
        }

        private class TestSession : ISession
        {
            private Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();

            public bool IsAvailable => true;
            public string Id => Guid.NewGuid().ToString();
            public IEnumerable<string> Keys => _sessionStorage.Keys;

            public void Clear() => _sessionStorage.Clear();

            public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

            public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

            public void Remove(string key) => _sessionStorage.Remove(key);

            public void Set(string key, byte[] value) => _sessionStorage[key] = value;

            public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);
        }
    }

}