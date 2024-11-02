//PayloadOps
//Packet Interface 
namespace Project_5_Space_Craft
{
    public interface Packet
    {
        //Calculates the CRC
        private string calculateCRC();
        //Helper function to convert a given string to a byte array
        private byte[] convertToByteArray(string str);
        //Validates the CRC
        private bool validateCRC(string crc);


    }

}