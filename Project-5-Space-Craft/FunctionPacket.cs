//PayloadOps
//Packet Definition for a function call
namespace Project_5_Space_Craft
{
    public class FunctionPacket : Packet
    {
        //TODO:WRITE a constructor

        //TODO
        //Calculates the CRC based on the dateTime, dataType, and data
        private string calculateCRC()
        {
            Crc32 crc = new Crc32();
            //crc.Append(ConvertToByteArray(dateTime.ToString()));
            //crc.Append(ConvertToByteArray(dataType));
            //crc.Append(ConvertToByteArray(data));
            return crc.GetCurrentHash().ToString();
        }

        //Helper function to convert a given string to a byte array
        private byte[] convertToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        //TODO
        //
        private bool validateCRC(string crc){
            return true;
        }
    }
}