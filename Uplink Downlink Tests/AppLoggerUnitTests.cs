using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uplink_Downlink;


namespace Uplink_Downlink_Tests
{
    [TestClass]
    public class AppLoggerUnitTests
    {
        private TestableAppLogger _appLogger;
        private string _logFilePath;

        [TestInitialize]
        public void Setup()
        {
            _logFilePath = "app_logs_test.txt";
            _appLogger = new TestableAppLogger(_logFilePath); // Using TestableAppLogger to access WriteToFile
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
        public void WriteToFile_ShouldWriteContentToFile()
        {
            // Arrange
            string content = "Test log content";

            // Act
            _appLogger.TestWriteToFile(content); // Using the accessible method for testing

            // Assert
            string lastLine = ReadLastLineFromFile(_logFilePath);
            Assert.AreEqual(content, lastLine);
        }

        [TestMethod]
        public void WriteToFile_ShouldHandleExceptionsGracefully()
        {
            // Arrange
            string content = "Test log content for exception handling";

            // Creating a restricted file path to simulate a failure
            var restrictedFilePath = "C:\\restricted_logs_test.txt";
            var restrictedLogger = new TestableAppLogger(restrictedFilePath);

            try
            {
                // Act
                restrictedLogger.TestWriteToFile(content);

                // Assert
                // We expect no exceptions to propagate, and the log should handle this gracefully.
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail("Exception should not propagate outside WriteToFile.");
            }
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

        // Testable class to expose protected WriteToFile method for testing purposes
        private class TestableAppLogger : AppLogger
        {
            public TestableAppLogger(string filePath) : base(filePath) { }

            public override void LogMetadata(string method, string endpoint, int statusCode) { }
            public override void LogLogin(string userId) { }
            public override void LogLogout(string userId) { }
            public override void LogAuthentication(string userId, bool success, string reason) { }

            public override void LogPacketReceived(string packetData){ }

            // Expose WriteToFile specifically for testing purposes
            public void TestWriteToFile(string content)
            {
                WriteToFile(content);
            }
        }
    }
}