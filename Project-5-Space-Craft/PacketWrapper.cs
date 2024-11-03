//PayloadOps
//PacketWrapper implementation for packetizing data for transfer
using System.Text;
using System;
using System.Text.Json;
namespace Project_5_Space_Craft
{
    public class PacketWrapper
    {
        public PacketWrapper()
        {

        }


        //Calculates the CRC based on the dateTime, dataType, and data
        public string CalculateCRC()
        {

            return "";
        }

        //Helper function to convert a given string to a byte array
        public byte[] ConvertToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        //TODO
        public bool ValidateCRC(string crc)
        {
            return true;
        }

        public String ToJson()
        {
            return "";
        }
    }
}
