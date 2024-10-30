using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using System.Text.Json;

namespace C_D.Utils
{
    public static class Serializer
    {
        public static string Serialize<T>(T data) => JsonSerializer.Serialize(data);

        public static T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);
    }
}
