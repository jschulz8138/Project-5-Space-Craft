﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Uplink_Downlink;

namespace LinkServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticatorController : ControllerBase
    {
        private readonly AppLogger _logger;

        // inject AppLogger through constructor
        public AuthenticatorController(AppLogger logger)
        {
            _logger = logger;
        }

        private static readonly Dictionary<string, string> _users = new()
        {
            {"user1", "password1" },
            {"user2", "password2" }
        };

        // Static dictionary to store logged-in sessions (as an example)
        private static readonly ConcurrentDictionary<string, bool> _authenticatedUsers = new();


        //I believe the route is api/Authenticator/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {

            // log the login attempt
            _logger.LogLogin(credentials.Username);

            if (_authenticatedUsers.ContainsKey(credentials.Username))
            {
                // log that the user is already authenticated
                _logger.LogAuthentication(credentials.Username, success: true);

                return Ok("Already authenticated");
            }
            else if (_users.TryGetValue(credentials.Username, out var password) && password == credentials.Password)
            {
                _authenticatedUsers[credentials.Username] = true;
                HttpContext.Session.SetString("username", credentials.Username);

                // log successful authentication
                _logger.LogAuthentication(credentials.Username, success: true);

                return Ok("Authenticated");
            }

            // log failed authentication attempt
            _logger.LogAuthentication(credentials.Username, success: false);

            return Unauthorized("Invalid credentials");
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] UserCredentials credentials)
        {
            if (_authenticatedUsers.ContainsKey(credentials.Username))
            {
                _authenticatedUsers.TryRemove(credentials.Username, out _);
                HttpContext.Session.Remove("username");

                // log the logout event
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