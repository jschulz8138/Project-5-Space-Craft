//PayloadOps
//Packet Definition for spaceship, contains a dateTime, dataType, data, and CRC
using System.IO.Hashing;
using System.Text;
namespace Project_5_Space_Craft
{
    public class ReadingsPacket : IPacket
    {
        private DateTime dateTime;
        public DateTime DateTime { get { return dateTime; } }
        private string dataType;
        public string DataType { get { return dataType; } }
        private string data;
        public string Data { get { return data; } }
        private string packetCRC;
        public string PacketCRC { get { return packetCRC; } }

        public ReadingsPacket(string dataType, string data)
        {
            dateTime = DateTime.Now;
            this.dataType = dataType;
            this.data = data;
            packetCRC = CalculateCRC();
        }

        public ReadingsPacket(IReading reading)
        {
            dateTime = DateTime.Now;
            this.dataType = reading.GetType().Name;
            this.data = reading.GetData();
            packetCRC = CalculateCRC();
        }

        //Calculates the CRC based on the dateTime, dataType, and data
        public string CalculateCRC()
        {
            Crc32 crc = new Crc32();
            crc.Append(ConvertToByteArray(dateTime.ToString()));
            crc.Append(ConvertToByteArray(dataType));
            crc.Append(ConvertToByteArray(data));
            return BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
        }

        //Helper function to convert a given string to a byte array
        public byte[] ConvertToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        //TODO
        public bool ValidateCRC(string crc){
            return true;
        }
    }
}
