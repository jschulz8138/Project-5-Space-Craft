//PayloadOps TODO:// Also remove this stub
//This is an example stub for an implementation of Functions and commands

namespace Project_5_Space_Craft
{
    public class FunctionStub : IFunction
    {
        private string command;

        public FunctionStub(string command)
        {
            this.command = command;
        }

        public string GetCommand()
        {
            return "Some Command";
        }

        public void RunCommand()
        {
            return;
        }

    }
}
