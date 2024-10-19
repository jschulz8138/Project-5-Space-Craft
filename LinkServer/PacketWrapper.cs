using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PacketWrapper
{
    public string JsonData { get; set; }

    public PacketWrapper(string jsonData)
    {
        this.JsonData = jsonData;
    }
}
