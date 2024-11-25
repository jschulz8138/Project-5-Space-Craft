//PayloadOps
//Packet Definition for spaceship, contains a dateTime, dataType, data, and CRC
using System.IO.Hashing;
using System.Text;
using System.Text.Json;
namespace Payload_Ops.Packets
{
    public class DataPacket : IPacket
    {
        private IReading reading;
        private DateTime dateTime;
        public DateTime DateTime { get { return dateTime; } }
        private string dataType;
        public string DataType { get { return dataType; } }
        private string data;
        public string Data { get { return data; } }
        private string crc;
        public string Crc { get { return crc; } }
        public DataPacket(IReading reading)
        {
            this.reading = reading;

            dateTime = DateTime.Now;
            dataType = reading.GetType().Name;
            data = reading.GetData();
            crc = CalculateCRC();
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

        public bool ValidateCRC(string crc)
        {
            return CalculateCRC() == crc;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public string GetPacketType()
        {
            return dataType;
        }
        public string GetPacketData()
        {
            return data;
        }
    }
}
