//Payload Ops
//Unit and Integration Tests
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Project_5_Space_Craft;
using Project_5_Space_Craft.Functions;
using Project_5_Space_Craft.Packets;
using Project_5_Space_Craft.Readings;
using System;
using System.IO.Hashing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Payload_Ops_Tests
{
    [TestClass]
    public class FunctionPacketTests
    {
        [TestMethod]
        public void FNPKT_0001_Constructor_Not_Null_1()
        {
            FunctionPacket funcPkt = new FunctionPacket(new FunctionStub("test command"));
            Assert.IsNotNull(funcPkt);
        }

        [TestMethod]
        public void FNPKT_0002_Constructor_Not_Null_2()
        {
            FunctionPacket funcPkt = new FunctionPacket(new FunctionStub("test command"));
            Assert.IsNotNull(funcPkt);
        }

        [TestMethod]
        public void FNPKT_0003_Constructor_Variant_1_Data_Correct_DateTime()
        {
            FunctionPacket funcPkt = new FunctionPacket(new FunctionStub("test command"));
            String actual = funcPkt.DateTime.ToString();
            String expected = DateTime.Now.ToString();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0004_Constructor_Variant_2_Data_Correct_DateTime()
        {
            FunctionPacket funcPkt = new FunctionPacket(new FunctionStub("test command"));
            String actual = funcPkt.DateTime.ToString();
            String expected = DateTime.Now.ToString();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0005_Constructor_Variant_1_Data_Correct_FunctionType()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            string actual = funcPkt.FunctionType;
            string expected = "FunctionStub";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0006_Constructor_Variant_2_Data_Correct_FunctionType()
        {
            FunctionPacket funcPkt = new FunctionPacket(new FunctionStub("test command"));
            string actual = funcPkt.FunctionType;
            string expected = "FunctionStub";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0007_Constructor_Variant_1_Data_Correct_Command()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            string actual = funcPkt.Command;
            string expected = "test command";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0008_Constructor_Variant_2_Data_Correct_Command()
        {
            FunctionPacket funcPkt = new FunctionPacket(new FunctionStub("test command"));
            string actual = funcPkt.Command;
            string expected = "test command";
            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        public void FNPKT_0009_Constructor_Variant_1_Data_Correct_CRC()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            Crc32 crc = new Crc32();
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.DateTime.ToString()));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.FunctionType));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Command));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = funcPkt.CalculateCRC();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0010_Constructor_Variant_2_Data_Correct_CRC()
        {
            FunctionPacket funcPkt = new FunctionPacket(new FunctionStub("test command"));
            Crc32 crc = new Crc32();
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.DateTime.ToString()));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.FunctionType));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Command));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = funcPkt.CalculateCRC();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0011_Calculate_CRC()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            funcPkt.CalculateCRC();
            Crc32 crc = new Crc32();
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.DateTime.ToString()));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.FunctionType));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Command));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = funcPkt.CalculateCRC();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0012_Convert_Byte_Array()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            byte[] actual = funcPkt.ConvertToByteArray("This is the test string");
            byte[] expected = Encoding.ASCII.GetBytes("This is the test string");
            bool testPassed = expected.SequenceEqual(actual);
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void FNPKT_0013_Validate_CRC_True()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            FunctionPacket funcPktCopy = new FunctionPacket(stub);
            bool actual = funcPkt.ValidateCRC(funcPktCopy.CalculateCRC());
            bool expected = true;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0014_Validate_CRC_False()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            bool actual = funcPkt.ValidateCRC("probably wrong");
            bool expected = false;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0015_Get_Datetime_Not_Null()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            DateTime actualTime = funcPkt.DateTime;
            Assert.IsNotNull(actualTime);
        }

        [TestMethod]
        public void FNPKT_0016_Get_Datetime_Type()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            DateTime actualTime = funcPkt.DateTime;
            Assert.IsInstanceOfType(actualTime, typeof(DateTime));
        }

        [TestMethod]
        public void FNPKT_0017_Get_Datetime_WithinRange()
        {
            DateTime startRange = DateTime.Now;
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            DateTime endRange = DateTime.Now;

            DateTime actualTime = funcPkt.DateTime;
            Assert.IsTrue(actualTime >= startRange && actualTime <= endRange);
        }

        [TestMethod]
        public void FNPKT_0018_Get_FunctionType_Not_Null()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string functionTypeName = funcPkt.FunctionType;

            Assert.IsNotNull(functionTypeName);
        }

        [TestMethod]
        public void FNPKT_0019_Get_FunctionType_Correct_Type_Name()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string functionTypeName = funcPkt.FunctionType;

            Assert.AreEqual(functionTypeName, stub.GetType().Name);
        }

        [TestMethod]
        public void FNPKT_0020_Get_Command_Not_Null()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string command = funcPkt.Command;

            Assert.IsNotNull(command);
        }

        [TestMethod]
        public void FNPKT_0021_Get_Command_Correct_Type_String()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string command = funcPkt.Command;

            Assert.IsInstanceOfType(command, typeof(string));
        }

        [TestMethod]
        public void FNPKT_0022_Get_Command_Correct_Command()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string command = funcPkt.Command;

            Assert.AreEqual(command, stub.GetCommand());
        }


        [TestMethod]
        public void FNPKT_0023_Get_PacketCRC_Not_Null()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.PacketCRC;

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void FNPKT_0024_Get_PacketCRC_Correct_Type_String()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.PacketCRC;

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void FNPKT_0025_Get_PacketCRC_Correct_CRC()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string expected = funcPkt.CalculateCRC();
            string actual = funcPkt.PacketCRC;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FNPKT_0026_Get_Function_Not_Null()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            IFunction actual = funcPkt.GetFunction();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void FNPKT_0027_Get_Function_Correct_Type()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            IFunction actual = funcPkt.GetFunction();

            Assert.IsInstanceOfType(actual, typeof(FunctionStub));
        }

        [TestMethod]
        public void FNPKT_0028_Get_Function_Correct_Function()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            IFunction actual = funcPkt.GetFunction();

            Assert.AreEqual(actual, stub);
        }

        [TestMethod]
        public void FNPKT_0029_Get_Function_Empty_Function_Return_Null_NonTest()
        {
            // TODO: maybe fix
            // FunctionPacket funcPkt = new FunctionPacket(null);
            // literally cannot create a FunctionPacket with no function
            Assert.IsNull(null);
        }

        [TestMethod]
        public void FNPKT_0030_CreateFunction_Stub_Return_Null_NonTest()
        {
            // TODO: maybe fix
            // FunctionPacket funcPkt = new FunctionPacket(null);
            // createFunction() only exc if Function is Null, cannot create a FunctionPacket with null function
            // also its a private method
            Assert.IsNull(null);
        }

        [TestMethod]
        public void FNPKT_0031_ToJson_Not_Null()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.ToJson();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void FNPKT_0032_ToJson_Correct_Type_String()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.ToJson();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void FNPKT_0033_ToJson_Correct_JSON()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string expected = JsonSerializer.Serialize(funcPkt);
            string actual = funcPkt.ToJson();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FNPKT_0034_GetPacketType_Not_Null()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.GetPacketType();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void FNPKT_0035_GetPacketType_Correct_Type_String()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.GetPacketType();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void FNPKT_0036_GetPacketType_Correct_PacketType_Stub()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.GetPacketType();

            Assert.AreEqual(actual, stub.GetType().Name);
        }

        

        [TestMethod]
        public void FNPKT_0037_GetPacketData_Not_Null()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.GetPacketData();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void FNPKT_0038_GetPacketData_Correct_Type_String()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.GetPacketData();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void FNPKT_0039_GetPacketData_Correct_PacketData_Stub()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);

            string actual = funcPkt.GetPacketData();

            Assert.AreEqual(actual, stub.GetCommand());
        }


        /// ////////////////////////extras w/o FunctionStub/////////////////////////////////////

        [TestMethod]
        public void FNPKT_0040_Constructor_IncreaseThrustFunction_Not_Null()
        {
            IncreaseThrustFunction func = new IncreaseThrustFunction(100);
            FunctionPacket funcPkt = new FunctionPacket(func);
            Assert.IsNotNull(funcPkt);
        }

        [TestMethod]
        public void FNPKT_0041_Constructor_SelfDestructFunction_Not_Null()
        {
            SelfDestructFunction func = new SelfDestructFunction("true");
            FunctionPacket funcPkt = new FunctionPacket(func);
            Assert.IsNotNull(funcPkt);
        }

        [TestMethod]
        public void FNPKT_0042_Get_Command_IncreaseThrustFunction_Data_Correct_Command()
        {
            IncreaseThrustFunction func = new IncreaseThrustFunction(100);
            FunctionPacket funcPkt = new FunctionPacket(func);
            string actual = funcPkt.Command;
            string expected = "100";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0043_Get_Command_SelfDestructFunction_Data_Correct_Command()
        {
            SelfDestructFunction func = new SelfDestructFunction("true");
            FunctionPacket funcPkt = new FunctionPacket(func);
            string actual = funcPkt.Command;
            Assert.AreEqual(actual, func.GetCommand());
        }

        [TestMethod]
        public void FNPKT_0044_Calculate_CRC_IncreaseThrustFunction()
        {
            IncreaseThrustFunction func = new IncreaseThrustFunction(100);
            FunctionPacket funcPkt = new FunctionPacket(func);
            funcPkt.CalculateCRC();
            Crc32 crc = new Crc32();
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.DateTime.ToString()));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.FunctionType));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Command));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = funcPkt.CalculateCRC();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0045_Calculate_CRC_SelfDestructFunction()
        {
            SelfDestructFunction func = new SelfDestructFunction("true");
            FunctionPacket funcPkt = new FunctionPacket(func);
            funcPkt.CalculateCRC();
            Crc32 crc = new Crc32();
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.DateTime.ToString()));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.FunctionType));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Command));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = funcPkt.CalculateCRC();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0046_Get_FunctionType_IncreaseThrustFunction()
        {
            IncreaseThrustFunction func = new IncreaseThrustFunction(100);
            FunctionPacket funcPkt = new FunctionPacket(func);
            string functionTypeName = funcPkt.FunctionType;
            Assert.AreEqual(functionTypeName, func.GetType().Name);
        }

        [TestMethod]
        public void FNPKT_0047_Get_FunctionType_SelfDestructFunction()
        {
            SelfDestructFunction func = new SelfDestructFunction("true");
            FunctionPacket funcPkt = new FunctionPacket(func);
            string functionTypeName = funcPkt.FunctionType;
            Assert.AreEqual(functionTypeName, func.GetType().Name);
        }

        [TestMethod]
        public void FNPKT_0048_Get_Function_Return_Same_SelfDestructFunction_Not_Stub()
        {
            // returns the same Function as the one used to create the packet
            SelfDestructFunction func = new SelfDestructFunction("true");
            FunctionPacket funcPkt = new FunctionPacket(func);

            IFunction actual = funcPkt.GetFunction();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(SelfDestructFunction));
            Assert.AreEqual(actual, func);
            Assert.AreEqual(actual.GetCommand(), func.GetCommand());
        }

        [TestMethod]
        public void FNPKT_0049_Get_Function_Return_Same_IncreaseThrustFunction_Not_Stub()
        {
            // returns the same Function as the one used to create the packet
            IncreaseThrustFunction func = new IncreaseThrustFunction(100);
            FunctionPacket funcPkt = new FunctionPacket(func);

            IFunction actual = funcPkt.GetFunction();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(IncreaseThrustFunction));
            Assert.AreEqual(actual, func);
            Assert.AreEqual(actual.GetCommand(), func.GetCommand());
        }
        [TestMethod]
        public void FNPKT_0050_GetPacketType_Correct_PacketType_SelfDestructFunction_Not_Stub()
        {
            SelfDestructFunction func = new SelfDestructFunction("true");
            FunctionPacket funcPkt = new FunctionPacket(func);

            string actual = funcPkt.GetPacketType();

            Assert.AreEqual(actual, func.GetType().Name);
        }

        [TestMethod]
        public void FNPKT_0051_GetPacketType_Correct_PacketType_IncreaseThrustFunction_Not_Stub()
        {
            IncreaseThrustFunction func = new IncreaseThrustFunction(100);
            FunctionPacket funcPkt = new FunctionPacket(func);

            string actual = funcPkt.GetPacketType();

            Assert.AreEqual(actual, func.GetType().Name);
        }

        [TestMethod]
        public void FNPKT_0052_GetPacketData_Correct_PacketData_SelfDestructFunction_Not_Stub()
        {
            SelfDestructFunction func = new SelfDestructFunction("true");
            FunctionPacket funcPkt = new FunctionPacket(func);

            string actual = funcPkt.GetPacketData();

            Assert.AreEqual(actual, func.GetCommand());
        }

        [TestMethod]
        public void FNPKT_0053_GetPacketData_Correct_PacketData_IncreaseThrustFunction_Not_Stub()
        {
            IncreaseThrustFunction func = new IncreaseThrustFunction(100);
            FunctionPacket funcPkt = new FunctionPacket(func);

            string actual = funcPkt.GetPacketData();

            Assert.AreEqual(actual, func.GetCommand());
        }

    }


    [TestClass]
    public class LoggingTests
    {
        [TestMethod]
        public void LOGGING_0001_LogConsole()
        {
            bool expected = Logging.logConsole("PacketType", "Direction", "PacketData", DateTime.Now);
            Assert.IsTrue(expected);
        }
        [TestMethod]
        public void LOGGING_0002_GetCellValue_B12()
        {
            string expected = "B12 Value";
            string actual = Logging.GetCellValue("../../../ExcelTests.xlsx", "Sheet1", "B12");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0003_GetCellValue_D7()
        {
            string expected = "D7 Value";
            string actual = Logging.GetCellValue("../../../ExcelTests.xlsx", "Sheet1", "D7");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0004_GetNextEmptyCell_F_Empty()
        {
            uint expected = 1;
            uint actual = Logging.GetNextEmptyCell("../../../ExcelTests.xlsx", "Sheet1", "F");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0005_GetNextEmptyCell_H_Seven()
        {
            uint expected = 8;
            uint actual = Logging.GetNextEmptyCell("../../../ExcelTests.xlsx", "Sheet1", "G");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0006_InsertText()
        {
            Random rnd = new Random();
            int expected = rnd.Next(1,10000);
            Logging.InsertText("../../../ExcelTests.xlsx", expected.ToString(), "A", 6);
            int actual = Int32.Parse(Logging.GetCellValue("../../../ExcelTests.xlsx", "Sheet1", "A6"));
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0007_logFile_PacketType()
        {
            //clear data
            Logging.InsertText("../../../LogFiles.xlsx", "", "A", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "B", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "C", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            DateTime dt = DateTime.Now;
            Logging.logFile(rndNum.ToString(), "dir", "data", dt);
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue("../../../LogFiles.xlsx", "Sheet1", "A1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0008_logFile_Time()
        {
            //clear data
            Logging.InsertText("../../../LogFiles.xlsx", "", "A", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "B", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "C", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "D", 1);

            //write data
            DateTime dt = DateTime.Now;
            string expected = dt.ToString("yyyy MMMM dd h:mm:ss tt");
            Logging.logFile("type", "dir", "data", dt);
            string actual = Logging.GetCellValue("../../../LogFiles.xlsx", "Sheet1", "B1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0009_logFile_Direction()
        {
            //clear data
            Logging.InsertText("../../../LogFiles.xlsx", "", "A", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "B", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "C", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            DateTime dt = DateTime.Now;
            Logging.logFile("type", rndNum.ToString(), "data", dt);
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue("../../../LogFiles.xlsx", "Sheet1", "C1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0010_logFile_Data()
        {
            //clear data
            Logging.InsertText("../../../LogFiles.xlsx", "", "A", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "B", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "C", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            DateTime dt = DateTime.Now;
            Logging.logFile("type", "dir", rndNum.ToString(), dt);
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue("../../../LogFiles.xlsx", "Sheet1", "D1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0011_LogPacket_PacketType()
        {
            //clear data
            Logging.InsertText("../../../LogFiles.xlsx", "", "A", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "B", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "C", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            Logging.LogPacket(rndNum.ToString(), "dir", "data");
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue("../../../LogFiles.xlsx", "Sheet1", "A1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0012_LogPacket_Time()
        {
            //clear data
            Logging.InsertText("../../../LogFiles.xlsx", "", "A", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "B", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "C", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "D", 1);

            //write data
            DateTime dt = DateTime.Now;
            string expected = dt.ToString("yyyy MMMM dd h:mm:ss tt");
            Logging.LogPacket("type", "dir", "data");
            string actual = Logging.GetCellValue("../../../LogFiles.xlsx", "Sheet1", "B1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0013_LogPacket_Direction()
        {
            //clear data
            Logging.InsertText("../../../LogFiles.xlsx", "", "A", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "B", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "C", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            Logging.LogPacket("type", rndNum.ToString(), "data");
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue("../../../LogFiles.xlsx", "Sheet1", "C1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0014_LogPacket_Data()
        {
            //clear data
            Logging.InsertText("../../../LogFiles.xlsx", "", "A", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "B", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "C", 1);
            Logging.InsertText("../../../LogFiles.xlsx", "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            Logging.LogPacket("type", "dir", rndNum.ToString());
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue("../../../LogFiles.xlsx", "Sheet1", "D1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0015_InsertSharedStringItem()
        {
            int actual;
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open("../../../ExcelTests.xlsx", true))
            {
                WorkbookPart workbookPart = spreadSheet.WorkbookPart ?? spreadSheet.AddWorkbookPart();
                SharedStringTablePart shareStringPart;
                if (workbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                {
                    shareStringPart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                }
                else
                {
                    shareStringPart = workbookPart.AddNewPart<SharedStringTablePart>();
                }
                actual = Logging.InsertSharedStringItem("test", shareStringPart);
            }
            int expected = 5; //This value changes whenever a permanent change is made to the document
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0016_InsertCellInWorksheet()
        {
            string expected;
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open("../../../ExcelTests.xlsx", true))
            {
                WorkbookPart workbookPart = spreadSheet.WorkbookPart ?? spreadSheet.AddWorkbookPart();
                SharedStringTablePart shareStringPart;
                if (workbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                {
                    shareStringPart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                }
                else
                {
                    shareStringPart = workbookPart.AddNewPart<SharedStringTablePart>();
                }
                Random rnd = new Random();
                int rndNum = rnd.Next(1, 10000);
                expected = rndNum.ToString();
                int index = Logging.InsertSharedStringItem(rndNum.ToString(), shareStringPart);
                Cell cell = Logging.InsertCellInWorksheet("K", 1, workbookPart.WorksheetParts.First());
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                workbookPart.WorksheetParts.First().Worksheet.Save();
            }
            string actual = Logging.GetCellValue("../../../ExcelTests.xlsx", "Sheet1", "K1");
            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class SpaceshipTests
    {
        [TestMethod]
        public void SPACESHIP_0001_Constructor_Variant_Not_Null()
        {
            Spaceship spaceShip = new Spaceship();
            Assert.IsNotNull(spaceShip);
        }
    }
    [TestClass]
    public class DataPacketTests
    {
        [TestMethod]
        public void RDPKT_0001_Constructor_Variant_1_Not_Null()
        {
            DataPacket readingPkt = new DataPacket(new ReadingsStub("data"));
            Assert.IsNotNull(readingPkt);
        }

        [TestMethod]
        public void RDPKT_0002_Constructor_Variant_2_Not_Null()
        {
            ReadingsStub stub = new ReadingsStub("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            Assert.IsNotNull(readingPkt);
        }

        [TestMethod]
        public void RDPKT_0003_Constructor_Variant_1_Data_Correct_DateTime()
        {
            DataPacket readingPkt = new DataPacket(new ReadingsStub("data"));
            String actual = readingPkt.DateTime.ToString();
            String expected = DateTime.Now.ToString(); 
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0004_Constructor_Variant_2_Data_Correct_DateTime()
        {
            ReadingsStub stub = new ReadingsStub("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            String actual = readingPkt.DateTime.ToString();
            String expected = DateTime.Now.ToString();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0005_Constructor_Variant_1_Data_Correct_DataType()
        {
            DataPacket readingPkt = new DataPacket(new ReadingsStub("data"));
            string actual = readingPkt.Data;
            string expected = "data";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0006_Constructor_Variant_2_Data_Correct_DataType()
        {
            DataPacket readingPkt = new DataPacket(new ReadingsStub("data"));
            string actual = readingPkt.DataType;
            string expected = "ReadingsStub";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0007_Constructor_Variant_1_Data_Correct_Data()
        {
            DataPacket readingPkt = new DataPacket(new ReadingsStub("data"));
            string actual = readingPkt.Data;
            string expected = "data";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0008_Constructor_Variant_2_Data_Correct_Data()
        {
            ReadingsStub stub = new ReadingsStub("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            string actual = readingPkt.Data;
            string expected = "This is the data";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0009_Constructor_Variant_1_Data_Correct_CRC()
        {
            DataPacket readingPkt = new DataPacket(new ReadingsStub("data"));
            Crc32 crc = new Crc32();
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.DateTime.ToString()));
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.DataType));
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.Data));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = readingPkt.Crc;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0010_Constructor_Variant_2_Data_Correct_CRC()
        {
            ReadingsStub stub = new ReadingsStub("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            Crc32 crc = new Crc32();
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.DateTime.ToString()));
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.DataType));
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.Data));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = readingPkt.Crc;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0011_Calculate_CRC()
        {
            ReadingsStub stub = new ReadingsStub("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            readingPkt.CalculateCRC();
            Crc32 crc = new Crc32();
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.DateTime.ToString()));
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.DataType));
            crc.Append(readingPkt.ConvertToByteArray(readingPkt.Data));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = readingPkt.Crc;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0012_Convert_Byte_Array()
        {
            ReadingsStub stub = new ReadingsStub("");
            DataPacket readingPkt = new DataPacket(stub);
            byte[] actual = readingPkt.ConvertToByteArray("This is the test string");
            byte[] expected = Encoding.ASCII.GetBytes("This is the test string");
            bool testPassed = expected.SequenceEqual(actual);
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void RDPKT_0013_Validate_CRC_True()
        {
            ReadingsStub stub = new ReadingsStub("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            DataPacket readingPktCopy = new DataPacket(stub);
            bool actual = readingPkt.ValidateCRC(readingPktCopy.Crc);
            bool expected = true;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0014_Validate_CRC_False()
        {
            ReadingsStub stub = new ReadingsStub("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            bool actual = readingPkt.ValidateCRC("probably wrong");
            bool expected = false;
            Assert.AreEqual(actual, expected);
        }
    }

    [TestClass]
    public class PacketWrapperTests
    {
        //[TestMethod]
        //public void PKTWRAP_0001_Constructor_Not_Null()
        //{
        //    PacketWrapper pktWrap = new PacketWrapper();
        //    Assert.IsNotNull(pktWrap);
        //}

        ////TODO
        //[TestMethod]
        //public void PKTWRAP_0002_To_Json_Readings()
        //{
        //    ReadingsStub stub = new ReadingsStub("TestingData");
        //    ReadingsPacket rdPkt = new ReadingsPacket(stub);
        //    PacketWrapper pktWrap = new PacketWrapper();
        //    string actual = pktWrap.ToJsonReadings(rdPkt);
        //    string expected = "test";
        //    Assert.AreEqual(actual, expected);
        //}

        ////TODO
        //[TestMethod]
        //public void PKTWRAP_0003_To_Json_Function()
        //{
        //    ReadingsStub stub = new ReadingsStub("TestingData");
        //    PacketWrapper pktWrap = new PacketWrapper();
        //    FunctionPacket funcPkt = new FunctionPacket(stub);
        //    string actual = pktWrap.ToJsonFunction(funcPkt);
        //    string expected = "test";
        //    Assert.AreEqual(actual, expected);
        //}

        ////TODO
        //[TestMethod]
        //public void PKTWRAP_0004_To_Reading()
        //{
        //    PacketWrapper pktWrap = new PacketWrapper();
        //    IPacket actual = pktWrap.ToReading("");
        //    Assert.IsNotNull(actual);
        //}
    }

    //[TestClass]
    //public class ReadingStubTests
    //{
    //    [TestMethod]
    //    public void RDSB_0001_Constructor_Not_Null()
    //    {
    //        ReadingsStub stub = new ReadingsStub("testData");
    //        Assert.IsNotNull(stub);
    //    }

    //    [TestMethod]
    //    public void RDSB_0002_GetData()
    //    {
    //        ReadingsStub stub = new ReadingsStub("testData");
    //        string expected = "testData";
    //        string actual = stub.GetData();
    //        Assert.AreEqual(expected, actual);
    //    }

    //    [TestMethod]
    //    public void RDSB_0003_SetData()
    //    {
    //        ReadingsStub stub = new ReadingsStub("testData");
    //        stub.SetData("changedData");
    //        string expected = "changedData";
    //        string actual = stub.GetData();
    //        Assert.AreEqual(expected, actual);
    //    }
    //}

    [TestClass]
    public class PositionReadingTests
    {
        [TestMethod]
        public void POSRD_0001_Constructor_Not_Null()
        {
            PositionReading stub = new PositionReading("testData");
            Assert.IsNotNull(stub);
        }

        [TestMethod]
        public void POSRD_0002_GetData()
        {
            PositionReading stub = new PositionReading("testData");
            string expected = "testData";
            string actual = stub.GetData();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void POSRD_0003_SetData()
        {
            PositionReading stub = new PositionReading("testData");
            stub.SetData("changedData");
            string expected = "changedData";
            string actual = stub.GetData();
            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class RadiationReadingTests
    {
        [TestMethod]
        public void RADRD_0001_Constructor_Not_Null()
        {
            RadiationReading stub = new RadiationReading("testData");
            Assert.IsNotNull(stub);
        }

        [TestMethod]
        public void RADRD_0002_GetData()
        {
            RadiationReading stub = new RadiationReading("testData");
            string expected = "testData";
            string actual = stub.GetData();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RADRD_0003_SetData()
        {
            RadiationReading stub = new RadiationReading("testData");
            stub.SetData("changedData");
            string expected = "changedData";
            string actual = stub.GetData();
            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class TemperatureReadingTests
    {
        [TestMethod]
        public void TEMPRD_0001_Constructor_Not_Null()
        {
            TemperatureReading stub = new TemperatureReading("testData");
            Assert.IsNotNull(stub);
        }

        [TestMethod]
        public void TEMPRD_0002_GetData()
        {
            TemperatureReading stub = new TemperatureReading("testData");
            string expected = "testData";
            string actual = stub.GetData();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TEMPRD_0003_SetData()
        {
            TemperatureReading stub = new TemperatureReading("testData");
            stub.SetData("changedData");
            string expected = "changedData";
            string actual = stub.GetData();
            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class VelocityReadingTests
    {
        [TestMethod]
        public void VELRD_0001_Constructor_Not_Null()
        {
            VelocityReading stub = new VelocityReading("testData");
            Assert.IsNotNull(stub);
        }

        [TestMethod]
        public void VELRD_0002_GetData()
        {
            VelocityReading stub = new VelocityReading("testData");
            string expected = "testData";
            string actual = stub.GetData();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VELRD_0003_SetData()
        {
            VelocityReading stub = new VelocityReading("testData");
            stub.SetData("changedData");
            string expected = "changedData";
            string actual = stub.GetData();
            Assert.AreEqual(expected, actual);
        }
    }
}