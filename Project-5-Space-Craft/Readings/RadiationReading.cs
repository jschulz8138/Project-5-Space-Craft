//PayloadOps
//Implementation of Raditation Readings, inheriting from IReading
namespace Project_5_Space_Craft
{
    public class RadiationReading : IReading
    {
        private string data;

        public RadiationReading(string data) {
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
