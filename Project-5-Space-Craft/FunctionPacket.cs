//PayloadOps
//Packet Definition for a function call
using System.IO.Hashing;
using System.Text;
namespace Project_5_Space_Craft
{
    public class FunctionPacket : Packet
    {
        public FunctionPacket(SpaceshipReading reading)
        {

        }

        //TODO
        //Calculates the CRC based on the dateTime, dataType, and data
        public string CalculateCRC()
        {
            Crc32 crc = new Crc32();
            //crc.Append(ConvertToByteArray(dateTime.ToString()));
            //crc.Append(ConvertToByteArray(dataType));
            //crc.Append(ConvertToByteArray(data));
            return crc.GetCurrentHash().ToString();
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