using System;

namespace Project_5_Space_Craft
{
    class Spaceship
    {
        private List<SpaceshipReadings> data;
        private Timer timer;
        public Spaceship() {
            this.data = new List<SpaceshipReadings>();

            DateTime now = DateTime.Now;
#if DEBUG
            // For DEBUG: Set to trigger every minute
            DateTime nextEvent = now.AddMinutes(1).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
            Console.WriteLine($"Timer set to trigger every minute in DEBUG mode.");
            TimeSpan eventLength = TimeSpan.FromMinutes(1);
#else
            // For RELEASE: Set to trigger every hour
            DateTime nextEvent = now.AddHours(1).AddMinutes(-now.Minute).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
            Console.WriteLine($"Timer set to trigger every hour in RELEASE mode.");
            TimeSpan eventLength = TimeSpan.FromHours(1);
#endif
            this.timer = new Timer(this.TimedEvent, null, nextEvent - now, eventLength);
        }

        public Spaceship Add(SpaceshipReadings data)
        {
            this.data.Add(data);
            return this;
        }

        private void TimedEvent(object? state)
        {
            Console.WriteLine("Event triggered at: " + DateTime.Now);
            //Need to spin up some packet wrappers \o/
            foreach (SpaceshipReadings singleData in this.data)
            {
                int num = Send(new PacketWrapper(singleData));
            }

        }

        private int Send(PacketWrapper packet)
        {

            //This function is blocked by uplink downlink
            //return uplink.downlink.send(packet)
            foreach (KeyValuePair<string, string> entry in packet.ToJson())
            {
                // do something with entry.Value or entry.Key
                Console.WriteLine(entry.Key + " : " + entry.Value + ",");
            }
            return 1;
        }

        private void UnpackMessage()
        {
            //This function is blocked by uplink downlink
            //switch per type of message and handle
        }
    }
}
