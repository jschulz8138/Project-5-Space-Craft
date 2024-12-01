//PayloadOps
//Packet Definition for a function call
using System.IO.Hashing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Payload_Ops.Packets
{
    public class FunctionPacket : IPacket
    {
        public string Date { get; private set; }

        public string FunctionType { get; private set; }

        public string Command { get; private set; }

        public string PacketCRC { get; private set; }

        // Constructor with parameters matching property names
        [JsonConstructor]
        public FunctionPacket(string Date, string FunctionType, string Command, string PacketCRC)
        {
            if (string.IsNullOrWhiteSpace(Date))
                throw new ArgumentException("Date cannot be null or empty.", nameof(Date));
            if (string.IsNullOrWhiteSpace(FunctionType))
                throw new ArgumentException("FunctionType cannot be null or empty.", nameof(FunctionType));
            if (string.IsNullOrWhiteSpace(Command))
                throw new ArgumentException("Command cannot be null or empty.", nameof(Command));
            if (string.IsNullOrWhiteSpace(PacketCRC))
                throw new ArgumentException("PacketCRC cannot be null or empty.", nameof(PacketCRC));
            this.Date = Date;
            this.FunctionType = FunctionType;
            this.Command = Command;
            this.PacketCRC = PacketCRC;
            this.function = this.createFunction();
        }

        [JsonIgnore] private IFunction? function;

        public FunctionPacket(IFunction function)
        {
            this.function = function;
            Date = DateTime.Now.ToString();
            FunctionType = function.GetType().Name;
            Command = function.GetCommand();
            PacketCRC = CalculateCRC();
        }

        public IFunction? GetFunction()
        {
            return function ?? createFunction();
        }

        public IFunction? createFunction()
        {
            switch (FunctionType.ToLower())
            {
                case "selfdestructfunction": return new SelfDestructFunction(Command);
                case "increasethrustfunction": return new IncreaseThrustFunction(Command);
                case "telemetry" : return new MoveshipFunction(Command);
                default: return null;
            }
        }

        public string CalculateCRC()
        {
            Crc32 crc = new Crc32();
            crc.Append(ConvertToByteArray(Date.ToString()));
            crc.Append(ConvertToByteArray(FunctionType));
            crc.Append(ConvertToByteArray(Command));
            return BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
        }

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
            return FunctionType;
        }
        public string GetPacketData()
        {
            return Command;
        }
    }

}