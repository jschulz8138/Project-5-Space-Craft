using LinkServer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Uplink_Downlink;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LinkServer.IntegrationTests
{
    [TestClass]
    public class AuthenticatorControllerIntegrationTests
    {
        private AuthenticatorController _authController;
        private string _logFilePath;

        [TestInitialize]
        public void Setup()
        {
            _logFilePath = "authenticator_logs_test.txt";
            var mockSession = new MockHttpSession(); // Use mock session for testing
            _authController = new AuthenticatorController(new ServerLogger(_logFilePath))
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { Session = mockSession }
                }
            };
        }

        [TestCleanup]
        public void Teardown()
        {
            if (File.Exists(_logFilePath))
            {
                File.Delete(_logFilePath);
            }
        }

        [TestMethod]
        public void Login_ShouldLogLoginAndAuthentication()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };

            // Act
            _authController.Login(credentials);

            // Assert: Check log entries for login and authentication
            string lastLine = ReadLastLineFromFile(_logFilePath);
            Assert.IsTrue(lastLine.Contains("SERVER AUTHENTICATION"));
            Assert.IsTrue(lastLine.Contains("User: user1"));
            Assert.IsTrue(lastLine.Contains("Status: SUCCESS"));
        }

        [TestMethod]
        public void Logout_ShouldLogLogoutEvent()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };

            // Act
            _authController.Login(credentials);
            _authController.Logout(credentials);

            // Assert: Check that logout event was logged
            string lastLine = ReadLastLineFromFile(_logFilePath);
            Assert.IsTrue(lastLine.Contains("SERVER LOGOUT"));
            Assert.IsTrue(lastLine.Contains("User: user1"));
        }

        [TestMethod]
        public void Login_ShouldLogFailedAuthentication()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "wrongpassword" };

            // Act
            _authController.Login(credentials);

            // Assert: Check log entry for failed authentication
            string lastLine = ReadLastLineFromFile(_logFilePath);
            Assert.IsTrue(lastLine.Contains("SERVER AUTHENTICATION"));
            Assert.IsTrue(lastLine.Contains("User: user1"));
            Assert.IsTrue(lastLine.Contains("Status: FAILURE"));
        }

        private string ReadLastLineFromFile(string filePath)
        {
            using (var file = new StreamReader(filePath))
            {
                string line;
                string lastLine = null;
                while ((line = file.ReadLine()) != null)
                {
                    lastLine = line;
                }
                return lastLine;
            }
        }
    }

}

// Mock session class for integration testing
public class MockHttpSession : ISession
{
    private readonly Dictionary<string, byte[]> _sessionStorage = new();

    public bool IsAvailable => true;
    public string Id => "mocked-session-id";
    public IEnumerable<string> Keys => _sessionStorage.Keys;

    public void Clear() => _sessionStorage.Clear();
    public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    public void Remove(string key) => _sessionStorage.Remove(key);
    public void Set(string key, byte[] value) => _sessionStorage[key] = value;
    public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);
}