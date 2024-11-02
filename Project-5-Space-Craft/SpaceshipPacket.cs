//PayloadOps
//Packet Definition for spaceship, contains a dateTime, dataType, data, and CRC
using System.IO.Hashing;
using System.Text;
namespace Project_5_Space_Craft
{
    public class SpaceshipPacket
    {
        private DateTime dateTime;
        private string dataType;
        private string data;
        private string packetCRC;

        public SpaceshipPacket(string dataType, string data)
        {
            dateTime = DateTime.Now;
            this.dataType = dataType;
            this.data = data;
            packetCRC = CalculateCRC();
        }

        public SpaceshipPacket(SpaceshipReading reading)
        {
            dateTime = DateTime.Now;
            this.dataType = reading.GetType().Name;
            this.data = reading.GetData();
            packetCRC = CalculateCRC();
        }

        //Calculates the CRC based on the dateTime, dataType, and data
        private string CalculateCRC()
        {
            Crc32 crc = new Crc32();
            crc.Append(ConvertToByteArray(dateTime.ToString()));
            crc.Append(ConvertToByteArray(dataType));
            crc.Append(ConvertToByteArray(data));
            return crc.GetCurrentHash().ToString();
        }

        //Helper function to convert a given string to a byte array
        private byte[] ConvertToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}
