//PayloadOps
//Packet Interface
namespace Payload_Ops
{
    public interface IPacket
    {
        public string GetPacketType();
        public string GetPacketData();
        //Calculates the CRC
        public string CalculateCRC();

        //Helper function to convert a given string to a byte array
        public byte[] ConvertToByteArray(string str);

        //Validates the CRC
        public bool ValidateCRC(string crc);

        public String ToJson();
    }

}