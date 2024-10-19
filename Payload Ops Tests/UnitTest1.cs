using System.Globalization;
using System.Net.Sockets;
using Project_5_Space_Craft;
namespace Payload_Ops_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TM001PacketWrapperToJson()
        {
            //Arange
            PacketWrapper pw = new PacketWrapper(new ReadingsStub());
            Dictionary<string, string> expected = new Dictionary<string, string>() {

                { "datetime", DateTime.Now.ToString(new CultureInfo("en-US"))},
                { "datatype", "Project_5_Space_Craft.ReadingsStub" },
                { "data", "0"},
                { "crc", "FFFFFFFF" }
        };
            //Act
            Dictionary<string, string> actual = pw.ToJson();
            //Assert
            foreach (KeyValuePair<string, string> entry in actual)
            {
                Assert.AreEqual(expected[entry.Key], entry.Value);
            }
        }
    }
}