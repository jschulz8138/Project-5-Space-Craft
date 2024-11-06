////PayloadOps
////PacketWrapper implementation for packetizing data for transfer
//using System.Text;
//using System;
//using System.Text.Json;
//namespace Project_5_Space_Craft
//{
//    public class PacketWrapper
//    {
//        private IPacket? packet;
//        public IPacket Packet { get { return packet; } }
//        public PacketWrapper(string jsonPacket)
//        {
//            using (JsonDocument doc = JsonDocument.Parse(jsonPacket))
//            {
//                JsonElement root = doc.RootElement;
//                if (root.TryGetProperty("DataType", out JsonElement dataTypeElement))
//                {   
//                    = dataTypeElement.GetString();
//                }
//                else
//                {
//                    this.packet = null;
//                }
//            }
//        }

//        private IPacket dataTypeToPacket(string dataType, string jsonPacket)
//        {
//            IPacket? packet;
//            switch (dataType)
//            {
//                case "position":
//                    packet = JsonSerializer.Deserialize<DataPacket>(jsonPacket);
//                    break;
//                case "radiation":
//                    break;
//                case "temperature":
//                    break;
//                case "velocity":
//                    break;
//            }
//            return packet;
//        }


//        //Calculates the CRC based on the dateTime, dataType, and data
//        public string CalculateCRC()
//        {
//            return "";
//        }

//        //Helper function to convert a given string to a byte array
//        public byte[] ConvertToByteArray(string str)
//        {
//            return Encoding.ASCII.GetBytes(str);
//        }

//        //TODO
//        public bool ValidateCRC(string crc)
//        {
//            return true;
//        }

//        public String ToJson()
//        {
//            return "";
//        }
//    }
//}
