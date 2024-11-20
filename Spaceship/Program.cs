//Main program that spaceship will be created in.
using Proj5Spaceship;

Spaceship ship = new Spaceship();

UDLogicHandler _UDLogicHandler = new UDLogicHandler(ship);

await _UDLogicHandler.StartClient(Spaceship.GroundStationURI);

_UDLogicHandler.StartServer(null);
