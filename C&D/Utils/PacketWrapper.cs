namespace CAndD.Utils
{
    public static class PacketWrapper
    {
        public static string WrapPacket(string data)
        {
            // Wrap data into a packet format if necessary
            return $"<Packet>{data}</Packet>";
        }
    }
}
