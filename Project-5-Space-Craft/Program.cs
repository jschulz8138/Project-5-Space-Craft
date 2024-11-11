//Main program that spaceship will be created in.
using Payload_Ops;
using Payload_Ops.Packets;
using Payload_Ops.Readings;
using Payload_Ops.Functions;
using System.Text.Json;


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

FunctionStub fstub = new FunctionStub("TestingData");
FunctionPacket fpkt = new FunctionPacket(fstub);

Console.WriteLine(fpkt.ToJson());
Console.WriteLine("""{"Date":"2024-11-07T19:26:34.0177707-05:00","FunctionType":"FunctionStub","Command":"TestingData","PacketCRC":"some_crc_value"}""");
FunctionPacket? packet = JsonSerializer.Deserialize<FunctionPacket>(fpkt.ToJson());
//ship.Receive("""{"Date":"2024-11-07T19:26:34.0177707-05:00","FunctionType":"FunctionStub","Command":"TestingData","PacketCRC":"some_crc_value"}""");

//Console.WriteLine(pktWrap.ToJsonReadings(stub));
//Logging.LogPacket("Temperature", "Outbound", "-95c");
//Logging.LogPacket("Radiation", "Outbound", "Extreme");
//Logging.LogPacket("Position", "Inbound", "Move Up");
//Logging.LogPacket("Velocity", "Outbound", "8232m/s");

//string keepSpaceshipRunning = Console.ReadLine();

