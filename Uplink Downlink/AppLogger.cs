﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Uplink_Downlink
{
    public abstract class AppLogger
    {
        protected readonly string logFilePath;

        protected AppLogger(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public abstract void LogMetadata(string method, string endpoint, int statusCode);
        public abstract void LogLogin(string userId);
        public abstract void LogLogout(string userId);
        public abstract void LogAuthentication(string userId, bool success);

        protected virtual void WriteToFile(string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}