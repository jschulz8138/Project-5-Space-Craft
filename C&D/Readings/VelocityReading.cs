using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using C_D.Interface;

namespace Payload_Ops
{
    public class VelocityReading : IReading
    {
        private string data;

        public VelocityReading(string data)
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
