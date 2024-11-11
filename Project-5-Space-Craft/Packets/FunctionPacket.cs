//PayloadOps
//Packet Definition for a function call
using System.IO.Hashing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Payload_Ops.Functions;
namespace Payload_Ops.Packets
{
    //public class FunctionPacket : IPacket
    //{
    //    [JsonIgnore] private IFunction? function;
    //    //[JsonPropertyName("dateTime")] private DateTime dateTime;
    //    //[JsonIgnore] public DateTime DateTime { get { return dateTime; } }
    //    //[JsonPropertyName("functionType")] private string functionType;
    //    //[JsonIgnore] public string FunctionType { get { return functionType; } }
    //    //[JsonPropertyName("command")] private string command;
    //    //[JsonIgnore] public string Command { get { return command; } }
    //    //[JsonPropertyName("crc")] private string crc;
    //    //[JsonIgnore] public string PacketCRC { get { return crc; } }
    //    [JsonPropertyName("dateTime")]
    //    public DateTime DateTime { get; private set; }

    //    [JsonPropertyName("functionType")]
    //    public string FunctionType { get; private set; }

    //    [JsonPropertyName("command")]
    //    public string Command { get; private set; }

    //    [JsonPropertyName("crc")]
    //    public string PacketCRC { get; private set; }

    //    public FunctionPacket(IFunction function)
    //    {
    //        this.function = function;
    //        DateTime = DateTime.Now;
    //        FunctionType = function.GetType().Name;
    //        Command = function.GetCommand();
    //        PacketCRC = CalculateCRC();
    //    }
    //    [JsonConstructor]
    //    public FunctionPacket(string dateTime, string functionType, string command, string crc)
    //    {
    //        this.DateTime = DateTime.Parse(dateTime);
    //        this.FunctionType = functionType;
    //        this.Command = command;
    //        this.PacketCRC = crc;
    //        this.function = this.createFunction();
    //    }

    //    public IFunction? GetFunction()
    //    {
    //        return function ?? createFunction();
    //    }

    //    private IFunction? createFunction()
    //    {
    //        switch (FunctionType.ToLower())
    //        {
    //            case "selfdestructfunction": return new SelfDestructFunction(Command);
    //            case "increasethrustfunction": return new IncreaseThrustFunction(Command);
    //            case "functionstub": return new FunctionStub(Command);
    //            default: return null;
    //        }
    //    }

    //    //public FunctionPacket(string functionType, string command)
    //    //{
    //    //    dateTime = DateTime.Now;
    //    //    this.functionType = functionType;
    //    //    this.command = command;
    //    //    packetCRC = CalculateCRC();
    //    //}

    //    //Calculates the CRC based on the dateTime, dataType, and data
    //    public string CalculateCRC()
    //    {
    //        Crc32 crc = new Crc32();
    //        crc.Append(ConvertToByteArray(DateTime.ToString()));
    //        crc.Append(ConvertToByteArray(FunctionType));
    //        crc.Append(ConvertToByteArray(Command));
    //        return BitConverter.ToString(crc.GetCurrentHash()).Replace("-", "");
    //    }

    //    //Helper function to convert a given string to a byte array
    //    public byte[] ConvertToByteArray(string str)
    //    {
    //        return Encoding.ASCII.GetBytes(str);
    //    }

    //    public bool ValidateCRC(string crc)
    //    {
    //        return CalculateCRC() == crc;
    //    }

    //    public string ToJson()
    //    {
    //        return JsonSerializer.Serialize(this);
    //    }
    //    public string GetPacketType()
    //    {
    //        return FunctionType;
    //    }
    //    public string GetPacketData()
    //    {
    //        return Command;
    //    }
    //}
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
            this.Date = Date;
            this.FunctionType = FunctionType;
            this.Command = Command;
            this.PacketCRC = PacketCRC;
            this.function = this.createFunction();
        }

        [JsonIgnore]private IFunction? function;

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
            //return createFunction();
        }

        private IFunction? createFunction()
        {
            switch (FunctionType.ToLower())
            {
                case "selfdestructfunction": return new SelfDestructFunction(Command);
                case "increasethrustfunction": return new IncreaseThrustFunction(Command);
                case "functionstub": return new FunctionStub(Command);
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