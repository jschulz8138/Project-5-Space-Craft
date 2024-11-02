//Main program that spaceship will be created in.
using Project_5_Space_Craft;

//Create objects
Spaceship ship = new Spaceship();
ReadingsStub testReading = new ReadingsStub();

//Add a reading
ship.AddReading(testReading);

//Send a reading
ship.SendReading(testReading);
