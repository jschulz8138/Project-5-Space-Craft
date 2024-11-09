using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uplink_Downlink;


namespace Uplink_Downlink_Tests
{
    namespace Uplink_Downlink_Tests
    {
        [TestClass]
        public class ServerLoggerUnitTests
        {
            private ServerLogger _serverLogger;
            private string _logFilePath;

            [TestInitialize]
            public void Setup()
            {
                _logFilePath = "server_logs_test.txt";
                _serverLogger = new ServerLogger(_logFilePath);
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
            public void LogLogin_ShouldWriteLoginMessageToFile()
            {
                // Arrange
                string userId = "testUser";

                // Act
                _serverLogger.LogLogin(userId);

                // Assert
                string lastLine = ReadLastLineFromFile(_logFilePath);
                Assert.IsTrue(lastLine.Contains("SERVER LOGIN"));
                Assert.IsTrue(lastLine.Contains($"User: {userId}"));
            }

            [TestMethod]
            public void LogLogout_ShouldWriteLogoutMessageToFile()
            {
                // Arrange
                string userId = "testUser";

                // Act
                _serverLogger.LogLogout(userId);

                // Assert
                string lastLine = ReadLastLineFromFile(_logFilePath);
                Assert.IsTrue(lastLine.Contains("SERVER LOGOUT"));
                Assert.IsTrue(lastLine.Contains($"User: {userId}"));
            }

            [TestMethod]
            public void LogAuthentication_Success_ShouldWriteSuccessMessageToFile()
            {
                // Arrange
                string userId = "testUser";
                bool success = true;

                // Act
                _serverLogger.LogAuthentication(userId, success);

                // Assert
                string lastLine = ReadLastLineFromFile(_logFilePath);
                Assert.IsTrue(lastLine.Contains("SERVER AUTHENTICATION"));
                Assert.IsTrue(lastLine.Contains("Status: SUCCESS"));
                Assert.IsTrue(lastLine.Contains($"User: {userId}"));
            }

            [TestMethod]
            public void LogAuthentication_Failure_ShouldWriteFailureMessageToFile()
            {
                // Arrange
                string userId = "testUser";
                bool success = false;

                // Act
                _serverLogger.LogAuthentication(userId, success);

                // Assert
                string lastLine = ReadLastLineFromFile(_logFilePath);
                Assert.IsTrue(lastLine.Contains("SERVER AUTHENTICATION"));
                Assert.IsTrue(lastLine.Contains("Status: FAILURE"));
                Assert.IsTrue(lastLine.Contains($"User: {userId}"));
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

}
