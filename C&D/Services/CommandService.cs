using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using C_D.Models;

namespace C_D.Services
{
    public class CommandService
    {
        public CommandReadings ExecuteCommand(CommandRequest command)
        {
            // Implement command execution logic here
            return new CommandReadings { Status = $"Executed {command.CommandType} command: {command.Command}" };
        }
    }
}
