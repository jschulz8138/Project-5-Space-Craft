namespace Project_5_Space_Craft
{
    internal interface SpaceshipReadings
    {
        string getData();
        //You'll probably need a setData or updateData to change your readings :)
    }

    internal class ReadingsStub : SpaceshipReadings
    {
        string data = "0";
        public string getData()
        {
            this.data = (Int32.Parse(this.data) + 1).ToString();
            return this.data;
        }
    }

}
