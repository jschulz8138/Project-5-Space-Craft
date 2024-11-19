using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using C_D.Interface;

namespace C_D.Readings
{
    internal class ReadingStub : IReading
    {
        private string data;

        public ReadingStub(string data)
        {
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
