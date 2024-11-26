//Main program that spaceship will be created in.
using Spaceship;

Ship ship = new Ship();
UDLogicHandler _UDLogicHandler = new UDLogicHandler(ship);

// Run both tasks concurrently
await Task.WhenAll(
    _UDLogicHandler.StartClientAsync(Ship.GroundStationURI),
    _UDLogicHandler.StartServerAsync(null)
);