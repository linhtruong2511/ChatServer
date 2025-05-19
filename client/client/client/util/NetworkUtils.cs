using chatapp.dto;
using chatapp.dto.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.util
{
    internal class NetworkUtils
    {
        private NetworkUtils() { }
        public static Packet ReadStream(StreamReader stream)
        {
            return JsonConvert.DeserializeObject<Packet>(stream.ReadLine());
        }

        public static void WriteStream(StreamWriter stream, Packet packet)
        {
            stream.WriteLine(JsonConvert.SerializeObject(packet));
        }
        public static async Task<Packet> ReadStreamAsync(StreamReader stream)
        {
            return JsonConvert.DeserializeObject<Packet>(await stream.ReadLineAsync());
        }

        public static async Task WriteStreamAsync(StreamWriter stream, Packet packet)
        {
            await stream.WriteLineAsync(JsonConvert.SerializeObject(packet));
        }
    }
}
