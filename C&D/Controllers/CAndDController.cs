using CAndD.Controllers;
using CAndD.Services;

namespace CAndD.Controllers
{
    public class CAndDController
    {
        private readonly CommandService _commandService;
        private readonly TelemetryService _telemetryService;
        private readonly MessageService _messageService;

        public CAndDController()
        {
            _commandService = new CommandService();
            _telemetryService = new TelemetryService();
            _messageService = new MessageService();
        }

        public void ProcessCommand(string command)
        {
            // Validate and handle command messages
            var isValid = _messageService.ValidateMessage(command);
            if (isValid)
            {
                _commandService.ExecuteCommand(command);
                Console.WriteLine("Command executed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid command format.");
            }
        }

        public void DisplayTelemetryData()
        {
            var telemetryData = _telemetryService.CollectTelemetry();
            Console.WriteLine("Telemetry Data:");
            Console.WriteLine(telemetryData);

            // Save telemetry data to JSON file
            _telemetryService.SaveTelemetryDataAsJson(telemetryData);
            Console.WriteLine("Telemetry data saved to TelemetryData.json");
        }

    }
}
