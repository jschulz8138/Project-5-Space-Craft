using Uplink_Downlink;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {

        var connectionManager = new ConnectionManager("https://127.0.0.1");//replacing with the server IP or hostname
        //authencticating with the URL

        if (await connectionManager.AuthenticateAsync("user", "pass"))
        {
            Console.WriteLine("Authentication successful.");

            // Initializing the CommunicationHandler for data transmission
            var communicationHandler = new CommunicationHandler("https://127.0.0.1");
            int delayBeforeUpdate = 60; // set to 60 for testing

            // Starting periodic data updates to the ground station
            var dataUpdater = new DataUpdater(communicationHandler, delayBeforeUpdate); // Updating every 60 seconds
            await dataUpdater.StartAsync();
        }
        else
        {
            Console.WriteLine("Authentication failed.");
        }



    }
}


public class DataUpdater
{
    private readonly CommunicationHandler _handler;
    private readonly int _intervalInSeconds;

    public DataUpdater(CommunicationHandler handler, int intervalInSeconds)
    {
        _handler = handler;
        _intervalInSeconds = intervalInSeconds;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            var generalData = new Dictionary<string, string> { { "status", "active" } };
            await _handler.UpdateGroundStationAsync(generalData);
            Console.WriteLine("Updated ground station with general data.");
            await Task.Delay(_intervalInSeconds * 1000); // Wait for the specified interval before next update
        }
    }
}