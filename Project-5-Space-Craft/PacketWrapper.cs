//PayloadOps
//PacketWrapper implementation for packetizing data for transfer
using System.Text.Json;
namespace Project_5_Space_Craft
{
    public class PacketWrapper
    {
        //TODO: ADd constructor
        //given a spaceship reading, convert it to a string in the form of a json object
        public String ToJson<Packet>(SpaceshipReading reading)
        {
            Packet pkt = new Packet(reading);
            return JsonSerializer.Serialize(pkt);
        }

        //Given a json packet in the form of a string, convert it to a readable packet
        public Packet ToReading(String packet)
        {
            //TODO: Figure this out :)
            SpaceshipPacket? pkt = JsonSerializer.Deserialize<SpaceshipPacket>(packet);
            return pkt;
        }
    }
}
