//PayloadOps
//PacketWrapper implementation for packetizing data for transfer
using System.Text.Json;
namespace Project_5_Space_Craft
{
    public class PacketWrapper
    {
        public PacketWrapper()
        {

        }

        //given a spaceship reading, convert it to a string in the form of a json object, used for reading packets
        public String ToJsonReadings(IReading reading)
        {
            IPacket pkt = new ReadingsPacket(reading);
            return JsonSerializer.Serialize(pkt);
        }

        //given a spaceship reading, convert it to a string in the form of a json object, used for function packets
        public String ToJsonFunction(IReading reading)
        {
            IPacket pkt = new FunctionPacket(reading);
            return JsonSerializer.Serialize(pkt);
        }

        //Given a json packet in the form of a string, convert it to a readable packet
        public IPacket ToReading(String packet)
        {
            //TODO: Figure this out :)
            ReadingsPacket? pkt = JsonSerializer.Deserialize<ReadingsPacket>(packet);
            return pkt;
        }
    }
}
