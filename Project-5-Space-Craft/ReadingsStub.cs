//PayloadOps
//This is an example stud for an implementation of SpaceshipReadings
namespace Project_5_Space_Craft
{
    public class ReadingsStub : SpaceshipReading
    {
        private string data = "0";

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
