﻿//PayloadOps TODO:// Also remove this stub
//This is an example stub for an implementation of Functions and commands

namespace Payload_Ops.Functions
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
            return this.command;
        }

        public void RunCommand()
        {
            return;
        }

    }
}
