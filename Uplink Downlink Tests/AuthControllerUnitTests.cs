using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LinkServer.Controllers;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;


namespace LinkServer.Controllers.Tests
{
    [TestClass]
    public class AuthenticatorControllerTests
    {
        private AuthenticatorController _controller = null!;

        // Initializes variables for each test
        [TestInitialize]
        public void Setup()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new MockHttpSession(); 

            _controller = new AuthenticatorController(null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };
        }

        [TestCleanup]
        public void Teardown()
        {
            // Clear the authenticated users collection
            var authenticatedUsersField = typeof(AuthenticatorController)
                .GetField("_authenticatedUsers", BindingFlags.Static | BindingFlags.NonPublic);
            if (authenticatedUsersField != null)
            {
                var authenticatedUsers = authenticatedUsersField.GetValue(null) as IDictionary<string, bool>;
                authenticatedUsers?.Clear();
            }

            // Reset login attempts
            var loginAttemptsField = typeof(AuthenticatorController)
                .GetField("_loginAttempts", BindingFlags.NonPublic | BindingFlags.Instance);
            if (loginAttemptsField != null)
            {
                loginAttemptsField.SetValue(_controller, (short)0);
            }
        }

        // LOGIN FUNCTION TESTS
        // Successful test should be a successful authentication
        [TestMethod]
        public void Login_WithValidCredentials_ShouldAuthenticateUser()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };

            // Act
            var result = _controller.Login(credentials) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Successfully authenticated.", result.Value);
            Assert.IsTrue(AuthenticatorController.IsAuthenticated("user1"));
        }

        // Unsuccessful test should return unauthorized
        [TestMethod]
        public void Login_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "invalidpassword" };

            // Act
            var result = _controller.Login(credentials) as UnauthorizedObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Authentication failed, invalid user credentials.", result.Value);
            Assert.IsFalse(AuthenticatorController.IsAuthenticated("user1"));
        }

        [TestMethod]
        public void Login_WithExceededAttempts_ShouldReturnUnauthorized()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "wrongpassword" };

            // Act - Fail login 3 times
            for (int i = 0; i < 3; i++)
            {
                _controller.Login(credentials);
            }

            // Act - Fourth login attempt
            var result = _controller.Login(credentials) as UnauthorizedObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Too many login attempts.", result.Value);
        }

        [TestMethod]
        public void Login_WhenAlreadyAuthenticated_ShouldReturnOk()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };

            // Log the user in initially
            _controller.Login(credentials);

            // Act - Attempt to login again
            var result = _controller.Login(credentials) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Already authenticated.", result.Value);
        }

            // LOGOUT FUNCTION TESTS
        // Attempts to logout, should return a successful logged out response
        [TestMethod]
        public void Logout_WhenUserIsAuthenticated_ShouldLogoutUser()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };
            // Log the user in first
            _controller.Login(credentials);

            // Act
            var logoutResult = _controller.Logout(credentials) as OkObjectResult;

            // Assert
            Assert.IsNotNull(logoutResult);
            Assert.AreEqual("Logged out", logoutResult.Value);
            Assert.IsFalse(AuthenticatorController.IsAuthenticated("user1"));
        }

        // Attempts to log out with credentials that are not in the system, should say that the user isn't logged in
        [TestMethod]
        public void Logout_WhenUserIsNotAuthenticated_ShouldReturnBadRequest()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user3", Password = "password3" };

            // Act
            var result = _controller.Logout(credentials) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("User is not logged in", result.Value);
        }

        [TestMethod]
        public void Logout_WithNonexistentUser_ShouldReturnBadRequest()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "nonexistent", Password = "password" };

            // Act
            var result = _controller.Logout(credentials) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("User is not logged in", result.Value);
        }

        [TestMethod]
        public void Logout_AfterMultipleLoginAttempts_ShouldLogoutUser()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };

            // Multiple login attempts (some correct, some incorrect)
            _controller.Login(credentials); // successful login
            _controller.Login(new UserCredentials { Username = "user1", Password = "wrongpassword" });

            // Act - Logout
            var result = _controller.Logout(credentials) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Logged out", result.Value);
            Assert.IsFalse(AuthenticatorController.IsAuthenticated("user1"));
        }

            // IS_AUTHENTICATED TEST
        // Tests the isAuthenticated function (Successful Test)
        [TestMethod]
        public void IsAuthenticated_TrueWhenAuthenticated()
        {
            // Arrange
            var credentials = new UserCredentials { Username = "user1", Password = "password1" };
            // Log the user in first
            _controller.Login(credentials);

            // Act
            var result = AuthenticatorController.IsAuthenticated("user1");

            // Assert
            Assert.IsTrue(result);
        }

        // Tests the isAuthenticated function (Unsuccessful Test)
        [TestMethod]
        public void IsAuthenticated_FalseWhenNotAuthenticated()
        {
            // Act
            var result = AuthenticatorController.IsAuthenticated("user1");

            // Assert
            Assert.IsFalse(result);
        }
    }

    // Created a class for a mock http session, since the login function wouldn't work without a http session
    public class MockHttpSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new();

        public void Clear() => _sessionStorage.Clear();

        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public void Set(string key, byte[] value) => _sessionStorage[key] = value;

        public byte[]? Get(string key) => _sessionStorage.TryGetValue(key, out var value) ? value : null;

        public void Remove(string key) => _sessionStorage.Remove(key);

        public string Id => "mocked-session-id";

        public bool IsAvailable => true;

        public void LoadAsync() { /* No-op for mock */ }

        public Task LoadAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task CommitAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
        {
            // Check if the key exists in the session storage
            if (_sessionStorage.TryGetValue(key, out var tempValue))
            {
                value = tempValue;
                return true;
            }
            else
            {
                value = null; // Set to null if key not found
                return false;
            }
        }
    }
}