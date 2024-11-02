//PayloadOps
//This is the interface for all spaceship readings. All readings should inherit and be based off of this.
namespace Project_5_Space_Craft
{
    public interface IReading
    {
        public string GetData(); //Standardized function that returns the specified data as a string

        public void SetData(string newData); //Standardized function that sets the specified value of the data as a string
    }
}
