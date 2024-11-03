//PayloadOps
//This is the interface for all spaceship commands. All commands should inherit and be based off of this.
namespace Project_5_Space_Craft
{
    public interface IFunction
    {
        public string GetCommand();

        public void RunCommand(); //Standardized function that runs the command
    }
}
