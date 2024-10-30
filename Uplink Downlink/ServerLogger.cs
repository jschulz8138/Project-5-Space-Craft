﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplink_Downlink
{
    public class ServerLogger : AppLogger
    {
        public ServerLogger(string filePath) : base(filePath) { }

        public override void LogMetadata(string method, string endpoint, int statusCode)
        {
            WriteToFile($"{DateTime.Now}, SERVER REQUEST, Method: {method}, Endpoint: {endpoint}, Status: {statusCode}");
        }

        public override void LogLogin(string userId)
        {
            WriteToFile($"{DateTime.Now}, SERVER LOGIN, User: {userId}");
        }

        public override void LogLogout(string userId)
        {
            WriteToFile($"{DateTime.Now}, SERVER LOGOUT, User: {userId}");
        }

        public override void LogAuthentication(string userId, bool success)
        {
            string status = success ? "SUCCESS" : "FAILURE";
            WriteToFile($"{DateTime.Now}, SERVER AUTHENTICATION, User: {userId}, Status: {status}");
        }
    }
}