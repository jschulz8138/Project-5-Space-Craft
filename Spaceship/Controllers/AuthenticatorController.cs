using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Uplink_Downlink;

namespace Spaceship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticatorController : ControllerBase
    {
        private readonly AppLogger _logger;

        public AuthenticatorController(AppLogger logger)
        {
            _logger = logger;
        }

        private static readonly Dictionary<string, string> _users = new()
        {
            {"user1", "password1" },
            {"user2", "password2" }
        };
        private short _loginAttempts = 0;

        // Static dictionary to store logged-in sessions (as an example)
        private static readonly ConcurrentDictionary<string, bool> _authenticatedUsers = new();

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {

            if (_loginAttempts >= 3)
            {
                _logger.LogAuthentication(credentials.Username, success: false);
                return Unauthorized("Too many login attempts.");
            }

            if (_authenticatedUsers.ContainsKey(credentials.Username))
            {
                _logger.LogAuthentication(credentials.Username, success: true);
                return Ok("Already authenticated.");
            }
            bool usernameExists = _users.ContainsKey(credentials.Username);
            bool passwordMatches = usernameExists && _users[credentials.Username] == credentials.Password;
            
            if (usernameExists && passwordMatches)
            {
                _authenticatedUsers[credentials.Username] = true;
                HttpContext.Session.SetString("username", credentials.Username);
                
                _logger.LogAuthentication(credentials.Username, success: true);
                return Ok("Authenticated");
            }
            else
            {
                if (!usernameExists && !passwordMatches)
                    _logger.LogAuthentication(credentials.Username, success: false, reason: "both username and password are invalid");
                else if (!usernameExists)
                    _logger.LogAuthentication(credentials.Username, success: false, reason: "username is invalid");
                else
                    _logger.LogAuthentication(credentials.Username, success: false, reason: "password is invalid");

                return Unauthorized("Invalid credentials");
            }
        }


        [HttpPost("logout")]
        public IActionResult Logout([FromBody] UserCredentials credentials)
        {
            if (_authenticatedUsers.ContainsKey(credentials.Username))
            {
                _authenticatedUsers.TryRemove(credentials.Username, out _);
                HttpContext.Session.Remove("username");

                _logger.LogLogout(credentials.Username);
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