//PayloadOps
//Packet Definition for a function call
using System.IO.Hashing;
using System.Text;
using System.Text.Json;
namespace Project_5_Space_Craft
{
    public class FunctionPacket : IPacket
    {
        private DateTime dateTime;
        public DateTime DateTime { get { return dateTime; } }
        private string functionType;
        public string FunctionType { get { return functionType; } }
        private string command;
        public string Command { get { return command; } }
        private string packetCRC;
        public string PacketCRC { get { return packetCRC; } }

        public FunctionPacket(IFunction function)
        {
            dateTime = DateTime.Now;
            this.dataType = function.GetType().Name;
            this.data = function.GetCommand();
            packetCRC = CalculateCRC();
        }

        public DataPacket(string functionType, string command)
        {
            dateTime = DateTime.Now;
            this.functionType = functionType;
            this.command = command;
            packetCRC = CalculateCRC();
        }

        //Calculates the CRC based on the dateTime, dataType, and data
        public string CalculateCRC()
        {
            Crc32 crc = new Crc32();
            crc.Append(ConvertToByteArray(dateTime.ToString()));
            crc.Append(ConvertToByteArray(functionType));
            crc.Append(ConvertToByteArray(command));
            return crc.GetCurrentHash().ToString();
        }

        //Helper function to convert a given string to a byte array
        public byte[] ConvertToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public bool ValidateCRC(string crc){
            return this.CalculateCRC == crc;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}