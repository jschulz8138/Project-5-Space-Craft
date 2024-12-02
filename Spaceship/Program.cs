//Main program that spaceship will be created in.
using Spaceship;

Ship ship = new Ship();                                         //Static ship. One ship for the entire tier
UDLogicHandler _UDLogicHandler = new UDLogicHandler(ship);

// Run both tasks concurrently
await Task.WhenAll(
    _UDLogicHandler.StartClientAsync(Ship.GroundStationURI),
    _UDLogicHandler.StartServerAsync(null)
);