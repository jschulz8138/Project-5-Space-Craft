using LinkServer.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Xunit;

namespace UD_FilterTests
{
    public class AuthenticateFilterTests
    {
        [Fact]
        public void OnActionExecuting_ShouldReturnUnauthorized_WhenUsernameIsNotInSession()
        {
            // Arrange
            var httpContext = new DefaultHttpContext
            {
                Session = new MockSession() // Attach the mock session
            };

            // No "username" is set in the session, simulating an unauthenticated user

            var routeData = new RouteData();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = routeData,
                ActionDescriptor = new ControllerActionDescriptor()
            };

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new object()
            );

            var filter = new AuthenticateFilter();

            // Act
            filter.OnActionExecuting(actionExecutingContext);

            // Assert
            var result = actionExecutingContext.Result as UnauthorizedObjectResult;
            Xunit.Assert.NotNull(result); // Ensure the result is Unauthorized
            Xunit.Assert.Equal("User is not authenticated.", result.Value); // Check the error message
        }

        [Fact]
        public void OnActionExecuting_ShouldAllowRequest_WhenUsernameIsInSession()
        {
            // Arrange
            var httpContext = new DefaultHttpContext
            {
                Session = new MockSession() // Attach the mock session
            };

            // Add "username" to the session to simulate an authenticated user
            httpContext.Session.SetString("username", "user1");

            var routeData = new RouteData();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = routeData,
                ActionDescriptor = new ControllerActionDescriptor()
            };

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new object()
            );

            var filter = new AuthenticateFilter();

            // Act
            filter.OnActionExecuting(actionExecutingContext);

            // Assert
            Xunit.Assert.Null(actionExecutingContext.Result); // Ensure the Result is null (no interruption)
        }

        [Fact]
        public void OnActionExecuted_ShouldReturnBadRequest_WhenConditionIsMet()
        {
            // Arrange
            var httpContext = new DefaultHttpContext
            {
                Session = new MockSession() // Attach the mock session
            };

            var routeData = new RouteData();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = routeData,
                ActionDescriptor = new ControllerActionDescriptor()
            };

            var actionExecutedContext = new ActionExecutedContext(
                actionContext,
                new List<IFilterMetadata>(),
                new object()
            );

            var filter = new AuthenticateFilter();

            // Act
            filter.OnActionExecuted(actionExecutedContext);

            // Assert
            var result = actionExecutedContext.Result as BadRequestObjectResult;
            Xunit.Assert.NotNull(result); // Ensure the result is BadRequest
            Xunit.Assert.Equal("Something went wrong, OnActionExecuted() should not be called.", result.Value); // Check the error message
        }

        public class MockSession : ISession
        {
            private readonly Dictionary<string, byte[]> _sessionStorage = new();

            public IEnumerable<string> Keys => _sessionStorage.Keys;

            public string Id => Guid.NewGuid().ToString();

            public bool IsAvailable => true;

            public void Clear() => _sessionStorage.Clear();

            public void Remove(string key) => _sessionStorage.Remove(key);

            public void Set(string key, byte[] value) => _sessionStorage[key] = value;

            public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);

            public void SetString(string key, string value) =>
                Set(key, System.Text.Encoding.UTF8.GetBytes(value));

            public string? GetString(string key)
            {
                return TryGetValue(key, out var value) ? System.Text.Encoding.UTF8.GetString(value) : null;
            }

            public Task LoadAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task CommitAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }
        }
    }
    }