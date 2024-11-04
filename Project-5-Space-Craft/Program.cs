//Main program that spaceship will be created in.
using Project_5_Space_Craft;


//Create objects
Spaceship ship = new Spaceship();
ReadingsStub testReading = new ReadingsStub("Test Data");

//Add a reading
ship.AddReading(testReading);

//Send a reading
ship.SendReading(testReading);

ReadingsStub stub = new ReadingsStub("TestingData");
DataPacket pkt = new DataPacket(stub);
//PacketWrapper pktWrap = new PacketWrapper();

Console.WriteLine(pkt.ToJson());

//Console.WriteLine(pktWrap.ToJsonReadings(stub));
Logging.LogPacket("Temperature", "Outbound", "-95c");
Logging.LogPacket("Radiation", "Outbound", "Extreme");
Logging.LogPacket("Position", "Inbound", "Move Up");
Logging.LogPacket("Velocity", "Outbound", "8232m/s");



