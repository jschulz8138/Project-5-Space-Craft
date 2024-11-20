//Payload Ops
//Implementation of Spaceship that runs the overall program. 
using System.Text.Json;
using Payload_Ops.Packets;
using Uplink_Downlink;
using CAndD.Services;
using Payload_Ops;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using CAndD.Models;

namespace Proj5Spaceship
{
    public class Spaceship
    {
        private static readonly string GroundStationURI = "http://localhost:5014"; // will change
        public List<IReading> spaceShipReadings;
        public List<IFunction> spaceShipFunctions;
        private ConnectionManager _connectionManager = new ConnectionManager(GroundStationURI);
        private CommunicationHandler _communicationHandler = new CommunicationHandler(GroundStationURI);
        private Timer timer;
        private bool readyForNextSend = true;

        public Spaceship()
        {
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

        public async Task<bool> Send(IPacket packet)
        {
            bool result = false;
            string jsonPacket = packet.ToJson();
            Logging.LogPacket(packet.GetPacketType(), "Outbound", packet.GetPacketData());
            if (!_connectionManager.IsAuthenticated)
            {
                result = await _connectionManager.AuthenticateAsync(jsonPacket);
            }
            else
            {
                result = await _communicationHandler.UpdateGroundStationAsync(jsonPacket);
            }
            //!response.IsSuccessStatusCode
            return result;
        }

        public async Task SendAll()
        {
            this.readyForNextSend = false;
            while (this.spaceShipReadings.Count() != 0)
            {
                if (await Send(new DataPacket(this.spaceShipReadings[0])) == true)
                    this.spaceShipReadings.RemoveAt(0);
            }
            this.readyForNextSend = true;
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

        public bool Receive(string jsonObj)
        {
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
            Console.WriteLine("Readings sent at: " + DateTime.Now + (this.readyForNextSend ? " - Ready to Send...Sending" : " - Not ready to send"));
            if (this.readyForNextSend)
            {
                TelemetryService ts = new TelemetryService();
                TelemetryResponse tsTelem = ts.CollectTelemetry();
                this.AddReading(new PositionReading(tsTelem.Position));
                this.AddReading(new TemperatureReading(tsTelem.Temperature.ToString()));
                this.AddReading(new RadiationReading(tsTelem.Radiation.ToString()));
                this.AddReading(new VelocityReading(tsTelem.Velocity.ToString()));
                SendAll();
            }
        }
    }
}
