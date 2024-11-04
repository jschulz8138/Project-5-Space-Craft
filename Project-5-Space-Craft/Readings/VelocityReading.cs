//PayloadOps
//Implementation of Velocity Readings, inheriting from IReading
namespace Project_5_Space_Craft
{
    public class VelocityReading : IReading
    {
        private string data;

        public VelocityReading(string data) {
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
