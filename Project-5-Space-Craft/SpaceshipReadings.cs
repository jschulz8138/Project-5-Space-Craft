namespace Project_5_Space_Craft
{
    public interface SpaceshipReadings
    {
        string getData();
        // You might add setData if dynamic updates are needed.
    }

    public class ReadingsStub : SpaceshipReadings
    {
        private string data;
        public ReadingsStub()
        {
            
            data = @"
            {
                ""Position"": ""X:0, Y:1, Z:3"",
                ""Temperature"": 28.5,
                ""Radiation"": 1.2,
                ""Velocity"": 3.4
            }";
        }

        public string getData()
        {
            return this.data;
        }

        public void SetData(string newData)
        {
            this.data = newData;
        }
    }
}
