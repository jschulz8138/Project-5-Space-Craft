//Payload Ops
//File that handles logging to both the console and writing to an excel file
namespace Project_5_Space_Craft
{
    static class Logging
    {
        private static String currentPacketType = "";
        private static String currentSendingDirection = "";
        private static String currentPacketData = "";
        private static DateTime currentTime;

        //Interacts with PacketWrapper
        public static void LogPacket(String packetType, String direction, String data)
        {
            currentPacketType = packetType;
            currentSendingDirection = direction;
            currentPacketData = data;
            currentTime = DateTime.Now;
            logFile();
            logConsole();
        }

        private static void logFile()
        {
            
        }

        private static void logConsole()
        {
            Console.WriteLine("Packet Detected!" +
                " DataType: " + currentPacketData +
                " Time: " + currentTime +
                " Direction: " + currentSendingDirection +
                " Data: " + currentPacketData);
        }
    }
}

