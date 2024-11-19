using Uplink_Downlink;
using System;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Waiting for the server to start...");
        await Task.Delay(1000);
        var authPacket = new AuthPacket
        {
            Username = "user",
            Password = "pass"
        };

        // Serialize the AuthPacket to JSON string
        string jsonPacket = JsonSerializer.Serialize(authPacket);


        var connectionManager = new ConnectionManager("http://localhost:5014");



        if (await connectionManager.AuthenticateAsync(jsonPacket))
        {
            Console.WriteLine("Authentication successful.");

            // Initializing the CommunicationHandler for data transmission
            var communicationHandler = new CommunicationHandler("http://localhost:5014");
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

internal class AuthPacket
{
    public string Username { get; set; }
    public string Password { get; set; }
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
            // Creating a data packet object
            var dataPacket = new { status = "active" };
            string packet = JsonSerializer.Serialize(dataPacket);

            // Debugging: Print the packet to verify
            Console.WriteLine($"Sending data packet: {packet}");

            await _handler.UpdateGroundStationAsync(packet);
            Console.WriteLine("Updated ground station with general data.");
            await Task.Delay(_intervalInSeconds * 1000); // Wait for the specified interval before next update
        }
    }
}