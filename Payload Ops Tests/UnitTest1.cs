//Payload Ops
//Unit and Integration Tests
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Payload_Ops;
using Payload_Ops.Functions;
using Payload_Ops.Packets;
using Payload_Ops.Readings;
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
            String actual = funcPkt.Date.ToString();
            String expected = DateTime.Now.ToString();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0004_Constructor_Variant_2_Data_Correct_DateTime()
        {
            FunctionPacket funcPkt = new FunctionPacket(new FunctionStub("test command"));
            String actual = funcPkt.Date.ToString();
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
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Date.ToString()));
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
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Date.ToString()));
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
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Date.ToString()));
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
    }
    [TestClass]
    public class LoggingTests
    {
        const string TEST_FILE = "../../../../Payload Ops Tests/ExcelTests.xlsx";
        const string LOG_FILE = "../../../../Payload Ops Tests/LogFiles.xlsx";

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
            string actual = Logging.GetCellValue(TEST_FILE, "Sheet1", "B12");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0003_GetCellValue_D7()
        {
            string expected = "D7 Value";
            string actual = Logging.GetCellValue(TEST_FILE, "Sheet1", "D7");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0004_GetNextEmptyCell_F_Empty()
        {
            uint expected = 1;
            uint actual = Logging.GetNextEmptyCell(TEST_FILE, "Sheet1", "F");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0005_GetNextEmptyCell_H_Seven()
        {
            uint expected = 8;
            uint actual = Logging.GetNextEmptyCell(TEST_FILE, "Sheet1", "G");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0006_InsertText()
        {
            Random rnd = new Random();
            int expected = rnd.Next(1,10000);
            Logging.InsertText(TEST_FILE, expected.ToString(), "A", 6);
            int actual = Int32.Parse(Logging.GetCellValue(TEST_FILE, "Sheet1", "A6"));
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0007_logFile_PacketType()
        {
            //clear data
            Logging.InsertText(LOG_FILE, "", "A", 1);
            Logging.InsertText(LOG_FILE, "", "B", 1);
            Logging.InsertText(LOG_FILE, "", "C", 1);
            Logging.InsertText(LOG_FILE, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            DateTime dt = DateTime.Now;
            Logging.logFile(rndNum.ToString(), "dir", "data", dt);
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(LOG_FILE, "Sheet1", "A1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0008_logFile_Time()
        {
            //clear data
            Logging.InsertText(LOG_FILE, "", "A", 1);
            Logging.InsertText(LOG_FILE, "", "B", 1);
            Logging.InsertText(LOG_FILE, "", "C", 1);
            Logging.InsertText(LOG_FILE, "", "D", 1);

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
            Logging.InsertText(LOG_FILE, "", "A", 1);
            Logging.InsertText(LOG_FILE, "", "B", 1);
            Logging.InsertText(LOG_FILE, "", "C", 1);
            Logging.InsertText(LOG_FILE, "", "D", 1);

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
            Logging.InsertText(LOG_FILE, "", "A", 1);
            Logging.InsertText(LOG_FILE, "", "B", 1);
            Logging.InsertText(LOG_FILE, "", "C", 1);
            Logging.InsertText(LOG_FILE, "", "D", 1);

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
            Logging.InsertText(LOG_FILE, "", "A", 1);
            Logging.InsertText( LOG_FILE, "", "B", 1);
            Logging.InsertText(LOG_FILE, "", "C", 1);
            Logging.InsertText(LOG_FILE, "", "D", 1);

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
            Logging.InsertText(LOG_FILE, "", "A", 1);
            Logging.InsertText(LOG_FILE, "", "B", 1);
            Logging.InsertText(LOG_FILE, "", "C", 1);
            Logging.InsertText(LOG_FILE, "", "D", 1);

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
            Logging.InsertText(LOG_FILE, "", "A", 1);
            Logging.InsertText(LOG_FILE, "", "B", 1);
            Logging.InsertText(LOG_FILE, "", "C", 1);
            Logging.InsertText(LOG_FILE, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            Logging.LogPacket("type", rndNum.ToString(), "data");
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(LOG_FILE, "Sheet1", "C1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0014_LogPacket_Data()
        {
            //clear data
            Logging.InsertText(LOG_FILE, "", "A", 1);
            Logging.InsertText(LOG_FILE, "", "B", 1);
            Logging.InsertText(LOG_FILE, "", "C", 1);
            Logging.InsertText(LOG_FILE, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            Logging.LogPacket("type", "dir", rndNum.ToString());
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(LOG_FILE, "Sheet1", "D1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0015_InsertSharedStringItem()
        {
            int actual;
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(TEST_FILE, true))
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
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(TEST_FILE, true))
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
            string actual = Logging.GetCellValue(TEST_FILE, "Sheet1", "K1");
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

        [TestMethod]
        public void RDPKT_0015_ToJson_Not_Null()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.ToJson();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void RDPKT_0016_ToJson_Correct_Type_String()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.ToJson();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void RDPKT_0017_ToJson_Correct_JSON()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string expected = JsonSerializer.Serialize(dataPkt);
            string actual = dataPkt.ToJson();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RDPKT_0018_GetPacketType_Not_Null()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketType();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void RDPKT_0019_GetPacketType_Correct_Type_String()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketType();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void RDPKT_0020_Correct_PacketType_Stub()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketType();

            Assert.AreEqual(actual, stub.GetType().Name);
        }

        [TestMethod]
        public void RDPKT_0021_GetPacketData_Not_Null()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketData();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void RDPKT_0022_GetPacketData_Correct_Type_String()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketData();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void RDPKT_0023_GetPacketData_Correct_PacketData_Stub()
        {
            ReadingsStub stub = new ReadingsStub("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketData();

            Assert.AreEqual(actual, stub.GetData());
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
    [TestClass]
    public class SelfDestructTestClass
    {
        [TestMethod]
        public void SD_Authorize()
        {
            //Arange
            string expected = "True";
            SelfDestructFunction SDTest = new SelfDestructFunction("true");
            //Act
            string actual = SDTest.GetCommand();
            //Assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void SD_Unauthorized()
        {
            //Arange
            string expected = "False";
            SelfDestructFunction SDTest = new SelfDestructFunction("false");
            //Act
            string actual = SDTest.GetCommand();
            //Assert
            Assert.AreEqual(actual, expected);
        }
    }
    [TestClass]
    public class IncreaseThrustTests
    {
        [TestMethod]
        public void IncreaseThrustTest()
        {
            //Arange
            string expected = "500";
            IncreaseThrustFunction increaseThrust = new IncreaseThrustFunction("500");
            //Act
            string actual = increaseThrust.GetCommand();
            //Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
