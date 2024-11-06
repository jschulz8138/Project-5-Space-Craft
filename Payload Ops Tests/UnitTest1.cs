//Payload Ops
//Unit and Integration Tests
using Project_5_Space_Craft;
using System;
using System.IO.Hashing;
using System.Linq;
using System.Text;
namespace Payload_Ops_Tests
{
    [TestClass]
    public class FunctionPacketTests
    {
        [TestMethod]
        public void FNPKT_0001_Constructor_Not_Null_1()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            Assert.IsNotNull(funcPkt);
        }

        [TestMethod]
        public void FNPKT_0002_Constructor_Not_Null_2()
        {
            FunctionPacket funcPkt = new FunctionPacket("test type","test command");
            Assert.IsNotNull(funcPkt);
        }

        [TestMethod]
        public void FNPKT_0003_Constructor_Variant_1_Data_Correct_DateTime()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            String actual = funcPkt.DateTime.ToString();
            String expected = DateTime.Now.ToString();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0004_Constructor_Variant_2_Data_Correct_DateTime()
        {
            FunctionPacket funcPkt = new FunctionPacket("test type", "test command");
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
            FunctionPacket funcPkt = new FunctionPacket("test type", "test command");
            string actual = funcPkt.FunctionType;
            string expected = "test type";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0007_Constructor_Variant_1_Data_Correct_Command()
        {
            FunctionStub stub = new FunctionStub("test command");
            FunctionPacket funcPkt = new FunctionPacket(stub);
            string actual = funcPkt.Command;
            string expected = "Some Command";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0008_Constructor_Variant_2_Data_Correct_Command()
        {
            FunctionPacket funcPkt = new FunctionPacket("test type", "test command");
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
            string actual = funcPkt.Crc;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void FNPKT_0010_Constructor_Variant_2_Data_Correct_CRC()
        {
            FunctionPacket funcPkt = new FunctionPacket("test type", "test command");
            Crc32 crc = new Crc32();
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.DateTime.ToString()));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.FunctionType));
            crc.Append(funcPkt.ConvertToByteArray(funcPkt.Command));
            string expected = BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
            string actual = funcPkt.Crc;
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
            string actual = funcPkt.Crc;
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
            bool actual = funcPkt.ValidateCRC(funcPktCopy.Crc);
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
            DataPacket readingPkt = new DataPacket("dataType", "data");
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
            DataPacket readingPkt = new DataPacket("dataType", "data");
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
            DataPacket readingPkt = new DataPacket("dataType", "data");
            string actual = readingPkt.DataType;
            string expected = "dataType";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0006_Constructor_Variant_2_Data_Correct_DataType()
        {
            ReadingsStub stub = new ReadingsStub("This is the data");
            DataPacket readingPkt = new DataPacket(stub);
            string actual = readingPkt.DataType;
            string expected = "ReadingsStub";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RDPKT_0007_Constructor_Variant_1_Data_Correct_Data()
        {
            DataPacket readingPkt = new DataPacket("dataType", "data");
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
            DataPacket readingPkt = new DataPacket("dataType", "data");
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
        [TestMethod]
        public void PKTWRAP_0001_Constructor_Not_Null()
        {
            PacketWrapper pktWrap = new PacketWrapper();
            Assert.IsNotNull(pktWrap);
        }

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