using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using C_D.Interface;

namespace C_D.Readings
{
    public class PositionReading : IReading
    {
        private string data;

        public PositionReading(string initialData)
        {
            data = initialData;
        }

        public string GetData() => data;

        public void SetData(string newData) => data = newData;
    }
}
