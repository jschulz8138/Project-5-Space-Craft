//Main program that spaceship will be created in.
using Project_5_Space_Craft;


//Create objects
Spaceship ship = new Spaceship();
ReadingsStub testReading0 = new ReadingsStub("Test Data");
ReadingsStub testReading1 = new ReadingsStub("Test 1");
ReadingsStub testReading2 = new ReadingsStub("Test 2");

//Add a reading
ship.AddReading(testReading0);
ship.AddReading(testReading1);
ship.AddReading(testReading2);

//Send a reading
//ship.SendReading(testReading);

ReadingsStub stub = new ReadingsStub("TestingData");
DataPacket pkt = new DataPacket(stub);
//PacketWrapper pktWrap = new PacketWrapper();

Console.WriteLine(pkt.ToJson());

//Console.WriteLine(pktWrap.ToJsonReadings(stub));
//Logging.LogPacket("Temperature", "Outbound", "-95c");
//Logging.LogPacket("Radiation", "Outbound", "Extreme");
//Logging.LogPacket("Position", "Inbound", "Move Up");
//Logging.LogPacket("Velocity", "Outbound", "8232m/s");

//string keepSpaceshipRunning = Console.ReadLine();

