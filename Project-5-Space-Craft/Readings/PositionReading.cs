//PayloadOps
//Implementation of Position Readings, inheriting from IReading
namespace Project_5_Space_Craft
{
    public class PositionReading : IReading
    {
        private string data;

        public PositionReading(string data) {
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
