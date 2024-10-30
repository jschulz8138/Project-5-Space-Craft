using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_D.Models;
using C_D.Services;
using C_D.Utils;
using System.Net;

namespace C_D.Controllers
{
    public class CAndDController
    {
        private readonly CommandService _commandService;
        private readonly TelemetryService _telemetryService;

        public CAndDController()
        {
            _commandService = new CommandService();
            _telemetryService = new TelemetryService();
        }

        public void StartServer()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5000/");
            listener.Start();
            Console.WriteLine("Server started at http://localhost:5000/");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/api/command")
                {
                    using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
                    var json = reader.ReadToEnd();
                    var commandRequest = Serializer.Deserialize<CommandRequest>(json);
                    var result = _commandService.ExecuteCommand(commandRequest);
                    var responseData = Encoding.UTF8.GetBytes(Serializer.Serialize(result));
                    response.OutputStream.Write(responseData, 0, responseData.Length);
                }
                else if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/api/telemetry")
                {
                    var telemetry = _telemetryService.GetTelemetryData();
                    var responseData = Encoding.UTF8.GetBytes(Serializer.Serialize(telemetry));
                    response.OutputStream.Write(responseData, 0, responseData.Length);
                }

                response.Close();
            }
        }
    }
}
