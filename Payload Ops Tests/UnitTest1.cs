//Payload Ops
//Unit and Integration Tests
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Payload_Ops;
using Payload_Ops.Packets;
using System.IO.Hashing;
using System.Text;
using System.Text.Json;
using Spaceship;

namespace Payload_Ops_Tests
{
    [TestClass]
    public class FunctionPacketTests
    {
        [TestMethod]
        public void FNPKT_0001_Constructor_Not_Null_1()
        {
            FunctionPacket funcPkt = new FunctionPacket(new IncreaseThrustFunction("10"));
            Assert.IsNotNull(funcPkt);
        }

        [TestMethod]
        public void FNPKT_0002_Constructor_Not_Null_2()
        {
            FunctionPacket funcPkt = new FunctionPacket(new IncreaseThrustFunction("15"));
            Assert.IsNotNull(funcPkt);
        }

        [TestMethod]
        public void FNPKT_0003_Constructor_Variant_1_Data_Correct_DateTime()
        {
            FunctionPacket funcPkt = new FunctionPacket(new IncreaseThrustFunction("20"));
            String actual = funcPkt.Date.ToString();
            String expected = DateTime.Now.ToString();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0004_Constructor_Variant_2_Data_Correct_DateTime()
        {
            FunctionPacket funcPkt = new FunctionPacket(new IncreaseThrustFunction("30"));
            String actual = funcPkt.Date.ToString();
            String expected = DateTime.Now.ToString();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0005_Constructor_Variant_1_Data_Correct_FunctionType()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("10");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            string actual = funcPkt.FunctionType;
            string expected = "IncreaseThrustFunction";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0006_Constructor_Variant_2_Data_Correct_FunctionType()
        {
            FunctionPacket funcPkt = new FunctionPacket(new IncreaseThrustFunction("10"));
            string actual = funcPkt.FunctionType;
            string expected = "IncreaseThrustFunction";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0007_Constructor_Variant_1_Data_Correct_Command()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("26");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            string actual = funcPkt.Command;
            string expected = "26";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0008_Constructor_Variant_2_Data_Correct_Command()
        {
            FunctionPacket funcPkt = new FunctionPacket(new IncreaseThrustFunction("50"));
            string actual = funcPkt.Command;
            string expected = "50";
            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        public void FNPKT_0009_Constructor_Variant_1_Data_Correct_CRC()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("72");
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
            FunctionPacket funcPkt = new FunctionPacket(new IncreaseThrustFunction("95"));
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
            IncreaseThrustFunction stub = new IncreaseThrustFunction("34");
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
            IncreaseThrustFunction stub = new IncreaseThrustFunction("4");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            byte[] actual = funcPkt.ConvertToByteArray("This is the test string");
            byte[] expected = Encoding.ASCII.GetBytes("This is the test string");
            bool testPassed = expected.SequenceEqual(actual);
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void FNPKT_0013_Validate_CRC_True()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("6");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            FunctionPacket funcPktCopy = new FunctionPacket(stub);
            bool actual = funcPkt.ValidateCRC(funcPktCopy.CalculateCRC());
            bool expected = true;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0014_Validate_CRC_False()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("7");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            bool actual = funcPkt.ValidateCRC("probably wrong");
            bool expected = false;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0015_PacketCRC()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("7");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            string actual = funcPkt.PacketCRC;
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void FNPKT_0016_GetFunction()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("7");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            Assert.AreSame(stub, funcPkt.GetFunction());
        }
        [TestMethod]
        public void FNPKT_0017_ToJson()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("7");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            Assert.IsNotNull(funcPkt.ToJson());
        }
        [TestMethod]
        public void FNPKT_0018_GetPacketType()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("7");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            string actual = funcPkt.GetPacketType();
            string expected = "IncreaseThrustFunction";
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void FNPKT_0019_GetPacketData()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("7");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            string actual = funcPkt.GetPacketData();
            string expected = "7";
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void FNPKT_0020_Constructor_Variant_2_Good()
        {
            FunctionPacket funcPkt = new FunctionPacket("date", "type", "command", "crc");
            Assert.IsNotNull(funcPkt);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FNPKT_0021_Constructor_Variant_2_Date_Exception()
        {
            FunctionPacket funcPkt = new FunctionPacket(" ", "type", "command", "crc");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FNPKT_0022_Constructor_Variant_2_Type_Exception()
        {
            FunctionPacket funcPkt = new FunctionPacket("date", " ", "command", "crc");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FNPKT_0023_Constructor_Variant_2_Command_Exception()
        {
            FunctionPacket funcPkt = new FunctionPacket("date", "type", " ", "crc");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FNPKT_0024_Constructor_Variant_2_Crc_Exception()
        {
            FunctionPacket funcPkt = new FunctionPacket("date", "type", "command", " ");
        }
        [TestMethod]
        public void FNPKT_0025_CreateFunction_SelfDestruct()
        {
            SelfDestructFunction stub = new SelfDestructFunction(true);
            FunctionPacket funcPkt = new FunctionPacket(stub);
            funcPkt.createFunction();
            Assert.IsNotNull(funcPkt);
        }
        [TestMethod]
        public void FNPKT_0025_CreateFunction_IncreaseThrust()
        {
            IncreaseThrustFunction stub = new IncreaseThrustFunction("3");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            funcPkt.createFunction();
            Assert.IsNotNull(funcPkt);
        }
    }
    [TestClass]
    public class LoggingTests
    {
        const string ExcelTestsFilePath = "../../../ExcelTests.xlsx";
        const string ExcelLogFilesPath = "../../../LogFiles.xlsx";
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
            string actual = Logging.GetCellValue(ExcelTestsFilePath, "Sheet1", "B12");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0003_GetCellValue_D7()
        {
            string expected = "D7 Value";
            string actual = Logging.GetCellValue(ExcelTestsFilePath, "Sheet1", "D7");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0004_GetNextEmptyCell_F_Empty()
        {
            uint expected = 1;
            uint actual = Logging.GetNextEmptyCell(ExcelTestsFilePath, "Sheet1", "F");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0005_GetNextEmptyCell_H_Seven()
        {
            uint expected = 8;
            uint actual = Logging.GetNextEmptyCell(ExcelTestsFilePath, "Sheet1", "G");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0006_InsertText()
        {
            Random rnd = new Random();
            int expected = rnd.Next(1,10000);
            Logging.InsertText(ExcelTestsFilePath, expected.ToString(), "A", 6);
            int actual = Int32.Parse(Logging.GetCellValue(ExcelTestsFilePath, "Sheet1", "A6"));
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0007_logFile_PacketType()
        {
            //clear data
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            DateTime dt = DateTime.Now;
            Logging.logFile(rndNum.ToString(), "dir", "data", dt);
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "A1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0008_logFile_Time()
        {
            //clear data
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);

            //write data
            DateTime dt = DateTime.Now;
            string expected = dt.ToString("yyyy MMMM dd h:mm:ss tt");
            Logging.logFile("type", "dir", "data", dt);
            string actual = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "B1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0009_logFile_Direction()
        {
            //clear data
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            DateTime dt = DateTime.Now;
            Logging.logFile("type", rndNum.ToString(), "data", dt);
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "C1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0010_logFile_Data()
        {
            //clear data
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            DateTime dt = DateTime.Now;
            Logging.logFile("type", "dir", rndNum.ToString(), dt);
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0011_LogPacket_PacketType()
        {
            //clear data
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            Logging.LogPacket(rndNum.ToString(), "dir", "data");
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "A1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0012_LogPacket_Time()
        {
            //clear data
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);

            //write data
            DateTime dt = DateTime.Now;
            string expected = dt.ToString("yyyy MMMM dd h:mm:ss tt");
            Logging.LogPacket("type", "dir", "data");
            string actual = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "B1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0013_LogPacket_Direction()
        {
            //clear data
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            Logging.LogPacket("type", rndNum.ToString(), "data");
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "C1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0014_LogPacket_Data()
        {
            //clear data
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);

            //write data
            Random rnd = new Random();
            int rndNum = rnd.Next(1, 10000);
            Logging.LogPacket("type", "dir", rndNum.ToString());
            string expected = rndNum.ToString();
            string actual = Logging.GetCellValue(ExcelLogFilesPath  , "Sheet1", "D1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0015_InsertSharedStringItem()
        {
            int actual;
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(ExcelTestsFilePath, true))
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
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(ExcelTestsFilePath, true))
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
            string actual = Logging.GetCellValue(ExcelTestsFilePath, "Sheet1", "K1");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void LOGGING_0017_CheckAndCreateExcelFile_Exists()
        {
            bool expected = Logging.CheckAndCreateExcelFile(ExcelTestsFilePath);
            Assert.IsTrue(expected);
        }
        [TestMethod]
        public void LOGGING_0018_CheckAndCreateExcelFile_DoesNotExist()
        {
            System.IO.File.Delete("../../../DeleteFile.xlsx");
            bool expected = Logging.CheckAndCreateExcelFile("../../../DeleteFile.xlsx");
            Assert.IsFalse(expected);
        }
        [TestMethod]
        public void LOGGING_0019_LogPacketStressTest()
        {
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "A", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "A", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 3);
            VelocityReading velReading = new VelocityReading("30");
            RadiationReading radReading = new RadiationReading("40");
            PositionReading posReading = new PositionReading("30, 20");
            DataPacket pkt1 = new DataPacket(velReading);
            DataPacket pkt2 = new DataPacket(radReading);
            DataPacket pkt3 = new DataPacket(posReading);
            Logging.LogPacket(pkt1.GetPacketType(), "Outbound", pkt1.GetPacketData());
            Logging.LogPacket(pkt2.GetPacketType(), "Outbound", pkt2.GetPacketData());
            Logging.LogPacket(pkt3.GetPacketType(), "Outbound", pkt3.GetPacketData());
            string actual1 = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D1");
            string actual2 = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D2");
            string actual3 = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D3");
            string expected1 = "30";
            string expected2 = "40";
            string expected3 = "30, 20";
            Assert.AreEqual(actual1, expected1);
            Assert.AreEqual(actual2, expected2);
            Assert.AreEqual(actual3, expected3);
        }
    }

    [TestClass]
    public class SpaceshipTests
    {
        const string ExcelLogFilesPath = "../../../LogFiles.xlsx";
        [TestMethod]
        public void SPACESHIP_0001_Constructor_Variant_Not_Null()
        {
            Ship spaceShip = new Ship();
            Assert.IsNotNull(spaceShip);
        }
        [TestMethod]
        public void SPACESHIP_0002_AddReading()
        {
            Ship spaceShip = new Ship();
            VelocityReading reading = new VelocityReading("30");
            spaceShip.AddReading(reading);
            Assert.IsTrue(spaceShip.spaceShipReadings.Contains(reading));
        }
        [TestMethod]
        public void SPACESHIP_0003_AddFunction()
        {
            Ship spaceShip = new Ship();
            IncreaseThrustFunction function = new IncreaseThrustFunction("30");
            spaceShip.AddFunction(function);
            Assert.IsTrue(spaceShip.spaceShipFunctions.Contains(function));
        }
        [TestMethod]
        public void SPACESHIP_0004_Send()
        {
            Ship spaceShip = new Ship();
            VelocityReading reading = new VelocityReading("30");
            DataPacket pkt = new DataPacket(reading);
            spaceShip.Send(pkt);
        }
        [TestMethod]
        public void SPACESHIP_0005_SendAll()
        {
            Ship spaceShip = new Ship();
            VelocityReading reading = new VelocityReading("30");
            VelocityReading reading2 = new VelocityReading("70");
            spaceShip.AddReading(reading);
            spaceShip.AddReading(reading2);
            spaceShip.SendAll();
        }
        [TestMethod]
        public void SPACESHIP_0006_RunAll()
        {
            Ship spaceShip = new Ship();
            IncreaseThrustFunction func = new IncreaseThrustFunction("30");
            spaceShip.AddFunction(func);
            spaceShip.RunAll();
        }
        [TestMethod]
        public void SPACESHIP_0007_Recieve_Invalid()
        {
            Ship spaceShip = new Ship();
            bool actual = spaceShip.Receive("string is wrong");
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SPACESHIP_0008_TimedEvent()
        {
            Ship spaceShip = new Ship();
            spaceShip.TimedEvent("");
            Assert.IsNotNull(spaceShip);
        }
        [TestMethod]
        public void SPACESHIP_0009_VerifyLoggingSendAllData()
        {
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "A", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "A", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 3);
            VelocityReading velReading = new VelocityReading("30");
            RadiationReading radReading = new RadiationReading("40");
            PositionReading posReading = new PositionReading("30, 20");
            Ship spaceShip = new Ship();
            spaceShip.Send(new DataPacket(velReading));
            spaceShip.Send(new DataPacket(radReading));
            spaceShip.Send(new DataPacket(posReading));
            string actual1 = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D1");
            string actual2 = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D2");
            string actual3 = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D3");
            string expected1 = "30";
            string expected2 = "40";
            string expected3 = "30, 20";
            Assert.AreEqual(actual1, expected1);
            Assert.AreEqual(actual2, expected2);
            Assert.AreEqual(actual3, expected3);
        }
        [TestMethod]
        public void SPACESHIP_0010_VerifyLoggingSendData()
        {
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);
            VelocityReading velReading = new VelocityReading("30");
            DataPacket pkt = new DataPacket(velReading);
            Ship spaceShip = new Ship();
            spaceShip.Send(pkt);
            string actual1 = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D1");
            string expected1 = "30";
            Assert.AreEqual(actual1, expected1);
        }
        [TestMethod]
        public void SPACESHIP_0011_VerifyLoggingSendFunc()
        {
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);
            IncreaseThrustFunction thrustFunc = new IncreaseThrustFunction("30");
            FunctionPacket pkt = new FunctionPacket(thrustFunc);
            Ship spaceShip = new Ship();
            spaceShip.Send(pkt);
            string actual1 = Logging.GetCellValue(ExcelLogFilesPath, "Sheet1", "D1");
            string expected1 = "30";
            Assert.AreEqual(actual1, expected1);
        }
        [TestMethod]
        public void SPACESHIP_0012_VerifyAllAdded()
        {
            Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 1);
            Logging.InsertText(ExcelLogFilesPath, "", "A", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 2);
            Logging.InsertText(ExcelLogFilesPath, "", "A", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "B", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "C", 3);
            Logging.InsertText(ExcelLogFilesPath, "", "D", 3);
            VelocityReading velReading = new VelocityReading("30");
            RadiationReading radReading = new RadiationReading("40");
            PositionReading posReading = new PositionReading("30, 20");
            Ship spaceShip = new Ship();
            spaceShip.AddReading(velReading);
            spaceShip.AddReading(radReading);
            spaceShip.AddReading(posReading);
            int actual = spaceShip.spaceShipReadings.Count;
            int expected = 3;
            Assert.AreEqual(actual, expected);
        }
        //[TestMethod]
        //public void SPACESHIP_0012_VerifyAllSent()
        //{
        //    Logging.InsertText(ExcelLogFilesPath, "", "A", 1);
        //    Logging.InsertText(ExcelLogFilesPath, "", "B", 1);
        //    Logging.InsertText(ExcelLogFilesPath, "", "C", 1);
        //    Logging.InsertText(ExcelLogFilesPath, "", "D", 1);
        //    Logging.InsertText(ExcelLogFilesPath, "", "A", 2);
        //    Logging.InsertText(ExcelLogFilesPath, "", "B", 2);
        //    Logging.InsertText(ExcelLogFilesPath, "", "C", 2);
        //    Logging.InsertText(ExcelLogFilesPath, "", "D", 2);
        //    Logging.InsertText(ExcelLogFilesPath, "", "A", 3);
        //    Logging.InsertText(ExcelLogFilesPath, "", "B", 3);
        //    Logging.InsertText(ExcelLogFilesPath, "", "C", 3);
        //    Logging.InsertText(ExcelLogFilesPath, "", "D", 3);
        //    VelocityReading velReading = new VelocityReading("30");
        //    RadiationReading radReading = new RadiationReading("40");
        //    PositionReading posReading = new PositionReading("30, 20");
        //    Spaceship spaceShip = new Spaceship();
        //    spaceShip.AddReading(velReading);
        //    spaceShip.AddReading(radReading);
        //    spaceShip.AddReading(posReading);
        //    spaceShip.SendAll();
        //    int actual = spaceShip.spaceShipReadings.Count;
        //    int expected = 0;
        //    Assert.AreEqual(actual, expected);
        //}
    }
    [TestClass]
    public class DataPacketTests
    {
        [TestMethod]
        public void RDPKT_0001_Constructor_Variant_1_Not_Null()
        {
            DataPacket readingPkt = new DataPacket(new VelocityReading("data"));
            Assert.IsNotNull(readingPkt);
        }

        [TestMethod]
        public void RDPKT_0002_Constructor_Variant_2_Not_Null()
        {
            VelocityReading stub = new VelocityReading("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            Assert.IsNotNull(readingPkt);
        }

        [TestMethod]
        public void RDPKT_0003_Constructor_Variant_1_Data_Correct_DateTime()
        {
            DataPacket readingPkt = new DataPacket(new VelocityReading("data"));
            String actual = readingPkt.DateTime.ToString();
            String expected = DateTime.Now.ToString(); 
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0004_Constructor_Variant_2_Data_Correct_DateTime()
        {
            VelocityReading stub = new VelocityReading("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            String actual = readingPkt.DateTime.ToString();
            String expected = DateTime.Now.ToString();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0005_Constructor_Variant_1_Data_Correct_DataType()
        {
            DataPacket readingPkt = new DataPacket(new VelocityReading("data"));
            string actual = readingPkt.Data;
            string expected = "data";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0006_Constructor_Variant_2_Data_Correct_DataType()
        {
            DataPacket readingPkt = new DataPacket(new VelocityReading("data"));
            string actual = readingPkt.DataType;
            string expected = "VelocityReading";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0007_Constructor_Variant_1_Data_Correct_Data()
        {
            DataPacket readingPkt = new DataPacket(new VelocityReading("data"));
            string actual = readingPkt.Data;
            string expected = "data";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0008_Constructor_Variant_2_Data_Correct_Data()
        {
            VelocityReading stub = new VelocityReading("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            string actual = readingPkt.Data;
            string expected = "This is the data";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0009_Constructor_Variant_1_Data_Correct_CRC()
        {
            DataPacket readingPkt = new DataPacket(new VelocityReading("data"));
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
            VelocityReading stub = new VelocityReading("This is the data");
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
            VelocityReading stub = new VelocityReading("This is the data");
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
            VelocityReading stub = new VelocityReading("");
            DataPacket readingPkt = new DataPacket(stub);
            byte[] actual = readingPkt.ConvertToByteArray("This is the test string");
            byte[] expected = Encoding.ASCII.GetBytes("This is the test string");
            bool testPassed = expected.SequenceEqual(actual);
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void RDPKT_0013_Validate_CRC_True()
        {
            VelocityReading stub = new VelocityReading("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            DataPacket readingPktCopy = new DataPacket(stub);
            bool actual = readingPkt.ValidateCRC(readingPktCopy.Crc);
            bool expected = true;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0014_Validate_CRC_False()
        {
            VelocityReading stub = new VelocityReading("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            bool actual = readingPkt.ValidateCRC("probably wrong");
            bool expected = false;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0015_ToJson_Not_Null()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.ToJson();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void RDPKT_0016_ToJson_Correct_Type_String()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.ToJson();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void RDPKT_0017_ToJson_Correct_JSON()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string expected = JsonSerializer.Serialize(dataPkt);
            string actual = dataPkt.ToJson();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RDPKT_0018_GetPacketType_Not_Null()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketType();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void RDPKT_0019_GetPacketType_Correct_Type_String()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketType();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void RDPKT_0020_Correct_PacketType_Stub()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketType();

            Assert.AreEqual(actual, stub.GetType().Name);
        }

        [TestMethod]
        public void RDPKT_0021_GetPacketData_Not_Null()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketData();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void RDPKT_0022_GetPacketData_Correct_Type_String()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketData();

            Assert.IsInstanceOfType(actual, typeof(string));
        }

        [TestMethod]
        public void RDPKT_0023_GetPacketData_Correct_PacketData_Stub()
        {
            VelocityReading stub = new VelocityReading("test data");
            DataPacket dataPkt = new DataPacket(stub);

            string actual = dataPkt.GetPacketData();

            Assert.AreEqual(actual, stub.GetData());
        }
    }

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
        public void SD_0001_Contructor_Variant_1_Authorized()
        {
            //Arange
            string expected = "True";
            SelfDestructFunction SDTest = new SelfDestructFunction(true);
            //Act
            string actual = SDTest.GetCommand();
            //Assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void SD_0002_Contructor_Variant_1_Unauthorized()
        {
            //Arange
            string expected = "False";
            SelfDestructFunction SDTest = new SelfDestructFunction(false);
            //Act
            string actual = SDTest.GetCommand();
            //Assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void SD_0003_Contructor_Variant_2_Authorized()
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
        public void SD_0004_Contructor_Variant_2_Unauthorized()
        {
            //Arange
            string expected = "False";
            SelfDestructFunction SDTest = new SelfDestructFunction("false");
            //Act
            string actual = SDTest.GetCommand();
            //Assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SD_0005_RunCommand_Authorized()
        {
            SelfDestructFunction SDTest = new SelfDestructFunction(true);
            SDTest.RunCommand();
        }
        [TestMethod]
        public void SD_0006_RunCommand_Unauthorized()
        {
            SelfDestructFunction SDTest = new SelfDestructFunction(false);
            SDTest.RunCommand();
        }
    }
    [TestClass]
    public class IncreaseThrustTests
    {
        [TestMethod]
        public void THRUST_0001_Constructor_Variant_1()
        {
            string expected = "500";
            IncreaseThrustFunction increaseThrust = new IncreaseThrustFunction("500");
            string actual = increaseThrust.GetCommand();
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void THRUST_0002_Constructor_Variant_2()
        {
            string expected = "500";
            IncreaseThrustFunction increaseThrust = new IncreaseThrustFunction(500);
            string actual = increaseThrust.GetCommand();
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void THRUST_0003_Run_Command()
        {
            IncreaseThrustFunction increaseThrust = new IncreaseThrustFunction(500);
            increaseThrust.RunCommand();
        }
    }
}
