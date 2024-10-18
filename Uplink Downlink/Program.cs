using Uplink_Downlink;

class Program
{
    static async Task Main(string[] args)
    {
        var link = new Link("http://localhost:5000");
        link.StartServer();
    }
}