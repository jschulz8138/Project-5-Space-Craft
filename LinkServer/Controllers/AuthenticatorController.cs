using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace LinkServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticatorController : ControllerBase
    {
        private static readonly List<UserCredentials> _users =
        [
            new UserCredentials { Username = "user1", Password = "password1" },
            new UserCredentials { Username = "user2", Password = "password2" }
        ];

        private short _loginAttempts = 0;
        private static readonly ConcurrentDictionary<string, bool> _authenticatedUsers = new();

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            if (_loginAttempts >= 3)
                return Unauthorized("Too many login attempts.");

            if (_authenticatedUsers.ContainsKey(credentials.Username))
                return Ok("Already authenticated.");

            var user = _users.FirstOrDefault(u => u.Username == credentials.Username && u.Password == credentials.Password);
            if (user != null)
            {
                _authenticatedUsers[credentials.Username] = true;
                HttpContext.Session.SetString("username", credentials.Username);
                return Ok("Successfully authenticated.");
            }

            _loginAttempts++;
            return Unauthorized("Authentication failed, invalid user credentials.");
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] UserCredentials credentials)
        {
            if (_authenticatedUsers.ContainsKey(credentials.Username))
            {
                _authenticatedUsers.TryRemove(credentials.Username, out _);
                HttpContext.Session.Remove("username");
                return Ok("Logged out");
            }

            return BadRequest("User is not logged in");
        }

        public static bool IsAuthenticated(string username)
        {
            return _authenticatedUsers.ContainsKey(username);
        }
    }

    public class UserCredentials
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}