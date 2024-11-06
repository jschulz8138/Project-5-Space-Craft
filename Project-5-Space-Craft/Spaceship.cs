//Payload Ops
//Implementation of Spaceship that runs the overall program. 
using System.Text.Json;
using Project_5_Space_Craft.Packets;
namespace Project_5_Space_Craft
{
    public class Spaceship
    {
        private List<IReading> spaceShipReadings;
        private List<IFunction> spaceShipFunctions;
        private Timer timer;
        //private PacketWrapper pktWrapper;

        public Spaceship() {
            //initalize variables
            this.spaceShipReadings = new List<IReading>();
            //pktWrapper = new PacketWrapper();

            DateTime now = DateTime.Now;

#if DEBUG   // For DEBUG: Set to trigger every minute
            DateTime nextEvent = now.AddMinutes(1).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
            Console.WriteLine($"Timer set to trigger every minute in DEBUG mode.");
            TimeSpan eventLength = TimeSpan.FromMinutes(1);

#else       // For RELEASE: Set to trigger every hour
            DateTime nextEvent = now.AddHours(1).AddMinutes(-now.Minute).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
            Console.WriteLine($"Timer set to trigger every hour in RELEASE mode.");
            TimeSpan eventLength = TimeSpan.FromHours(1);
#endif
            this.timer = new Timer(this.TimedEvent, null, nextEvent - now, eventLength);
        }

        public void AddReading(IReading reading)
        {
            this.spaceShipReadings.Add(reading);
        }

        public void AddFunction(IFunction function)
        {
            this.spaceShipFunctions.Add(function);
        }

        //TODO: Connect functionality to uplink / downlink
        public bool Send(IPacket packet)
        {
            Logging.LogPacket(packet.GetPacketType(), "Outbound", packet.GetPacketData());
            //TODO: Send packet
            //!response.IsSuccessStatusCode
            return true;
        }

        public void SendAll()
        {
            while (this.spaceShipReadings.Count() != 0)
            {
                if (Send(new DataPacket(this.spaceShipReadings[0])) == true)
                    this.spaceShipReadings.RemoveAt(0);
            }
        }

        public void RunAll()
        {
            while (this.spaceShipFunctions.Count() != 0)
            {
                //if (Send(DataPacket(this.spaceShipReadings[0])) == true)
                this.spaceShipFunctions[0].RunCommand();
                this.spaceShipFunctions.RemoveAt(0);
            }
        }

        public void Receive(string jsonObj)
        {
            FunctionPacket packet = JsonSerializer.Deserialize<FunctionPacket>(jsonObj);
            this.AddFunction(packet.GetFunction());
        }

        private void TimedEvent(object? state)
        {
            Console.WriteLine("Readings sent at: " + DateTime.Now);
            SendAll();
        }
    }
}
