using System.Globalization;

namespace Project_5_Space_Craft
{
    internal class PacketWrapper
    {
        SpaceshipReadings readings;
        public PacketWrapper(SpaceshipReadings readings)
        {
            this.readings = readings;
        }
        public Dictionary<string, string> ToJson()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("datetime", DateTime.Now.ToString(new CultureInfo("en-US")));
            d.Add("datatype", readings.GetType().ToString());
            d.Add("data", readings.getData());
            d.Add("crc", this.CrcCalculator());
            return d;
        }
        //Placeholder Stub
        private string CrcCalculator()
        {
            return 0xFFFFFFFF.ToString("X8");
        }
    }
}
