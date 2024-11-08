//PayloadOps
//Implementation of increasing thrust, inheriting from IFunction
namespace Payload_Ops
{
    public class IncreaseThrustFunction : IFunction
    {
        private int thrust;

        public IncreaseThrustFunction(int thrust) {
            this.thrust = thrust;
        }
        public IncreaseThrustFunction(string thrust)
        {
            this.thrust = int.Parse(thrust);
        }

        public string GetCommand(){
            return this.thrust.ToString();
        }

        public void RunCommand(){
            Console.WriteLine($"Executing command: {this.thrust.ToString()}");
        }
    }
}
