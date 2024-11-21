//Main program that spaceship will be created in.
using Proj5Spaceship;

Spaceship ship = new Spaceship();
UDLogicHandler _UDLogicHandler = new UDLogicHandler(ship);

// Run both tasks concurrently
await Task.WhenAll(
    _UDLogicHandler.StartClientAsync(Spaceship.GroundStationURI),
    _UDLogicHandler.StartServerAsync(null)
);