//PayloadOps
//Packet Definition for a function call
using System.IO.Hashing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
namespace Payload_Ops.Packets
{
    public class FunctionPacket : IPacket
    {
        [JsonIgnore]
        private IFunction function;
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
            this.function = function;
            dateTime = DateTime.Now;
            functionType = function.GetType().Name;
            command = function.GetCommand();
            packetCRC = CalculateCRC();
        }

        public IFunction? GetFunction()
        {
            return function ?? createFunction();
        }

        private IFunction? createFunction()
        {
            switch (functionType.ToLower())
            {
                case "selfdestructfunction": return new SelfDestructFunction(command);
                case "increasethrustfunction": return new IncreaseThrustFunction(command);
                default: return null;
            }
        }

        //public FunctionPacket(string functionType, string command)
        //{
        //    dateTime = DateTime.Now;
        //    this.functionType = functionType;
        //    this.command = command;
        //    packetCRC = CalculateCRC();
        //}

        //Calculates the CRC based on the dateTime, dataType, and data
        public string CalculateCRC()
        {
            Crc32 crc = new Crc32();
            crc.Append(ConvertToByteArray(dateTime.ToString()));
            crc.Append(ConvertToByteArray(functionType));
            crc.Append(ConvertToByteArray(command));
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
            return functionType;
        }
        public string GetPacketData()
        {
            return command;
        }
    }
}