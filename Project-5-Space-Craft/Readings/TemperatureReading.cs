//PayloadOps
//Implementation of Temperature Readings, inheriting from IReading
namespace Payload_Ops
{
    public class TemperatureReading : IReading
    {
        private string data;

        public TemperatureReading(string data) {
            this.data = data;
        }

        public string GetData()
        {
            return data;
        }

        public void SetData(string newData)
        {
            data = newData;
        }
    }
}
