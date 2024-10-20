using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace UnitTestPacketWrapper
{
    [TestClass]
    public class PacketWrapperTests
    {
        //T01: test to make sure that it is storing the data
        [TestMethod]
        public void PacketWrapper_ShouldStoreValidData()
        {
            string validJson = "{ \"message\": \"Hello, Grooup3\" }";


            PacketWrapper packetWrapper = new(validJson);


            Assert.AreEqual(validJson, packetWrapper.JsonData);
        }

        //T02: Storing null in the packet

        [TestMethod]
        public void PacketWrapper_StoringNull()
        {
            string nullJson = null;
            PacketWrapper packetWrapper = new(nullJson);

            Assert.IsNull(packetWrapper.JsonData);
        }

        //T03:storing the empty string
        [TestMethod]
        public void PacketWrapper_StoringEmptyString_Should()
        {

            string emptyJson = string.Empty;
            PacketWrapper packetWrapper = new PacketWrapper(emptyJson);


            Assert.AreEqual(emptyJson, packetWrapper.JsonData);
        }

        //T04:setting up new value for the JsonData
        [TestMethod]

        public void JsonData_SettingNewValue_shouldUpdate()
        {
            string initialJson = "{ \"message\": \"Hello, Group3\" }";
            PacketWrapper packetWrapper = new PacketWrapper(initialJson);

            Console.WriteLine("Before updating : " + packetWrapper.JsonData);

            string newJson = "{ \"message\": \"Goodbye, Group3\" }";
            packetWrapper.JsonData = newJson;

            Console.WriteLine("After updating: " + packetWrapper.JsonData);


            Assert.AreEqual(newJson, packetWrapper.JsonData);
        }

        //T05:Updating the JsonData to null 

        [TestMethod]
        public void JsonData_UpdatetoNull_ShouldUpdate()
        {
            string initialJson = "{ \"message\": \"Hello, Group 3\" }";
            PacketWrapper packetWrapper = new PacketWrapper(initialJson);

            packetWrapper.JsonData = null;


            Assert.IsNull(packetWrapper.JsonData);

        }

        //06: Updating JsonData to empty string
        [TestMethod]
        public void JsonData_UpdatetoEMpty_ShouldUpdate()
        {
            string initialJson = "{ \"message\": \"Hello, Group 3\" }";
            PacketWrapper packetWrapper = new PacketWrapper(initialJson);


            packetWrapper.JsonData = string.Empty;


            Assert.AreEqual(string.Empty, packetWrapper.JsonData);
        }




    }

}


