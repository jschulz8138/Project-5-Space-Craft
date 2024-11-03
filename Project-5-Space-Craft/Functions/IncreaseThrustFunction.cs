//PayloadOps
//Implementation of increasing thrust, inheriting from IFunction
namespace Project_5_Space_Craft
{
    public class IncreaseThrustFunction : IFunction
    {
        private int thrust;

        public IncreaseThrustFunction(int thrust) {
            this.thrust = thrust;
        }

        public string GetCommand(){
            return this.thrust.ToSting();
        }

        public void RunCommand(){
            Console.WriteLine($"Executing command: {this.thrust.ToString}");
        }
    }
}
