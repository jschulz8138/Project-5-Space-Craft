using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Uplink_Downlink;

namespace LinkServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticatorController : ControllerBase
    {
        private readonly AppLogger _logger;
        private static readonly ConcurrentDictionary<string, short> _loginAttempts = new();
        private static readonly ConcurrentDictionary<string, bool> _authenticatedUsers = new();

        public AuthenticatorController(AppLogger logger)
        {
            _logger = logger;
        }

        private static readonly Dictionary<string, string> _users = new()
        {
            {"user1", "password1" },
            {"user2", "password2" }
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            if (_loginAttempts.TryGetValue(credentials.Username, out var attempts) && attempts >= 3)
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
                _loginAttempts.TryRemove(credentials.Username, out _);
                HttpContext.Session.SetString("username", credentials.Username);
                

                _logger.LogAuthentication(credentials.Username, success: true);
                return Ok("Authenticated");
            }
            else
            {
                if (!usernameExists)
                {
                    _logger.LogAuthentication(credentials.Username, success: false, reason: "username is invalid");
                }
                else
                {
                    _loginAttempts.AddOrUpdate(credentials.Username, 1, (key, count) => (short)(count + 1));
                    _logger.LogAuthentication(credentials.Username, success: false, reason: "invalid password");
                }

                return Unauthorized("Invalid credentials.");
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

            return BadRequest("User is not logged in.");

        }

        public static bool IsAuthenticated(string username) => _authenticatedUsers.ContainsKey(username);     
    }

    public class UserCredentials
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}