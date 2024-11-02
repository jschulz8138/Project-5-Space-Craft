//Payload Ops
//Implementation of Spaceship that runs the overall program. 
namespace Project_5_Space_Craft
{
    public class Spaceship
    {
        private List<SpaceshipReading> spaceShipReadings;
        private Timer timer;
        private PacketWrapper pktWrapper;


        public Spaceship() {
            //initalize variables
            this.spaceShipReadings = new List<SpaceshipReading>();
            pktWrapper = new PacketWrapper();

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

        public void AddReading(SpaceshipReading reading)
        {
            spaceShipReadings.Add(reading);
        }

        //TODO: Connect functionality to uplink / downlink
        public String SendReading(SpaceshipReading reading)
        {
            spaceShipReadings.Remove(reading);
            return pktWrapper.ToJson(reading);
        }


        private void TimedEvent(object? state)
        {
            Console.WriteLine("Event triggered at: " + DateTime.Now);

            foreach (SpaceshipReading reading in spaceShipReadings)
            {
                SendReading(reading);
            }

        }
    }
}
