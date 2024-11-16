//Payload Ops
//Implementation of Spaceship that runs the overall program. 
using System.Text.Json;
using Payload_Ops.Packets;
using Uplink_Downlink;
namespace Payload_Ops
{
    public class Spaceship
    {
        private static readonly string GroundStationURI = "http://localhost:5014"; // will change
        private List<IReading> spaceShipReadings;
        private List<IFunction> spaceShipFunctions;
        private ConnectionManager _connectionManager = new ConnectionManager(GroundStationURI);
        private CommunicationHandler _communicationHandler = new CommunicationHandler(GroundStationURI);
        private Timer timer;
        public Spaceship() {
            //initalize variables
            this.spaceShipReadings = new List<IReading>();
            this.spaceShipFunctions = new List<IFunction>();

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
        public async Task<bool> Send(IPacket packet) 
        {
            bool result = false;
            string jsonPacket = packet.ToJson();
            //TODO Temporarily removed logging functionality
            //Logging.LogPacket(packet.GetPacketType(), "Outbound", packet.GetPacketData());
            if (!_connectionManager.IsAuthenticated)
            {
                result = await _connectionManager.AuthenticateAsync(jsonPacket);
            }
            else // I have them commented out because packet isn't in string format in this current version of the code
            {
                result = await _communicationHandler.UpdateGroundStationAsync(jsonPacket);
            }
            //TODO: Send packet
            //!response.IsSuccessStatusCode
            return result;
        }

        public async Task SendAll()
        {
            while (this.spaceShipReadings.Count() != 0)
            {
                if (await Send(new DataPacket(this.spaceShipReadings[0])) == true)
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

        public bool Receive(string jsonObj) { 
            Console.WriteLine(jsonObj);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true 
            };
            FunctionPacket? packet = null;
            try
            {
                packet = JsonSerializer.Deserialize<FunctionPacket>(jsonObj, options);
            }
            catch (Exception e)
            {
                Console.WriteLine("Function packet was not deserialized properly due to mismatching json data");
                Console.WriteLine(e);
                return false;
            }

            if (packet == null)
            {
                Console.WriteLine("Packet == null");
                return false;
            }

            IFunction? function = packet.GetFunction();

            if (function == null)
            {
                Console.WriteLine("(function == null)");
                return false;
            }
            this.AddFunction(function);

            Logging.LogPacket(packet.GetPacketType(), "Incoming", packet.GetPacketData());
            return true;
        }

        public void TimedEvent(object? state)
        {
            Console.WriteLine("Readings sent at: " + DateTime.Now);
            SendAll();
        }
    }
}
