//PayloadOps
//Implementation of Temperature Readings, inheriting from IReading
namespace Project_5_Space_Craft
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
