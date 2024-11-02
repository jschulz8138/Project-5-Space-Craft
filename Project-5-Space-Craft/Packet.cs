//PayloadOps
//Packet Interface
namespace Project_5_Space_Craft
{
    public interface Packet
    {
        //Calculates the CRC
        public string CalculateCRC();

        //Helper function to convert a given string to a byte array
        public byte[] ConvertToByteArray(string str);

        //Validates the CRC
        public bool ValidateCRC(string crc);
    }

}