//PayloadOps
//Implementation of self destruct function, inheriting from IFunction
namespace Payload_Ops
{
    public class MoveshipFunction : IFunction
    {
        private string command;

        public MoveshipFunction(string command) {
            this.command = command;
        }

        public string GetCommand(){
            return this.command;
        }

        public void RunCommand(){
            Console.WriteLine($"Executing command: {this.command}");
        }
    }
}
