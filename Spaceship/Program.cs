//Main program that spaceship will be created in.
using DocumentFormat.OpenXml.Spreadsheet;
using Payload_Ops;
using Payload_Ops.Packets;
using Proj5Spaceship;

//Example Readings
Spaceship ship = new Spaceship();
ship.SendAll();

//VelocityReading velReading = new VelocityReading("9823m/s");
//TemperatureReading tempReading = new TemperatureReading("-270.45c");
//RadiationReading radReading = new RadiationReading("0.15mSv");
//PositionReading stub = new PositionReading("-47.36999493 151.738540034");
//DataPacket pkt = new DataPacket(stub);

//Add a reading
//ship.AddReading(velReading);
//ship.AddReading(tempReading);
//ship.AddReading(radReading);



//Example Functions
//IncreaseThrustFunction incThrustFunc = new IncreaseThrustFunction("27");
//SelfDestructFunction selfDesFunc = new SelfDestructFunction(true);
//FunctionPacket fpkt = new FunctionPacket(incThrustFunc);

string returnVar = Console.ReadLine();
