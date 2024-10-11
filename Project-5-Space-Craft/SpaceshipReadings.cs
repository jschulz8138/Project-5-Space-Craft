namespace Project_5_Space_Craft
{
    public interface SpaceshipReadings
    {
        string getData();
        //You'll probably need a setData or updateData to change your readings :)
    }

    public class ReadingsStub : SpaceshipReadings
    {
        string data = "0";
        public string getData()
        {
            return this.data;
        }
    }

}
