﻿//PayloadOps
//This is an example stud for an implementation of SpaceshipReadings
namespace Project_5_Space_Craft
{
    public class ReadingsStub : IReading
    {
        private string data;

        public ReadingsStub(string data) {
            this.data = data;
        }

        public string GetData()
        {
            return data;
        }

        public void SetData(string newData)
        {
            data = newData;
        }
    }
}
